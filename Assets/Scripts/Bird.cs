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
    public void MoveToTarget(Vector3 Target, bool IsFlipX)
    {   
        StateFly();
        StartCoroutine(Move(transform, Target, 0.54f));
        StartCoroutine(WaitTimeChangeState(IsFlipX));
    }
    IEnumerator WaitTimeChangeState(bool IsFlipX)
    {
        yield return new WaitForSeconds(0.54f);

        if(IsFlipX)
        {
            FlipX();
        }
        yield return new WaitForSeconds(0.1f);
        StateGrounding();
        yield return new WaitForSeconds(0.5f);
        StateIdle();
    }
    public void FlipX()
    {
        if (_skeletonAnimation.skeleton.FlipX)
        {
            _skeletonAnimation.skeleton.FlipX = false;
        }
        else
        {
            _skeletonAnimation.skeleton.FlipX = true;
        }
    }
    public void MoveToOnScreen(Vector3 RealPosBird)
    {
        StateFly();
        StartCoroutine(Move(transform, RealPosBird, 1f));
        StartCoroutine(WaitTimeChangeStateWhenStartGame());
    }
    IEnumerator WaitTimeChangeStateWhenStartGame()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitForSeconds(0.1f);
        StateGrounding();
        yield return new WaitForSeconds(0.4f);
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
