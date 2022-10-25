using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateUndo :MonoBehaviour
{
    public List<BirdUndo> listStateBirdUndo = new List<BirdUndo>();
    public List<Bird> listBirdsChangeState = new List<Bird>();
    public StateUndo(List<BirdUndo> ListStateBirdUndo,List<Bird> ListBirdsChangeState)
    {
        listStateBirdUndo = ListStateBirdUndo;
        listBirdsChangeState = ListBirdsChangeState;
    }
}
public class BirdUndo
{
    public int idOldBranch;
    public int idSlot;
    public int idNextBranch;
    public Vector3 posOldSlot;
    public Bird bird;
    public bool isChangeSeats;
    public BirdUndo(int IdBranch, Bird Bird,Vector3 IdOldSlot,int IdNextBranch, bool IsChangeSeats, int IdSlot)
    {
        idOldBranch = IdBranch;
        bird = Bird;
        posOldSlot = IdOldSlot;
        idNextBranch = IdNextBranch;
        isChangeSeats = IsChangeSeats;
        idSlot = IdSlot;
    }
}
