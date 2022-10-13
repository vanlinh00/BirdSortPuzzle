using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GamePlay : MonoBehaviour
{
    [SerializeField] Vector3 _posStartRightBranch;
    [SerializeField] Vector3 _posStartLeftBranch;
    [SerializeField] BranchManager _branchManager;

    public List<Branch> ListAllBranchs = new List<Branch>();
    private int CountClick = 0;
    private int idCurrentBranch;
    private int idNextBranch;
    private bool _canClick=true;
    public bool _isChangeBird = false;
    private void Update()
    {
        CheckCantClick();

        if (_canClick == false)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0)|| _isChangeBird)
        {
            _isChangeBird = false;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                Branch branch = hit.collider.gameObject.GetComponent<Branch>();
               
                CountClick++;

                if(CountClick==1)
                {
                    idCurrentBranch = branch.id;

                    if(ListAllBranchs[idCurrentBranch-1].birds.Count!=0)
                    {
                        branch.Touching();
                    }
                    else
                    {
                        CountClick = 0;
                    }
                }
                if(CountClick==2)
                {
                   idNextBranch = branch.id;

                   if(idNextBranch!=idCurrentBranch)
                    {
                        if(_branchManager.IsSameIdBird( idCurrentBranch-1,idNextBranch - 1))
                        {
                            MoveBirdToNextBranch();
                        }
                        else
                        {
                            int indexCurrent = idCurrentBranch - 1;
                            ListAllBranchs[indexCurrent].UnTouching();
                            CountClick = 0;
                            _isChangeBird = true;
                        }
                       
                    }
                    else
                    {
                        int indexCurrent = idCurrentBranch - 1;
                        ListAllBranchs[indexCurrent].UnTouching();
                        ListAllBranchs[indexCurrent].ClearBirdMove();
                        CountClick = 0;
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
    
    public void MoveBirdToNextBranch()
    {
        int indexCurrentB = idCurrentBranch-1;
        int indexNextB = idNextBranch-1;

        int AmountSlot = ListAllBranchs[indexNextB].PositionSlotAvailable().Count;
        int AmountBirdMove = ListAllBranchs[indexCurrentB].listBirdMove.Count;

        int index = 0;
        
        if(AmountSlot>AmountBirdMove)
        {
            index = AmountBirdMove;
        }
        else
        {
            index = AmountSlot;

        }
   
        for (int i = 0; i < index; i++)
        {
             ListAllBranchs[indexCurrentB].listBirdMove[i].MoveToTarget(ListAllBranchs[indexNextB].PositionSlotAvailable()[i]);
        }
        for (int i = 0; i < index; i++)
        {
            ListAllBranchs[indexNextB].birds.Add(ListAllBranchs[indexCurrentB].listBirdMove[i]);
        }
        ListAllBranchs[indexCurrentB].RemoveBirdPromListBird(index);
        ListAllBranchs[indexCurrentB].ClearBirdMove();
        CountClick = 0;
    }

    int CountBrach = 0;
   [SerializeField] int level ;
    public void Start()
    {
        if(level==1)
        {
            CountBrach = 2;
        }
        if(level==2||level==3)
        {
            CountBrach = 5;
        }
       
        _branchManager.LoadDataLevel(level);
        StartCoroutine(WaitTime());
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.1f); 
        _branchManager.LoadAllBranchLeve(CountBrach);
        ListAllBranchs = _branchManager.BonrAllBirdOnBranch();
    }
}
