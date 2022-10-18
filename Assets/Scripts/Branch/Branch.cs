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
    public List<Vector3> PositionSlotAvailable()
    {  
        List<Vector3> PositionSlots = new List<Vector3>();
        for (int i = listBirds.Count; i<4;i++)
        {
            PositionSlots.Add(allSlots[i].transform.position);
        }

        return PositionSlots;
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
    IEnumerator FadeMoveBirdToOutScreen()
    {
        Vector3 NewPosOutScreen = new Vector3(Random.RandomRange(posOutScreen.x, posOutScreen.x + 1f), posOutScreen.y, 0f);

        listBirds[1].StateFly();
        listBirds[1].transform.DOMove(posOutScreen, 1f);
        yield return new WaitForSeconds(Random.RandomRange(0.05f, 0.1f));

        listBirds[2].StateFly();
        listBirds[2].transform.DOMove(posOutScreen, 1f);
        yield return new WaitForSeconds(Random.RandomRange(0.05f, 0.1f));

        listBirds[0].StateFly();
        listBirds[0].transform.DOMove(posOutScreen, 1f);
        yield return new WaitForSeconds(Random.RandomRange(0.05f, 0.1f));

        listBirds[3].StateFly();
        listBirds[3].transform.DOMove(posOutScreen, 1f);
        yield return new WaitForSeconds(Random.RandomRange(0.05f, 0.1f));

        for(int i=0;i<listBirds.Count;i++)
        {
            listBirds[i].transform.parent = null;
        }

        listBirds.Clear();
        listBirdMove.Clear();
    }
    private void OnMouseDown()
    {
        if (GameManager._instance.gameState == GameManager.GameState.ChangeSeatsBirds)
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


        List<int> ListNumberBird = new List<int>();

        int[] a = new int[20];
        for (int i=0;i< ListFakeBirds.Count-1;i++)
        {
            for (int j=i+1;j<ListFakeBirds.Count;j++)
            {
                if(ListFakeBirds[i].id==ListFakeBirds[j].id)
                {
                    a[ListFakeBirds[i].id]++;
                }
            }
        }

        int max = 0;
        int IdMax = 0;
        for(int i=0;i<a.Length;i++ )
        {
            if(a[i]>max)
            {
                IdMax = i;
                max = a[i];
            }
        }

        List<Bird> ListLastBird = new List<Bird>();
        for(int i=0;i<ListFakeBirds.Count; i++)
        {
            if(IdMax== ListFakeBirds[i].id)
            {
                ListLastBird.Add(ListFakeBirds[i]);
            }
            else
            {
                listBirds.Add(ListFakeBirds[i]);
            }
        }

        foreach(Bird bird in ListLastBird)
        {
            listBirds.Add(bird);
        }

        List<BirdUndo> ListBirdUndo = new List<BirdUndo>();
        for (int i = 0; i < listBirds.Count; i++)
        {
            listBirds[i].ParentObj = _animator.gameObject;
            listBirds[i].ChangeSeats(allSlots[i].transform.position, false, 0.5f);
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
    }
}
