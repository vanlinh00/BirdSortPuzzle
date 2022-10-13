using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchManager : MonoBehaviour
{
    public TextAsset jsonFile;
    DataBirdOnBranchs dataBirdOnBranchs = new DataBirdOnBranchs();
    [SerializeField]  List<Branch> ListAllBranchs = new List<Branch>();

    [SerializeField] BranchRightManager _branchRightManager;
    [SerializeField] BranchLeftManger _branchLeftManger;

    //{
    //    "AmountBranch": 2,
    //    "BirdOnBranch": [
    //         {"id": 1,"idBird":9,"idBranch":1,"posBird":1},
    //        { "id": 2,"idBird":9,"idBranch":1,"posBird":2},
    //        { "id": 3,"idBird":9,"idBranch":2,"posBird":1},
    //        { "id": 4,"idBird":9,"idBranch":2,"posBird":2}
    //    ]
    //}


    public void LoadDataLevel(int level)
    {
        //DataBirdOnBranchs DataBirdOnBranchsJson = JsonUtility.FromJson<DataBirdOnBranchs>(jsonFile.text);

        //foreach (DataBirdOnBranch dataBirdOnBranch in DataBirdOnBranchsJson.dataBirdOnBranch)
        //{
        //    Debug.Log(dataBirdOnBranch.id);
        //}


        //   "AmountBranch": 3   
        if (level == 1)
        {
            DataBirdOnBranch dataBirdOnBranch = new DataBirdOnBranch(1, 1, 1, 1);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch);


            DataBirdOnBranch dataBirdOnBranch2 = new DataBirdOnBranch(2, 1, 1, 2);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch2);


            DataBirdOnBranch dataBirdOnBranch3 = new DataBirdOnBranch(3, 1, 2, 1);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch3);

            DataBirdOnBranch dataBirdOnBranch4 = new DataBirdOnBranch(4, 1, 2, 2);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch4);
        }
        if (level==2)
        {

            DataBirdOnBranch dataBirdOnBranch = new DataBirdOnBranch(1, 1, 1, 1);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch);


            DataBirdOnBranch dataBirdOnBranch2 = new DataBirdOnBranch(2, 1, 1, 2);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch2);


            DataBirdOnBranch dataBirdOnBranch3 = new DataBirdOnBranch(3, 2, 1, 3);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch3);

            DataBirdOnBranch dataBirdOnBranch4 = new DataBirdOnBranch(4, 2, 2, 1);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch4);


            DataBirdOnBranch dataBirdOnBranch5 = new DataBirdOnBranch(5, 2, 2, 2);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch5);

            DataBirdOnBranch dataBirdOnBranch6 = new DataBirdOnBranch(6, 2, 2, 3);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch6);


            DataBirdOnBranch dataBirdOnBranch7 = new DataBirdOnBranch(7, 1, 3, 1);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch7);

            DataBirdOnBranch dataBirdOnBranch8 = new DataBirdOnBranch(8, 1, 3, 2);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch8);
        }
        if(level==3)
        {

            // "branch": 5,
            DataBirdOnBranch dataBirdOnBranch = new DataBirdOnBranch(1, 3, 1, 1);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch);


            DataBirdOnBranch dataBirdOnBranch2 = new DataBirdOnBranch(2, 1, 1, 2);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch2);


            DataBirdOnBranch dataBirdOnBranch3 = new DataBirdOnBranch(3, 2, 1, 3);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch3);


            DataBirdOnBranch dataBirdOnBranch4 = new DataBirdOnBranch(4, 2, 1, 4);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch4);


            DataBirdOnBranch dataBirdOnBranch5 = new DataBirdOnBranch(5, 1, 2, 1);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch5);

            DataBirdOnBranch dataBirdOnBranch6 = new DataBirdOnBranch(6, 2, 2, 2);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch6);


            DataBirdOnBranch dataBirdOnBranch7 = new DataBirdOnBranch(7, 1, 2, 3);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch7);

            DataBirdOnBranch dataBirdOnBranch8 = new DataBirdOnBranch(8, 1, 2, 4);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch8);

            DataBirdOnBranch dataBirdOnBranch9 = new DataBirdOnBranch(9, 2, 3, 1);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch9);

            DataBirdOnBranch dataBirdOnBranch10 = new DataBirdOnBranch(10, 3, 3, 2);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch10);

            DataBirdOnBranch dataBirdOnBranch11 = new DataBirdOnBranch(11, 3, 3, 3);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch11);

            DataBirdOnBranch dataBirdOnBranch12 = new DataBirdOnBranch(12, 3, 3, 4);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch12);
        }


    }
 
    public List<Branch>  BonrAllBirdOnBranch()
    {  
        for (int i = 0; i < dataBirdOnBranchs.dataBirdOnBranch.Count; i++)
        {
            DataBirdOnBranch data = dataBirdOnBranchs.dataBirdOnBranch[i];

            Vector3 PosBird = ListAllBranchs[data.idBranch - 1].GetPosSlot(data.slotBird - 1);
            GameObject Bird;
            if (data.idBranch % 2 != 0)
            {
                Bird = ObjectPooler._instance.SpawnFromPool("Bird" + data.idBird, PosBird, new Quaternion(0f, 180f, 0f, 0f));
            }
            else
            {
                Bird = ObjectPooler._instance.SpawnFromPool("Bird" + data.idBird, PosBird, Quaternion.identity);
            }
            ListAllBranchs[data.idBranch - 1].AddToListBrids(Bird.GetComponent<Bird>());
        }
        return ListAllBranchs;
    }
    public void LoadAllBranchLeve(int AmountBranch)
    {
        //DataLevel employeesInJson = new DataLevel();
        //employeesInJson = JsonUtility.FromJson<DataLevel>(jsonFile.text);
        //Debug.Log(employeesInJson.AmountBranch);
    
        for (int i = 1; i <= AmountBranch; i++)
        {
            if (i % 2 != 0)
            {
                ListAllBranchs.Add(_branchRightManager.BonrNewBranch());
            }
            else
            {
                ListAllBranchs.Add(_branchLeftManger.BonrNewBranch());
            }

        }
    }
    public bool IsSameIdBird(int IdBranch1,int IdBranch2)
    {
        if (ListAllBranchs[IdBranch2].birds.Count == 4)
        {
            return false;
        }
        else
        {
            if (ListAllBranchs[IdBranch2].birds.Count == 0)
            {
                return true;
            }
            else
            {
                Bird firstBirdBranch1 = ListAllBranchs[IdBranch1].birds[ListAllBranchs[IdBranch1].birds.Count - 1];
                Bird firstBirdBranh2 = ListAllBranchs[IdBranch2].birds[ListAllBranchs[IdBranch2].birds.Count - 1];

                return (firstBirdBranch1.id == firstBirdBranh2.id) ? true : false;
            }

        }
    }
}
