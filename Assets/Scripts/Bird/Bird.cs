using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        //  StartCoroutine(Move(transform, Target, 0.54f));
        transform.DOMove(Target, TimeMove);
        StartCoroutine(WaitTimeChangeState(IsFlipX));
    }
    IEnumerator WaitTimeChangeState(bool IsFlipX)
    {
        yield return new WaitForSeconds(TimeMove);
        if (IsFlipX)
        {
            FlipX();
        }
        yield return new WaitForSeconds(0.02f);
        yield return new WaitForSeconds(TimeMove*0.3f/ 8f);
        StateGrounding();
        if (ParentObj != null)
        {
            transform.parent = ParentObj.transform;
        }
        else
        {
            Debug.Log(id);
        }
        yield return new WaitForSeconds(0.2f);
        StateIdle();
    }
    public void Mix2Animation(AnimationReferenceAsset animation, bool loop)
    {
        Spine.TrackEntry animationEntry = _skeletonAnimation.state.AddAnimation(1, animation, loop, 0);
        animationEntry.Complete += AnimationEntry_Complete;
    }
    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {

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
   //public void Mix2Animation()
   // {
   // }
   // public void AddAnimation(AnimationReferenceAsset animation,bool loop)
   // {
   //     Spine.TrackEntry animationEntry = _skeletonAnimation.state.AddAnimation();
   // }
    //IEnumerator Move(Transform CurrentTransform, Vector3 Target, float TotalTime)
    //{
    //    var passed = 0f;
    //    var init = CurrentTransform.transform.position;
    //    while (passed < TotalTime)
    //    {
    //        passed += Time.deltaTime;
    //        var normalized = passed / TotalTime;
    //        var current = Vector3.Lerp(init, Target, normalized);
    //        CurrentTransform.position = current;
    //        yield return null;
    //    }

    //}
    
}
