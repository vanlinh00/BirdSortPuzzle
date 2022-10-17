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
        listBirds.RemoveAt(listBirds.Count - 1);
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
            listBirds[listBirds.Count - 1].GetComponent<Bird>().Statetouching();
            listBirdMove.Add(listBirds[listBirds.Count - 1]);
            for (int i = CountBirds - 1; i >= 0; i--)
            {
                if (IdFistBird == listBirds[i].id)
                {
                    listBirdMove.Add(listBirds[i]);
                    listBirds[i].GetComponent<Bird>().Statetouching();
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
                AllBirdsTouchBranch(TimeWaitBridMove);
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

        listBirds.Clear();
    }
    private void OnMouseDown()
    {
        if (GameManager._instance.gameState == GameManager.GameState.ChangeSeatsBirds)
        {
            ChangeSeats();
        }
    }
    public void ChangeSeats()
    {
        ///  1 2 3 4
        ///  1 4 2 3    // 4 lui ve so 2, 2 len 3 , 3 len 4 cung luc  => 1 du nguyen
        ///  1 3 4 2   // 2 len so 4, 3 lui ve so 2 , 4 lui ve so 3
        ///  2 4 3 1    // 1 len so 4, 2 lui ve so 1, 4 lui ve so 2   => 3 du nguyen

        List<Bird> ListFakeBirds = new List<Bird>();
        ListFakeBirds.AddRange(listBirds);
        listBirds.Clear();
        int Number = Random.RandomRange(1, 3);
        if (Number == 1)
        {
            if (ListFakeBirds.Count == 4)
            {
                listBirds.Add(ListFakeBirds[0]);
                listBirds.Add(ListFakeBirds[3]);
                listBirds.Add(ListFakeBirds[1]);
                listBirds.Add(ListFakeBirds[2]);
            }
        }
        if (Number == 2)
        {
            if (ListFakeBirds.Count == 4)
            {
                listBirds.Add(ListFakeBirds[1]);
                listBirds.Add(ListFakeBirds[3]);
                listBirds.Add(ListFakeBirds[2]);
                listBirds.Add(ListFakeBirds[0]);
            }
        }

        for (int i = 0; i < listBirds.Count; i++)
        {
            listBirds[i].ParentObj = _animator.gameObject;
            listBirds[i].MoveToTarget(allSlots[i].transform.position, false);
        }

        StartCoroutine(WaitTimeChangeSeats());
    }
    IEnumerator WaitTimeChangeSeats()
    {
        yield return new WaitForSeconds(0.54f);
        GameManager._instance.gameState = GameManager.GameState.SortBirds;
    }
}
