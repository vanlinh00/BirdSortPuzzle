using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTut : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
  public void Init(Vector3 PosHand)
    {
        transform.position = PosHand;
    }

}