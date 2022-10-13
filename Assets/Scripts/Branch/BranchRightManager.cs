using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchRightManager : MonoBehaviour
{
    int CountBrach = 1;
    public Branch BonrNewBranch()
    {
        GameObject BrachRight=null;
        Vector3 PoslastChild = new Vector3(0,0,0);
        if (transform.childCount!=0)
        {
            GameObject lastChildBranch = transform.GetChild(transform.childCount - 1).gameObject;
            PoslastChild = new Vector3(lastChildBranch.transform.localPosition.x, lastChildBranch.transform.localPosition.y - 1.74f, 0);
            BrachRight = ObjectPooler._instance.SpawnFromPool("BranchRight", new Vector3(-100, -100, 0), Quaternion.identity);
            BrachRight.GetComponent<Branch>().id = CountBrach + 2;
            CountBrach += 2;
        }
        else
        {
            PoslastChild = new Vector3(-2.71f, 2.04f, 0);
            BrachRight = ObjectPooler._instance.SpawnFromPool("BranchRight", new Vector3(-100, -100, 0), Quaternion.identity);
            BrachRight.GetComponent<Branch>().id = CountBrach;
        }
        BrachRight.transform.parent = transform;
        BrachRight.transform.localPosition = PoslastChild;
        return BrachRight.GetComponent<Branch>();
    }

    //public void Renew()
    //{

    //}
}
