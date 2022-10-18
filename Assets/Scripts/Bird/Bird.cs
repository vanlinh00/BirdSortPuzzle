using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Spine;

public class Bird : MonoBehaviour
{
    public int id;
    [SerializeField] SkeletonAnimation _skeletonAnimation;
    public Vector3 RealPosBird;
    public GameObject ParentObj;
    public float TimeMove = 0.54f;
 
    public void Start()
    {
        StateIdle();
    }
    public void StateFly()
    {
        // _skeletonAnimation.AnimationName = "fly";
        _skeletonAnimation.AnimationState.SetAnimation(0, "fly", true);
        _skeletonAnimation.AnimationState.SetEmptyAnimation(1, 0);
    }
    public void StateIdle()
    {
      //  _skeletonAnimation.AnimationName = "idle";
       _skeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
        _skeletonAnimation.AnimationState.SetEmptyAnimation(1, 0);
    }
    public void StateGrounding()
    {
        // _skeletonAnimation.AnimationName = "grounding";

        _skeletonAnimation.AnimationState.SetAnimation(0, "grounding", true);
        _skeletonAnimation.AnimationState.SetEmptyAnimation(1, 0);

    }
    public void Statetouching()
    {
       //  _skeletonAnimation.AnimationName = "touching";
       //_skeletonAnimation.AnimationState.SetAnimation(1, "touching", true);
    }
    public void MoveToTarget(Vector3 Target, bool IsFlipX)
    {
        //  StateFly();
        MixStateFlyAndTouching();
        //  StartCoroutine(Move(transform, Target, 0.54f));
        transform.DOMove(Target, TimeMove);
        StartCoroutine(WaitTimeChangeState(IsFlipX, TimeMove));
    }
    public void ChangeSeats(Vector3 Target, bool IsFlipX,float TimeMove)
    {
        StateFly();
        transform.DOMove(Target, TimeMove);
       StartCoroutine(WaitTimeChangeState(IsFlipX, TimeMove));
    }
    IEnumerator WaitTimeChangeState(bool IsFlipX, float TimeMove)
    {
      
        yield return new WaitForSeconds(TimeMove-0.1f);
        if (IsFlipX)
        {
            FlipX();
        }
        StateGrounding();
        yield return new WaitForSeconds(0.2f);
        StateIdle();
        if (ParentObj != null)
        {
            transform.parent = ParentObj.transform;
        }
        else
        {
            Debug.Log(id);
        }
    }
    public void MixStateFlyAndTouching()
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, "fly", true);
        _skeletonAnimation.AnimationState.SetEmptyAnimation(1, 0);
        _skeletonAnimation.AnimationState.AddAnimation(1, "touching", true, 0).MixDuration = 0.5f;
        _skeletonAnimation.AnimationState.AddEmptyAnimation(1, 0.5f, 100f);

    }
    public void MixStateIdleAndTouching(float TimeTouching)
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
        _skeletonAnimation.AnimationState.SetEmptyAnimation(1, 0);
        _skeletonAnimation.AnimationState.AddAnimation(1, "touching", true, 0).MixDuration = 0.5f;
        _skeletonAnimation.AnimationState.AddEmptyAnimation(1, 0.5f, TimeTouching);
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
        //StartCoroutine(Move(transform, RealPosBird, 1f));
        transform.DOMove(RealPosBird, 0.7f) ;
        StartCoroutine(WaitTimeChangeStateWhenStartGame());
    }
    IEnumerator WaitTimeChangeStateWhenStartGame()
    {
        yield return new WaitForSeconds(0.7f);
        StateGrounding();
        yield return new WaitForSeconds(0.4f);
        StateIdle();
    }
    public void Renew()
    {
        TimeMove = 0.54f;

        StateIdle();
        _skeletonAnimation.skeleton.FlipX = false;
        ObjectPooler._instance.AddElement("Bird"+id, gameObject);
        gameObject.transform.parent = ObjectPooler._instance.transform;
        gameObject.SetActive(false);
    }

    
}
