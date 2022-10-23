using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Branch : MonoBehaviour
{   
    public int id;
    public List<GameObject> allSlots = new List<GameObject>();
    public List<Bird> listBirds = new List<Bird>();
    public List<Bird> listBirdMove = new List<Bird>();
    public Animator _animator;
    public Vector3 posOutScreen;
    [SerializeField] SpriteRenderer _spriteRenderer;

    public void AddToListBrids(Bird bird)
    {
        listBirds.Add(bird);
    }
    public void DeleteLastBird()
    {
        if(listBirds.Count!=0)
        {
            listBirds.RemoveAt(listBirds.Count - 1);
        } 
    }
    public Vector3 GetPosSlot(int Number)
    {
        return allSlots[Number].transform.position;
    }
    public void RemoveBirdPromListBird(int CountBirdMoved)
    { 
        int index = listBirds.Count - CountBirdMoved;
        for (int i = listBirds.Count - 1; i >= index; i--)
        {
            listBirds.RemoveAt(i);
        }
    }
    public void Touching()
    {
        if (listBirds.Count != 0)
        {
            int CountBirds = listBirds.Count - 1;
            int IdFistBird = listBirds[listBirds.Count - 1].id;
            listBirds[listBirds.Count - 1].GetComponent<Bird>().MixStateIdleAndTouching(10f);
            listBirdMove.Add(listBirds[listBirds.Count - 1]);
            for (int i = CountBirds - 1; i >= 0; i--)
            {
                if (IdFistBird == listBirds[i].id)
                {
                    listBirdMove.Add(listBirds[i]);
                    listBirds[i].GetComponent<Bird>().MixStateIdleAndTouching(10f);
                }
                else
                {
                    break;
                }
            }
        }
    }
    public void UnTouching()
    {
        if(listBirds.Count!=0)
        {
            int CountBirds = listBirds.Count - 1;
            int IdFistBird = listBirds[listBirds.Count - 1].id;
            listBirds[listBirds.Count - 1].GetComponent<Bird>().StateIdle();
            for (int i = CountBirds - 1; i >= 0; i--)
            {
                if (IdFistBird == listBirds[i].id)
                {
                    listBirds[i].GetComponent<Bird>().StateIdle();
                }
                else
                {
                    break;
                }
            }
        }
     
    }

    public void ClearBirdMove()
    {
        listBirdMove.Clear();
    }
    public List<Vector3> PositionSlotAvailable(int CountBirdMove)
    {  
        List<Vector3> PositionSlots = new List<Vector3>();
        for (int i = listBirds.Count- CountBirdMove; i<4;i++)
        {
            PositionSlots.Add(allSlots[i].transform.position);
        }

        return PositionSlots;
    }
    public List<int> ListIdSlotAvailable(int CountBirdMove)
    {
        List<int> ListSlots = new List<int>();
        for (int i = listBirds.Count - CountBirdMove; i < 4; i++)
        {
            ListSlots.Add(i);
        }

        return ListSlots;

    }
    public void StateShaky()
    {
        _animator.SetBool("Shaky", true);
        
    }
    public void StateIdle()
    {
        _animator.SetBool("Shaky", false);

    }
    public void Renew()
    {
        StateIdle();
        id = 0;
        listBirdMove.Clear();
        listBirds.Clear();
        ObjectPooler._instance.AddElement("Branch", gameObject);
        gameObject.transform.parent = ObjectPooler._instance.transform;
        gameObject.SetActive(false);
    }
    public void MoveALlBirdToALlSlot()
    {
      StartCoroutine(FadeMoveAllBirdToAllSlot());

    }
    IEnumerator FadeMoveAllBirdToAllSlot()
    {
        float TimeWaitBridMove = 0f;
        for (int j = 0; j < listBirds.Count; j++)
        {    
            listBirds[j].transform.parent= _animator.gameObject.transform; 
            Vector3 RealPosBird = listBirds[j].RealPosBird;
            listBirds[j].MoveToOnScreen(RealPosBird);
            yield return new WaitForSeconds(Random.RandomRange(0.07f, 0.15f));
           
            if (j== listBirds.Count-1)
            {
                TimeWaitBridMove = CalculerTimeWait(listBirds[j].id);
                AllBirdsTouchBranch(TimeWaitBridMove-0.1f);
            }
        }
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
    public void AllBirdsTouchBranch(float TimeWaitBridMove)
    {
        StartCoroutine(WaitTimeTouchBranch(TimeWaitBridMove));
    }
    IEnumerator WaitTimeTouchBranch(float TimeWaitBridMove)
    {
        yield return new WaitForSeconds(TimeWaitBridMove+0.1f);
        StateShaky();
        yield return new WaitForSeconds(0.15f);
        StateIdle();
    }
    public bool IsFullSameBirdsOnBranch()
    {
        if(listBirds.Count!=0)
        {
            int CountSameBird = 0;
            int IdFistBird = listBirds[0].id;
            for (int i = 0; i < listBirds.Count; i++)
            {
                if (IdFistBird == listBirds[i].id)
                {
                    CountSameBird++;
                }
                else
                {
                    break;
                }
            }
            return (CountSameBird == 4) ? true : false;
        }
        else
        {
            return false;
        }
    }
    public void MoveAllBirdToOutScreen()
    {
        StartCoroutine(FadeMoveBirdToOutScreen());
    }
    public bool CanChangeSeats()
    {
        if(listBirds.Count!=0)
        {
            int FristIdBird = listBirds[0].id;
            for (int i = 0; i < listBirds.Count; i++)
            {
                if (FristIdBird != listBirds[i].id)
                {
                    return true;
                }
            }
            return false;
        }
        else {
            return false;
        }
    }
    
  public  void SetOrderBirdsAndBrands(int OrderLayer)
    {
        // int OrderLayer = 20;
        if(listBirds.Count!=0)
        {
            _spriteRenderer.sortingOrder = OrderLayer+1;
            for (int i = 0; i < listBirds.Count; i++)
            {
                OrderLayer++;
                listBirds[i].SetOrderLayer(OrderLayer);
            }
        }
    }
    IEnumerator FadeMoveBirdToOutScreen()
    {
        List<Bird> ListFakeBirds = new List<Bird>();
        ListFakeBirds.AddRange(listBirds);

        for(int i=0;i<listBirds.Count;i++)
        {
            listBirds[i].SetOrderLayer(40);
        }
        listBirds.Clear();
        yield return new WaitForSeconds(0.2f);
        Vector3 NewPosOutScreen = new Vector3(Random.RandomRange(posOutScreen.x, posOutScreen.x + 1f), posOutScreen.y, 0f);

        ListFakeBirds[1].StateFly();
        ListFakeBirds[1].transform.DOMove(posOutScreen, 1f);
        yield return new WaitForSeconds(Random.RandomRange(0.05f, 0.1f));

        ListFakeBirds[2].StateFly();
        ListFakeBirds[2].transform.DOMove(posOutScreen, 1f);
        yield return new WaitForSeconds(Random.RandomRange(0.05f, 0.1f));

        ListFakeBirds[0].StateFly();
        ListFakeBirds[0].transform.DOMove(posOutScreen, 1f);
        yield return new WaitForSeconds(Random.RandomRange(0.05f, 0.1f));

        ListFakeBirds[3].StateFly();
        ListFakeBirds[3].transform.DOMove(posOutScreen, 1f);
        yield return new WaitForSeconds(Random.RandomRange(0.05f, 0.1f));


        for (int i = 0; i < ListFakeBirds.Count; i++)
        {
            ListFakeBirds[i].transform.parent = null;
        }
   
    }
    private void OnMouseDown()
    {
        if (GameManager._instance.gameState == GameManager.GameState.ChangeSeatsBirds&& CanChangeSeats())
        {
            UnTouching();
            ClearBirdMove();
            ChangeSeats();
        }
    }
    public void ChangeSeats()
    {
        List<Bird> ListFakeBirds = new List<Bird>();
        ListFakeBirds.AddRange(listBirds);
        listBirds.Clear();

        int[] NumberBirds = new int[20];
        for (int i = 0; i < ListFakeBirds.Count; i++)
        {
            NumberBirds[ListFakeBirds[i].id]++;
        }

        List<int> ListID = new List<int>();

        for(int i=0;i<NumberBirds.Length;i++)
        {
            if(NumberBirds[i]!=0)
            {
                ListID.Add(i);
            }
        }

        List<List<Bird>> ListListBirds = new List<List<Bird>>();
        for (int i = 0; i < ListID.Count; i++)
        {
            List<Bird> smallListBird = new List<Bird>();
            for (int j = 0; j < ListFakeBirds.Count; j++)
            {
                if(ListFakeBirds[j].id==ListID[i])
                {
                    smallListBird.Add(ListFakeBirds[j]);
                }
            }
            ListListBirds.Add(smallListBird);
        }

        if (ListListBirds.Count == 2)
        {
            if(ListFakeBirds[0].id== ListListBirds[0][0].id)
            {
                for (int i = ListListBirds.Count-1; i >= 0 ; i--)
                {
                    for (int j = 0; j < ListListBirds[i].Count; j++)
                    {
                        listBirds.Add(ListListBirds[i][j]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < ListListBirds.Count; i++)
                {
                    for (int j = 0; j < ListListBirds[i].Count; j++)
                    {
                        listBirds.Add(ListListBirds[i][j]);
                    }
                }
            }
        }
        else if(ListListBirds.Count==3)
        {
            int max = 0;
            int IdMax = 0;
            for (int i = 0; i < NumberBirds.Length; i++)
            {
                if (NumberBirds[i] > max)
                {
                    IdMax = i;
                    max = NumberBirds[i];
                }
            }
            int CountListBirds = ListFakeBirds.Count;

            if(max==1)
            {
                if (Random.RandomRange(0, 2) == 1)
                {
                    listBirds.Add(ListFakeBirds[2]);
                    listBirds.Add(ListFakeBirds[0]);
                    listBirds.Add(ListFakeBirds[1]);
                }
                else
                {
                    listBirds.Add(ListFakeBirds[0]);
                    listBirds.Add(ListFakeBirds[2]);
                    listBirds.Add(ListFakeBirds[1]);
                }
            }
            else
            {
                if (ListFakeBirds[CountListBirds - 1].id != IdMax || ListFakeBirds[CountListBirds - 1].id == IdMax && ListFakeBirds[CountListBirds - 2].id != IdMax)
                {
                    List<Bird> ListLastBird = new List<Bird>();
                    for (int i = 0; i < ListFakeBirds.Count; i++)
                    {
                        if (IdMax == ListFakeBirds[i].id)
                        {
                            ListLastBird.Add(ListFakeBirds[i]);
                        }
                        else
                        {
                            listBirds.Add(ListFakeBirds[i]);
                        }
                    }
                    foreach (Bird bird in ListLastBird)
                    {
                        listBirds.Add(bird);
                    }
                }
                else
                {
                    List<Bird> ListLastBird = new List<Bird>();

                    for (int i = 0; i < ListFakeBirds.Count; i++)
                    {
                        if (IdMax != ListFakeBirds[i].id)
                        {
                            ListLastBird.Add(ListFakeBirds[i]);
                        }
                        else
                        {
                            listBirds.Add(ListFakeBirds[i]);
                        }
                    }

                    foreach (Bird bird in ListLastBird)
                    {
                        listBirds.Add(bird);
                    }

                    List<Bird> ListBirds3ID = new List<Bird>();
                    ListBirds3ID.AddRange(listBirds);
                    listBirds.Clear();
                    listBirds.Add(ListBirds3ID[0]);
                    listBirds.Add(ListBirds3ID[1]);

                    if (Random.RandomRange(0, 2) == 1)
                    {
                        listBirds.Add(ListBirds3ID[2]);
                        listBirds.Add(ListBirds3ID[3]);
                    }
                    else
                    {
                        listBirds.Add(ListBirds3ID[3]);
                        listBirds.Add(ListBirds3ID[2]);
                    }
                }
            }
 
        }
        else if(ListListBirds.Count==4)
        {
            int a = Random.RandomRange(0, 3);
            if(a==0)
            {
                for(int i=3;i>=0;i--)
                {
                    listBirds.Add(ListFakeBirds[i]);
                }
            }
            else if (a == 1)
            {
                listBirds.Add(ListFakeBirds[3]);
                listBirds.Add(ListFakeBirds[0]);
                listBirds.Add(ListFakeBirds[2]);
                listBirds.Add(ListFakeBirds[1]);
            }else if(a==2)
            {
                listBirds.Add(ListFakeBirds[0]);
                listBirds.Add(ListFakeBirds[3]);
                listBirds.Add(ListFakeBirds[1]);
                listBirds.Add(ListFakeBirds[2]);
            }
        }


        SetOrderBirdsAndBrands(20);

        List<BirdUndo> ListBirdUndo = new List<BirdUndo>();

        for (int i = 0; i < listBirds.Count; i++)
        {
           listBirds[i].ParentObj = _animator.gameObject;
            listBirds[i].idBranchStand = id - 1;
            listBirds[i].TimeMove = 0.3f;
            listBirds[i].isMoveCurve = false;
            listBirds[i].isMoveToSlot = false;
            listBirds[i].ChangeSeats(allSlots[i].transform.position, false);


            Vector3 PosOldSlot = listBirds[i].transform.position;
            BirdUndo BirdUndos = new BirdUndo(id, listBirds[i], PosOldSlot, id, true);
            ListBirdUndo.Add(BirdUndos);
        }

        StateUndo NewStateUndo = new StateUndo(ListBirdUndo, ListFakeBirds);
        GameManager._instance.StackStateUndos.Push(NewStateUndo);
        StartCoroutine(WaitTimeChangeSeats());
        StartCoroutine(WaitTimeChangeStateWhenChangeSeats());
    }
    IEnumerator WaitTimeChangeStateWhenChangeSeats()
    {
        yield return new WaitForSeconds(0.5f);
        StateShaky();
        yield return new WaitForSeconds(0.2f);
        StateIdle();
    }
    IEnumerator WaitTimeChangeSeats()
    {
        yield return new WaitForSeconds(0.54f);
        GameManager._instance.gameState = GameManager.GameState.SortBirds;
        UiGamePlay._instance.DisableChangeSeats();
    }
    public void MoveUp()
    {
        Vector3 Target = new Vector3(transform.position.x, transform.position.y+0.9f, 0);
        transform.DOMove(Target, 0.3f);
    }
}
