using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchRightManager : MonoBehaviour
{
    int CountBrach = 1;
    public Branch BonrNewBranch()
    {
        GameObject BrachRight = ObjectPooler._instance.SpawnFromPool("Branch", new Vector3(-100, -100, 0),Quaternion.identity);
        Vector3 PoslastChild = new Vector3(0,0,0);

        if (transform.childCount!=0)
        {
            GameObject lastChildBranch = transform.GetChild(transform.childCount - 1).gameObject;
            PoslastChild = new Vector3(lastChildBranch.transform.localPosition.x, lastChildBranch.transform.localPosition.y - 1.50f, 0);
            BrachRight.GetComponent<Branch>().id = CountBrach + 2;
            CountBrach += 2;
        }
        else
        {
            PoslastChild = new Vector3(-2.71f, 2.04f, 0);
            BrachRight.GetComponent<Branch>().id = CountBrach;
        }

        Vector3 PosOutScreen = new Vector3(1.8f, 6.27f, 0);
        BrachRight.GetComponent<Branch>().posOutScreen = PosOutScreen;
        BrachRight.transform.parent = transform;
        BrachRight.transform.localPosition = PoslastChild;
        return BrachRight.GetComponent<Branch>();
    }
    public void Renew()
    {
        transform.position = new Vector3(-2.5f, 0, 0f);
        CountBrach = 1;
    }
    //public void ReSetBranchs()
    //{
    //    GameObject Branch;
    //    int NumChild = transform.childCount;
    //    for (int i = 0; i < NumChild; i++)
    //     {
    //            Branch = transform.GetChild(0).gameObject;
    //           // Branch.SetActive(false);
    //            ObjectPooler._instance.AddElement("Branch", Branch);
    //            Branch.transform.parent = ObjectPooler._instance.transform;
    //     }
    //}

}
