using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour
{
    DataBirdOnBranchs DataBirdOnBranchsJson = new DataBirdOnBranchs();
    private void Awake()
    {
    }

    public DataBirdOnBranchs LoadDataLevel(int level)
    {
        TextAsset jsonFile;
        if (level<=11)
        {
            jsonFile = Resources.Load("Level/Level" + level) as TextAsset;
        }
        else
        {
            jsonFile = Resources.Load("Level/lvl" + level) as TextAsset;
        }
   

        DataBirdOnBranchsJson = JsonUtility.FromJson<DataBirdOnBranchs>(jsonFile.text);
        Debug.Log(DataBirdOnBranchsJson.AmountBranch);
        foreach (BirdOnBranch dataBirdOnBranch in DataBirdOnBranchsJson.BirdOnBranch)
        {
            Debug.Log(dataBirdOnBranch.idBranch);
        }
        return DataBirdOnBranchsJson;

    }
    
    public void Renew()
    {
      //  dataBirdOnBranchs.AmountBranch = 0;
       // dataBirdOnBranchs.dataBirdOnBranch.Clear();
    }
}


