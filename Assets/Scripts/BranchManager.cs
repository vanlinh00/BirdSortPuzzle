using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchManager : MonoBehaviour
{
    [SerializeField] DataBirdOnBranchs _dataBirdOnBranchs = new DataBirdOnBranchs();
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
    public void LoadDataBirdOnBranchs(DataBirdOnBranchs DataBirdOnBranchs)
    {
        _dataBirdOnBranchs = DataBirdOnBranchs;
    }
    public List<Branch>  BonrAllBirdOnBranch()
    {
        for (int i = 0; i < _dataBirdOnBranchs.dataBirdOnBranch.Count; i++)
        {
            DataBirdOnBranch data = _dataBirdOnBranchs.dataBirdOnBranch[i];
            Vector3 RealPosBird = ListAllBranchs[data.idBranch - 1].GetPosSlot(data.slotBird - 1);
            GameObject Bird;

            if (data.idBranch % 2 != 0)
            {
                Vector3 StartPosBird = new Vector3(RealPosBird.x - 2.5f, RealPosBird.y + 1.25f, 0);
                Bird = ObjectPooler._instance.SpawnFromPool("Bird" + data.idBird, StartPosBird, Quaternion.identity);
                Bird.GetComponent<Bird>().FlipX();
            }
            else
            {
                Vector3 StartPosBird = new Vector3(RealPosBird.x + 2.5f, RealPosBird.y + 1.25f, 0);
                Bird = ObjectPooler._instance.SpawnFromPool("Bird" + data.idBird, StartPosBird, Quaternion.identity);
            }
            Bird.GetComponent<Bird>().RealPosBird = RealPosBird;
            ListAllBranchs[data.idBranch - 1].AddToListBrids(Bird.GetComponent<Bird>());
        }
        return ListAllBranchs;
    }
  public void MoveAllBirdSToAllBranchs()
    {
        for (int i = 0; i < ListAllBranchs.Count; i++)
        {
            ListAllBranchs[i].MoveALlBirdToALlSlot();
        }
    }
    public void LoadAllBranchLevel(int AmountBranch)
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
        if (ListAllBranchs[IdBranch2].birds.Count == 4|| ListAllBranchs[IdBranch1].birds.Count == 0)
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
    public void MoveToScreen()
    {
        StartCoroutine(Move(_branchRightManager.transform, new Vector3(0f, 0f, 0f), 0.4f));
        StartCoroutine(Move(_branchLeftManger.transform, new Vector3(0f, 0f, 0f), 0.4f));
    }
    IEnumerator Move(Transform CurrentTransform, Vector3 Target, float TotalTime)
    {
        var passed = 0f;
        var init = CurrentTransform.transform.position;
        while (passed < TotalTime)
        {
            passed += Time.deltaTime;
            var normalized = passed / TotalTime;
            var current = Vector3.Lerp(init, Target, normalized);
            CurrentTransform.position = current;
            yield return null;
        }

    }

}
