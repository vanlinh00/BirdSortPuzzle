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
    public float _TimeMove;
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
                                StartCoroutine(MoveBirdToNextBranch(_idCurrentBranch - 1, _idNextBranch - 1));
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
        else
        {
            _countClick = 0;
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
    IEnumerator MoveBirdToNextBranch(int indexCurrentBranch, int indexNextBranch)
    {
     //   _idOldNextBranch = _idNextBranch;

        int AmountSlotNextBranch = ListAllBranchs[indexNextBranch].PositionSlotAvailable().Count;
        int AmountBirdMove = ListAllBranchs[indexCurrentBranch].listBirdMove.Count;

        bool  lastBridMoveToBranch = false;
        int CountBirdMove = 0;

        if(AmountSlotNextBranch>AmountBirdMove)
        {
            CountBirdMove = AmountBirdMove;
        }
        else
        {
            CountBirdMove = AmountSlotNextBranch;
            lastBridMoveToBranch = true;
            if(AmountBirdMove- AmountSlotNextBranch > 0)
            {
                ListAllBranchs[indexCurrentBranch].UnTouching();
            }
        }

        float timeWaitBirdMove = 0f;

        List<BirdUndo> ListBirdUndo = new List<BirdUndo>();

        for (int i = 0; i < CountBirdMove; i++)
        {
            float DistanceBirdMove = Vector3.Distance(ListAllBranchs[indexCurrentBranch].listBirdMove[i].transform.position, (ListAllBranchs[indexNextBranch].PositionSlotAvailable()[i]));

            _TimeMove = 0.44f * DistanceBirdMove / 1.863758f;
            ListAllBranchs[indexCurrentBranch].listBirdMove[i].TimeMove = _TimeMove;
            ListAllBranchs[indexCurrentBranch].listBirdMove[i].transform.parent = null;
            ListAllBranchs[indexCurrentBranch].listBirdMove[i].ParentObj = ListAllBranchs[indexNextBranch]._animator.gameObject;

            Vector3 PosOldSlot = ListAllBranchs[indexCurrentBranch].allSlots[ListAllBranchs[indexCurrentBranch].listBirds.Count-1- i].transform.position; /*ListAllBranchs[indexCurrentBranch].listBirdMove[i].transform.position;*/

            BirdUndo BirdUndos = new BirdUndo(indexCurrentBranch + 1, ListAllBranchs[indexCurrentBranch].listBirdMove[i], PosOldSlot, indexNextBranch + 1,false);
            ListBirdUndo.Add(BirdUndos);

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
            timeWaitBirdMove = timeWaitBirdMove * DistanceBirdMove/2.6f;
        }
        
        StateUndo NewStateUndo = new StateUndo(ListBirdUndo,null);
        GameManager._instance.StackStateUndos.Push(NewStateUndo);

        for (int i = 0; i < CountBirdMove; i++)
        {
            ListAllBranchs[indexNextBranch].listBirds.Add(ListAllBranchs[indexCurrentBranch].listBirdMove[i]);
        }

        ListAllBranchs[indexCurrentBranch].ClearBirdMove();
        ListAllBranchs[indexCurrentBranch].RemoveBirdPromListBird(CountBirdMove);
        _countClick = 0;
        yield return new WaitForSeconds(timeWaitBirdMove-0.12f);
        ListAllBranchs[indexNextBranch].StateShaky();
        yield return new WaitForSeconds(0.15f);
        ListAllBranchs[indexNextBranch].StateIdle();
        yield return new WaitForSeconds(0.2f);

        //_idOldNextBranch = -100;

        if (lastBridMoveToBranch)
        {
            if (ListAllBranchs[indexNextBranch].IsFullSameBirdsOnBranch())
            {
                SaveStateBirdsWhenFinishBranch(CountBirdMove, indexNextBranch);
                yield return new WaitForSeconds(0.2f);
                ListAllBranchs[indexNextBranch].MoveAllBirdToOutScreen();
         
            }
        }
    }
    public void SaveStateBirdsWhenFinishBranch(int CountBirdMoveToBranch,int indexNextBranch)
    {
        List<BirdUndo> ListBirdUndo = new List<BirdUndo>();

        for(int i=0;i< ListAllBranchs[indexNextBranch].listBirds.Count- CountBirdMoveToBranch;i++)
        {
            Vector3 PosOldSlot = ListAllBranchs[indexNextBranch].listBirds[i].transform.position;
            BirdUndo BirdUndos = new BirdUndo(indexNextBranch + 1, ListAllBranchs[indexNextBranch].listBirds[i], PosOldSlot, ListAllBranchs[indexNextBranch].id,false);
            ListBirdUndo.Add(BirdUndos);
        }
        StateUndo NewStateUndo = new StateUndo(ListBirdUndo,null);
        GameManager._instance.StackStateUndos.Push(NewStateUndo);
    }
    public float CalculerTimeWait(int IdBird)
    {
        float TimeWait = 0f;
        if (IdBird == 2)
        {
            TimeWait = 0.68f;
        }
        else if (IdBird == 1)
        {
            TimeWait = 1f;
        }
        else if (IdBird == 3)
        {
            TimeWait = 1.02f;
        }
        return TimeWait;
    }
    public  IEnumerator WaitTimeLoadData(int level)
    {
        _branchManager.LoadDataBirdOnBranchs(loadData.LoadDataLevel(level));
        yield return new WaitForSeconds(0.1f); 
        _branchManager.LoadAllBranchLevel();  
        yield return new WaitForSeconds(0.1f);
        _branchManager.MoveToScreen();
        yield return new WaitForSeconds(0.4f);
        ListAllBranchs = _branchManager.BonrAllBirdOnBranch();
        yield return new WaitForSeconds(0.1f);
        _branchManager.MoveAllBirdSToAllBranchs();
    }
    public void Renew()
    {
        loadData.Renew();
        _branchManager.RenewAllBranchs();
        _branchManager.RenewAllBirds();
        ListAllBranchs.Clear();
        _countClick = 0;
        _canClick = true;
        _isChangeBird = false;
    }

}
