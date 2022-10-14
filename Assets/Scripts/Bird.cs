using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public int id;
    [SerializeField] SkeletonAnimation _skeletonAnimation;
    public Vector3 RealPosBird;
    public void Start()
    {
        StateIdle();
    }
    public void StateFly()
    {
        _skeletonAnimation.AnimationName = "fly";
    }
    public void StateIdle()
    {
        _skeletonAnimation.AnimationName = "idle";
    }
    public void StateGrounding()
    {
        _skeletonAnimation.AnimationName = "grounding";
    }
    public void Statetouching()
    {
        _skeletonAnimation.AnimationName = "touching";
    }
    public void MoveToTarget(Vector3 Target)
    {
        StateFly();
        StartCoroutine(Move(transform, Target, 1f));
        StartCoroutine(WaitTimeChangeState());
    }
    IEnumerator WaitTimeChangeState()
    {
        yield return new WaitForSeconds(1f);
        if (transform.position.x < 0f)
        {
            transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }
        else
        {
            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        StateGrounding();
        yield return new WaitForSeconds(0.2f);
        StateIdle();
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
