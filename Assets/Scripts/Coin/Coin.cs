using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Coin : MonoBehaviour
{
    public void MoveToTarget(Vector3 Target)
    {
        transform.DOMove(Target, 0.9f);
        StartCoroutine(WaitTimeMove());
    }
    IEnumerator WaitTimeMove()
    {
        yield return new WaitForSeconds(1f);
    }
}
