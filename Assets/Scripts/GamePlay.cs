using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePlay : MonoBehaviour
{
    [SerializeField] BranchManager _branchManager;
    public List<Branch> ListAllBranchs = new List<Branch>();
    private int _countClick = 0;
    private int _idCurrentBranch;
    private int _idNextBranch;
    private bool _canClick=true;
    private bool _isChangeBird = false;
    public LoadData loadData;
    private void Update()
    {
        CheckCantClick();

        if (_canClick == false)
        {
            return;
        }

        if (GameManager._instance.gameState == GameManager.GameState.SortBirds)
        {
            if (Input.GetMouseButtonDown(0) || _isChangeBird)
            {
                _isChangeBird = false;

                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
                RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

                if (hit.collider != null)
                {
                    Branch branch = hit.collider.gameObject.GetComponent<Branch>();

                    _countClick++;

                    if (_countClick == 1)
                    {
                        _idCurrentBranch = branch.id;

                        if (ListAllBranchs[_idCurrentBranch - 1].listBirds.Count != 0 && !ListAllBranchs[_idCurrentBranch - 1].IsFullSameBirdsOnBranch() /*_idOldNextBranch!= _idCurrentBranch*/)
                        {
                            branch.Touching();
                        }
                        else
                        {
                            _countClick = 0;
                        }
                    }
                    if (_countClick == 2)
                    {
                        _idNextBranch = branch.id;

                        if (_idNextBranch != _idCurrentBranch)
                        {
                            if (_branchManager.IsSameIdBird(_idCurrentBranch - 1, _idNextBranch - 1))
                            {
                                StartCoroutine(MoveBirdToNextBranch());
                            }
                            else
                            {
                                int indexCurrent = _idCurrentBranch - 1;
                                ListAllBranchs[indexCurrent].UnTouching();
                                ListAllBranchs[indexCurrent].listBirdMove.Clear();
                                _isChangeBird = true;
                                _countClick = 0;
                            }
                        }
                        else
                        {
                            int indexCurrent = _idCurrentBranch - 1;
                            ListAllBranchs[indexCurrent].UnTouching();
                            ListAllBranchs[indexCurrent].ClearBirdMove();
                            _countClick = 0;
                        }
                    }
                }
            }
        }
    }
    void CheckCantClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                _canClick = false;
            }
            else
            {
                _canClick = true;
            }
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                _canClick = false;
            }
            else
            {
                _canClick = true;
            }
        }
    }

    IEnumerator MoveBirdToNextBranch()
    {
     //   _idOldNextBranch = _idNextBranch;

        int indexCurrentBranch = _idCurrentBranch-1;
        int indexNextBranch = _idNextBranch-1;

        int AmountSlotNextBranch = ListAllBranchs[indexNextBranch].PositionSlotAvailable().Count;
        int AmountBirdMove = ListAllBranchs[indexCurrentBranch].listBirdMove.Count;

        bool  lastBridMoveToBranch = false;

        int index = 0;

        if(AmountSlotNextBranch>AmountBirdMove)
        {
            index = AmountBirdMove;
        }
        else
        {
            index = AmountSlotNextBranch;
            lastBridMoveToBranch = true;

            if(AmountBirdMove- AmountSlotNextBranch > 0)
            {
                ListAllBranchs[indexCurrentBranch].UnTouching();
            }
        }
        float timeWaitBirdMove = 0f;

        List<BirdUndo> ListBirdUndo = new List<BirdUndo>();

        for (int i = 0; i < index; i++)
        {
            Vector3 PosOldSlot = ListAllBranchs[indexCurrentBranch].listBirdMove[i].transform.position;
            BirdUndo BirdUndos = new BirdUndo(indexCurrentBranch + 1, ListAllBranchs[indexCurrentBranch].listBirdMove[i], PosOldSlot, indexNextBranch + 1);
            ListBirdUndo.Add(BirdUndos);

            ListAllBranchs[indexCurrentBranch].listBirdMove[i].transform.parent = ListAllBranchs[indexNextBranch]._animator.gameObject.transform;

            bool IsMoveDown = (ListAllBranchs[indexCurrentBranch].id % 2 == ListAllBranchs[indexNextBranch].id % 2) ? true : false;

            if (IsMoveDown)
            {
                if(  ListAllBranchs[indexCurrentBranch].listBirdMove[i].transform.position.x <= ListAllBranchs[indexNextBranch].PositionSlotAvailable()[i].x)
                {
                    if(ListAllBranchs[indexCurrentBranch].id % 2==0)
                     {
                        ListAllBranchs[indexCurrentBranch].listBirdMove[i].FlipX();
                        ListAllBranchs[indexCurrentBranch].listBirdMove[i].MoveToTarget(ListAllBranchs[indexNextBranch].PositionSlotAvailable()[i], true);
                    }
                    else
                    {
                       if(ListAllBranchs[indexCurrentBranch].listBirdMove[i].transform.position.x == ListAllBranchs[indexNextBranch].PositionSlotAvailable()[i].x)
                        {
                            ListAllBranchs[indexCurrentBranch].listBirdMove[i].FlipX();
                            ListAllBranchs[indexCurrentBranch].listBirdMove[i].MoveToTarget(ListAllBranchs[indexNextBranch].PositionSlotAvailable()[i], true);
                        }
                        ListAllBranchs[indexCurrentBranch].listBirdMove[i].MoveToTarget(ListAllBranchs[indexNextBranch].PositionSlotAvailable()[i], false);
                    }
                }
                else
                {
                     if (ListAllBranchs[indexCurrentBranch].id % 2 == 0)
                      {
                        ListAllBranchs[indexCurrentBranch].listBirdMove[i].MoveToTarget(ListAllBranchs[indexNextBranch].PositionSlotAvailable()[i], false);
                      }
                    else
                    {
                        ListAllBranchs[indexCurrentBranch].listBirdMove[i].FlipX();
                        ListAllBranchs[indexCurrentBranch].listBirdMove[i].MoveToTarget(ListAllBranchs[indexNextBranch].PositionSlotAvailable()[i], true);
                    }

                }
            }
            else
            {
                ListAllBranchs[indexCurrentBranch].listBirdMove[i].MoveToTarget(ListAllBranchs[indexNextBranch].PositionSlotAvailable()[i], true);
            }
            yield return new WaitForSeconds(Random.RandomRange(0.05f, 0.1f));
            timeWaitBirdMove= CalculerTimeWait(ListAllBranchs[indexCurrentBranch].listBirdMove[i].id);
        }

        StateUndo NewStateUndo = new StateUndo(ListBirdUndo);
        GameManager._instance.StackStateUndos.Push(NewStateUndo);

        for (int i = 0; i < index; i++)
        {
            ListAllBranchs[indexNextBranch].listBirds.Add(ListAllBranchs[indexCurrentBranch].listBirdMove[i]);
        }

        ListAllBranchs[indexCurrentBranch].ClearBirdMove();
        ListAllBranchs[indexCurrentBranch].RemoveBirdPromListBird(index);
        _countClick = 0;
        yield return new WaitForSeconds(timeWaitBirdMove);
        ListAllBranchs[indexNextBranch].StateShaky();
        yield return new WaitForSeconds(0.15f);
        ListAllBranchs[indexNextBranch].StateIdle();
        //_idOldNextBranch = -100;

        if (lastBridMoveToBranch)
        {
            if (ListAllBranchs[indexNextBranch].IsFullSameBirdsOnBranch())
            {
                yield return new WaitForSeconds(0.18f);
                ListAllBranchs[indexNextBranch].MoveAllBirdToOutScreen();
            }
          
        }
    }
    public float CalculerTimeWait(int IdBird)
    {
        float TimeWait = 0f;
        if(IdBird==2)
        {
            TimeWait = 0.78f;
        }else if(IdBird==1)
        {
            TimeWait = 1.1f;
        }
        else if(IdBird==3)
        {
            TimeWait = 1.12f;
        }
        return TimeWait;
    }
  public  IEnumerator WaitTimeLoadData(int level)
    {
        _branchManager.LoadDataBirdOnBranchs(loadData.LoadDataLevel(level));
        yield return new WaitForSeconds(0.1f); 
        _branchManager.LoadAllBranchLevel(5);   /// CountBrach
        yield return new WaitForSeconds(0.1f);
        _branchManager.MoveToScreen();
        yield return new WaitForSeconds(0.4f);
        ListAllBranchs = _branchManager.BonrAllBirdOnBranch();
        yield return new WaitForSeconds(0.1f);
        _branchManager.MoveAllBirdSToAllBranchs();
    }
}
