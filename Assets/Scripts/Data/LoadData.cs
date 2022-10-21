using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    DataBirdOnBranchs dataBirdOnBranchs = new DataBirdOnBranchs();
    public TextAsset jsonFile;


    public DataBirdOnBranchs LoadDataLevel(int level)
    {
     
        //DataBirdOnBranchs DataBirdOnBranchsJson = JsonUtility.FromJson<DataBirdOnBranchs>(jsonFile.text);
        //foreach (DataBirdOnBranch dataBirdOnBranch in DataBirdOnBranchsJson.dataBirdOnBranch)
        //{
        //    Debug.Log(dataBirdOnBranch.id);
        //}
        //   "AmountBranch": 3   
        if (level == 1)
        {
            dataBirdOnBranchs.AmountBranch = 2;

            DataBirdOnBranch dataBirdOnBranch = new DataBirdOnBranch(1, 1, 1, 1);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch);


            DataBirdOnBranch dataBirdOnBranch2 = new DataBirdOnBranch(2, 1, 1, 2);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch2);


            DataBirdOnBranch dataBirdOnBranch3 = new DataBirdOnBranch(3, 1, 2, 1);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch3);

            DataBirdOnBranch dataBirdOnBranch4 = new DataBirdOnBranch(4, 1, 2, 2);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch4);
        }
        if (level == 2)
        {
            dataBirdOnBranchs.AmountBranch = 5;
            
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
        if (level == 3)
        {
            dataBirdOnBranchs.AmountBranch = 5; 
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
        
        }else if(level==4)
        {
            dataBirdOnBranchs.AmountBranch = 7;

            DataBirdOnBranch dataBirdOnBranch = new DataBirdOnBranch(1, 4, 1, 1);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch);

            DataBirdOnBranch dataBirdOnBranch2 = new DataBirdOnBranch(1, 6, 1, 2);
            dataBirdOnBranchs.dataBirdOnBranch.Add(dataBirdOnBranch2);


        }
        return dataBirdOnBranchs;

    }
    
    public void Renew()
    {
        dataBirdOnBranchs.AmountBranch = 0;
        dataBirdOnBranchs.dataBirdOnBranch.Clear();
    }
}


