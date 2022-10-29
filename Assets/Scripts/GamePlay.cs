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
    public bool IsBirdMoving;
    public float _timeWait;

    // tutorial
    public bool isDisplayTut = false;

    // check finish game
    public int AmountListBirdsFinishGame = 0;

    private void Awake()
    {
        _idCurrentBranch = -100;
           IsBirdMoving = true;
    }
    private void Update()
    {
        CheckCantClick();

        //if (_canClick == false)
        //{
        //    Debug.Log("_canClick");
        //    return;
        //}

        if (GameManager._instance.gameState == GameManager.GameState.SortBirds&&GameManager._instance.gameState!= GameManager.GameState.PauseGame)
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

                        if (ListAllBranchs[_idCurrentBranch - 1].listBirds.Count != 0 && !ListAllBranchs[_idCurrentBranch - 1].IsFullSameBirdsOnBranch())
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
       //  ListAllBranchs[indexNextBranch].IsBranchBirdsMoving = true;
        int AmountSlotNextBranch = ListAllBranchs[indexNextBranch].PositionSlotAvailable(0).Count;
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

        List<BirdUndo> ListBirdUndo = new List<BirdUndo>();
        IsBirdMoving = true;
        for (int i = 0; i < CountBirdMove; i++)
        {
            int IdSlot = ListAllBranchs[indexCurrentBranch].listBirds.Count - 1 - i;
            BirdUndo BirdUndos = new BirdUndo(indexCurrentBranch + 1, ListAllBranchs[indexCurrentBranch].listBirdMove[i], indexNextBranch + 1, false,IdSlot);
            ListBirdUndo.Add(BirdUndos);
        }
        StateUndo NewStateUndo = new StateUndo(ListBirdUndo, null);
        GameManager._instance.StackStateUndos.Push(NewStateUndo);
        UiGamePlay._instance.CountNumberUndo();

        for (int i = 0; i < CountBirdMove; i++)
        {
            ListAllBranchs[indexNextBranch].listBirds.Add(ListAllBranchs[indexCurrentBranch].listBirdMove[i]);
        }

        if (lastBridMoveToBranch)
        {
            if (ListAllBranchs[indexNextBranch].IsFullSameBirdsOnBranch())
            {
                SaveStateBirdsWhenFinishBranch(CountBirdMove, indexNextBranch);
            }
        }

        for (int i = 0; i < CountBirdMove; i++)
        {
            Vector3 PosNewSlot = ListAllBranchs[indexNextBranch].PositionSlotAvailable(CountBirdMove)[i];
          
            Bird birdMove = ListAllBranchs[indexCurrentBranch].listBirdMove[i];
            float DistanceBirdMove = Vector3.Distance(birdMove.transform.position, PosNewSlot);
            birdMove.transform.parent = null;
            birdMove.ParentObj = ListAllBranchs[indexNextBranch]._animator.gameObject;
            birdMove.SetOrderLayer(40);
            birdMove.idSlot = ListAllBranchs[indexNextBranch].ListIdSlotAvailable(CountBirdMove)[i];
            bool IsMoveDown = (ListAllBranchs[indexCurrentBranch].id % 2 == ListAllBranchs[indexNextBranch].id % 2) ? true : false;
            birdMove.idBranchStand = indexNextBranch;
            birdMove.TimeMove = 0.55f * DistanceBirdMove / 2.5f;
            if (IsMoveDown)
            {
                birdMove.TimeMove = 0.7f * DistanceBirdMove / 2.5f;

                if (birdMove.transform.position.x <= PosNewSlot.x)
                {
                    if(ListAllBranchs[indexCurrentBranch].id % 2==0)
                     {
                        birdMove.FlipX();
                        birdMove.MoveToTarget(true);
                    }
                    else
                    {
                       if(ListAllBranchs[indexCurrentBranch].listBirdMove[i].transform.position.x == PosNewSlot.x)
                        {
                            birdMove.FlipX();
                            birdMove.MoveToTarget(true);
                        }
                        birdMove.MoveToTarget(false);
                    }
                }
                else
                {
                     if (ListAllBranchs[indexCurrentBranch].id % 2 == 0)
                      {
                        birdMove.MoveToTarget(false);
                    }
                    else
                    {
                        birdMove.FlipX();
                        birdMove.MoveToTarget( true);
                    }
                }
            }
            else
            {
                birdMove.MoveToTarget( true);
            }
            yield return new WaitForSeconds(Random.RandomRange(0.05f, 0.1f));
        }
        ListAllBranchs[indexCurrentBranch].ClearBirdMove();
        ListAllBranchs[indexCurrentBranch].RemoveBirdFromListBird(CountBirdMove);
        _countClick = 0;

        yield return new WaitForSeconds(0.7f);
        if (GameManager._instance.Getlevel() == 3 && !isDisplayTut)
        {
            isDisplayTut = true;
            TutorialManager._instance.MoveHandToTarget(new Vector3(0.89f, -3.046f, 0));
        }

        yield return new WaitForSeconds(0.5f);
        ListAllBranchs[indexNextBranch].SetOrderBirdsAndBrands(20);
        if (lastBridMoveToBranch)
        {
            if (ListAllBranchs[indexNextBranch].IsFullSameBirdsOnBranch())
            {
                IsBirdMoving = true;
                ListAllBranchs[indexNextBranch].MoveAllBirdToOutScreen();
                yield return new WaitForSeconds(1.2f);
                IsBirdMoving = false;
                AmountListBirdsFinishGame--;
                if (AmountListBirdsFinishGame==0)
                {
                    Uicontroller._instance.OpenUiAndGame();
                }

            }else
            {
                IsBirdMoving = false;
            }
        }
        else
        {
            IsBirdMoving = false;
        }
    }
    public IEnumerator ShakyBranch(int indexNextBranch, float TimeWait)
    {
        yield return new WaitForSeconds(TimeWait);
        ListAllBranchs[indexNextBranch].StateShaky();
        yield return new WaitForSeconds(0.2f);
        ListAllBranchs[indexNextBranch].StateIdle();
        yield return new WaitForSeconds(0.2f);
      //  ListAllBranchs[indexNextBranch].IsBranchBirdsMoving = false;
    }
    public void SaveStateBirdsWhenFinishBranch(int CountBirdMoveToBranch, int indexNextBranch)
    {
        List<BirdUndo> ListBirdUndo = new List<BirdUndo>();
        int IndexBirds = ListAllBranchs[indexNextBranch].listBirds.Count - CountBirdMoveToBranch;
        for (int i = IndexBirds - 1; i >= 0; i--)
        {
            int IdSlot = i; 
            BirdUndo BirdUndos = new BirdUndo(indexNextBranch + 1, ListAllBranchs[indexNextBranch].listBirds[i], indexNextBranch + 1, false, IdSlot);
            ListBirdUndo.Add(BirdUndos);
        }
        StateUndo NewStateUndo = new StateUndo(ListBirdUndo, null);
        GameManager._instance.StackStateUndos.Push(NewStateUndo);
        UiGamePlay._instance.CountNumberUndo();
    }

    public  IEnumerator WaitTimeLoadData(int level)
    {
        yield return new WaitForSeconds(0.1f);
        _branchManager.LoadDataBirdOnBranchs(loadData.LoadDataLevel(level));
        yield return new WaitForSeconds(0.1f); 
        _branchManager.LoadAllBranchLevel();  
        yield return new WaitForSeconds(0.1f);
        _branchManager.MoveToScreen();
        yield return new WaitForSeconds(0.4f);
        ListAllBranchs = _branchManager.GetListAllBranhs();
        yield return new WaitForSeconds(0.1f);
        _branchManager.MoveAllBirdSToAllBranchs();
        AmountListBirdsFinishGame = BranchManager._instance.CountListBirdsFinishGame();
    }
    public  void EnableBirdsCanChangeSeats()
    {
        for(int i=0;i< ListAllBranchs.Count;i++)
        {
            if(ListAllBranchs[i].CanChangeSeats())
            {
                ListAllBranchs[i].SetOrderBirdsAndBrands(20);
            }
            else
            {
                ListAllBranchs[i].SetOrderBirdsAndBrands(15);
            }
        }
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
        _idCurrentBranch = -100;
    }

    public void UnTouchingAndRemoveAllBirdsMoveCurrentBranch()
    {
        if(_idCurrentBranch!=-100)
        {
            ListAllBranchs[_idCurrentBranch - 1].listBirdMove.Clear();
            ListAllBranchs[_idCurrentBranch - 1].UnTouching();
        }
    }
   
 
}
