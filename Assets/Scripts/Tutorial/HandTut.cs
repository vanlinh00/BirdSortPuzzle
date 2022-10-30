using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTut : MonoBehaviour
{
  public void Init(Vector3 PosHand)
    {
        transform.position = PosHand;
    }

}