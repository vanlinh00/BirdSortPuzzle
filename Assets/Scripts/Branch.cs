using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{   
    public int id;
    public List<GameObject> allSlots = new List<GameObject>();
    public List<Bird> birds = new List<Bird>();
    public List<Bird> listBirdMove = new List<Bird>();

    public void AddToListBrids(Bird bird)
    {
        birds.Add(bird);
    }
    public Vector3 GetPosSlot(int Number)
    {
        return allSlots[Number].transform.position;
    }
    public void RemoveBirdPromListBird(int CountBirdMoved)
    { 
        int index = birds.Count - CountBirdMoved;
        for (int i = birds.Count - 1; i >= index; i--)
        {
            birds.RemoveAt(i);
        }
    }
    public void Touching()
    {
        int CountBirds = birds.Count - 1;
        int IdFistBird = birds[birds.Count - 1].id;
        birds[birds.Count - 1].GetComponent<Bird>().Statetouching();
        listBirdMove.Add(birds[birds.Count - 1]);
        for (int i=CountBirds-1;i>=0;i--)
        {
            if(IdFistBird==birds[i].id)
            {
                listBirdMove.Add(birds[i]);
                birds[i].GetComponent<Bird>().Statetouching();
            }
            else
            {
                break;
            }
        }
    }
    public void UnTouching()
    {
        int CountBirds = birds.Count - 1;
        int IdFistBird = birds[birds.Count - 1].id;
        birds[birds.Count - 1].GetComponent<Bird>().StateIdle();
        for (int i = CountBirds - 1; i >= 0; i--)
        {
            if (IdFistBird == birds[i].id)
            {
                birds[i].GetComponent<Bird>().StateIdle();
            }
            else
            {
                break;
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
        for (int i = birds.Count; i<4;i++)
        {
            PositionSlots.Add(allSlots[i].transform.position);
        }

        return PositionSlots;
    }
    public void Renew()
    {
        id = 0;
        allSlots.Clear();
        birds.Clear();
        listBirdMove.Clear();
    }
}
