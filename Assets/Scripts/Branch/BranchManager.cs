using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchManager : MonoBehaviour
{
    [SerializeField] DataBirdOnBranchs _dataBirdOnBranchs = new DataBirdOnBranchs();
    [SerializeField]  List<Branch> _listAllBranchs = new List<Branch>();
    [SerializeField] BranchRightManager _branchRightManager;
    [SerializeField] BranchLeftManger _branchLeftManger;
    [SerializeField] List<Bird> _listBirds = new List<Bird>();

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
            Vector3 RealPosBird = _listAllBranchs[data.idBranch - 1].GetPosSlot(data.slotBird - 1);
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
            _listBirds.Add(Bird.GetComponent<Bird>());
            _listAllBranchs[data.idBranch - 1].AddToListBrids(Bird.GetComponent<Bird>());
        }
        return _listAllBranchs;
    }
  public void MoveAllBirdSToAllBranchs()
    {
        for (int i = 0; i < _listAllBranchs.Count; i++)
        {
            _listAllBranchs[i].MoveALlBirdToALlSlot();
        }
    }
    public void LoadAllBranchLevel()
    {
        //DataLevel employeesInJson = new DataLevel();
        //employeesInJson = JsonUtility.FromJson<DataLevel>(jsonFile.text);
        //Debug.Log(employeesInJson.AmountBranch);
    
        for (int i = 1; i <= _dataBirdOnBranchs.AmountBranch; i++)
        {
            if (i % 2 != 0)
            {
                _listAllBranchs.Add(_branchRightManager.BonrNewBranch());
            }
            else
            {
                _listAllBranchs.Add(_branchLeftManger.BonrNewBranch());
            }

        }
    }

    public bool IsSameIdBird(int IdCurrentBranch,int IdNextBranch)
    {
        if (_listAllBranchs[IdNextBranch].listBirds.Count == 4|| _listAllBranchs[IdCurrentBranch].listBirds.Count == 0)
        {
            return false;
        }
        else
        {
            if (_listAllBranchs[IdNextBranch].listBirds.Count == 0)
            {
                return true;
            }
            else
            {
                Bird firstBirdBranch1 = _listAllBranchs[IdCurrentBranch].listBirds[_listAllBranchs[IdCurrentBranch].listBirds.Count - 1];
                Bird firstBirdBranh2 = _listAllBranchs[IdNextBranch].listBirds[_listAllBranchs[IdNextBranch].listBirds.Count - 1];
                return (firstBirdBranch1.id == firstBirdBranh2.id) ? true : false;
            }

        }
    }
    public void RenewAllBranchs()
    {
        for (int i=0;i<_listAllBranchs.Count;i++)
        {
            _listAllBranchs[i].Renew();
        }
        _branchLeftManger.Renew();
        _branchRightManager.Renew();
    }
    public void RenewAllBirds()
    {
        for (int i = 0; i < _listBirds.Count; i++)
        {
            _listBirds[i].Renew();
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
