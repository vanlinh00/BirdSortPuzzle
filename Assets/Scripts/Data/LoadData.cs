using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : Singleton<LoadData>
{
    DataBirdOnBranchs DataBirdOnBranchsJson = new DataBirdOnBranchs();


    protected override void Awake()
    {
        base.Awake();
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
        return DataBirdOnBranchsJson;
    }

    
    public void Renew()
    {
        //  dataBirdOnBranchs.AmountBranch = 0;
       // DataBirdOnBranchsJson.BirdOnBranch.Clear();
    }
    public void Loadata(int idType, int IdItem)
    { 
        if(idType==1)
        {
            LoadDataBackGround(IdItem);
        }
        else if(idType==2)
        {
            LoadDataBirds(IdItem);
        }
        else if(idType==3)
        {
            LoadDataBranch(IdItem);
        }
     }
    public void LoadDataBackGround(int IdBg)
    {
        DataPlayer.ChangeBackGroundLoading(IdBg);
        BackGroundsManager._instance.LoadBackGround();
    }
    public void LoadDataBranch(int IdBranch)
    {
        var BranchSprite  = Resources.Load<Sprite>("Shop/Branchs/Branch" + IdBranch);
        BranchManager._instance.ChangeSpriteAllBranchs(BranchSprite);
        DataPlayer.ChangeBranchLoading(IdBranch);
    }
    public void LoadDataBirds(int IdSkinBird)
    {
        BranchManager._instance.ChangeSkinAllBrids(IdSkinBird);
        DataPlayer.ChangeBirdLoading(IdSkinBird);
    }
}


