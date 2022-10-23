using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    [SerializeField] HandTut _handTut;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveHandToTarget(Vector3 Target)
    {
        _handTut.gameObject.SetActive(true);
        _handTut.Init(Target);
    }
    public void SetActiveHand(bool Res)
    {
        _handTut.gameObject.SetActive(Res);
    }
}
