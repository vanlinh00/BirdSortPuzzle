using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataLevel
{
    //{
    //    "AmountBranch": 2,
    //    "BirdOnBranch": [
    //         {"id": 1,"idBird":9,"idBranch":1,"posBird":1},
    //        { "id": 2,"idBird":9,"idBranch":1,"posBird":2},
    //        { "id": 3,"idBird":9,"idBranch":2,"posBird":1},
    //        { "id": 4,"idBird":9,"idBranch":2,"posBird":2}
    //    ]
    //}

public int AmountBranch;
}
public class DataBirdOnBranchs
{
    public List<DataBirdOnBranch> dataBirdOnBranch = new List<DataBirdOnBranch>();
}
public class DataBirdOnBranch
{
    public int id;
    public int idBird;
    public int idBranch;
    public int slotBird;

    public DataBirdOnBranch(int Id,int IdBird, int IdBranch,int SlotBird )
    {
        id = Id;
        idBird = IdBird;
        idBranch = IdBranch;
        slotBird = SlotBird;
    }
}
