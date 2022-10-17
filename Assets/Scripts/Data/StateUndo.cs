using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateUndo :MonoBehaviour
{
    public List<BirdUndo> listStateBirdUndo = new List<BirdUndo>();
    public StateUndo(List<BirdUndo> ListStateBirdUndo)
    {
        listStateBirdUndo = ListStateBirdUndo;
    }
}
public class BirdUndo
{
    public int idOldBranch;
    public int idNextBranch;
    public Vector3 posOldSlot;
    public Bird bird;
    public BirdUndo(int IdBranch, Bird Bird,Vector3 IdOldSlot,int IdNextBranch)
    {
        idOldBranch = IdBranch;
        bird = Bird;
        posOldSlot = IdOldSlot;
        idNextBranch = IdNextBranch;
    }
}
