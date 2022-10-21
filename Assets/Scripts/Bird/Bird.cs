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
    public float TimeMove = 0.5f;
    public int idBranchStand;
 
    public void Start()
    {
        StateIdle();
    }
    public void SetOrderLayer(int NumberLayer)
    {
        _skeletonAnimation.GetComponent<MeshRenderer>().sortingOrder = NumberLayer;
    }
    public void StateFly()
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, "fly", true);
        _skeletonAnimation.AnimationState.SetEmptyAnimation(1, 0);
    }
    public void StateIdle()
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
        _skeletonAnimation.AnimationState.SetEmptyAnimation(1, 0);
    }
    public void StateGrounding()
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, "grounding", true);
        _skeletonAnimation.AnimationState.SetEmptyAnimation(1, 0);

    }

    public void MoveToTarget(Vector3 Target, bool IsFlipX)
    {
        MixStateFlyAndTouching();
        StartCoroutine(TestMove(Target,IsFlipX));
    }

    IEnumerator TestMove(Vector3 Target, bool IsFlipX)
    {
        //bool isMove = true;
        //while (isMove)
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
        //    if (transform.position == Target)
        //    {
        //        isMove = false;
        //        StartCoroutine(WaitTimeChangeState(IsFlipX));
        //        StartCoroutine(GameManager._instance._gamePlay.ShakyBranch(idBranchStand));

        //    }
        //    yield return new WaitForEndOfFrame();
        //}
        transform.DOMove(Target, TimeMove).SetEase(Ease.Linear);
        yield return new WaitForSeconds(TimeMove);
        StartCoroutine(WaitTimeChangeState(IsFlipX));
        StartCoroutine(GameManager._instance._gamePlay.ShakyBranch(idBranchStand));

    }

    public void ChangeSeats(Vector3 Target, bool IsFlipX)
    {
        StateFly();
        StartCoroutine(TestMove(Target, IsFlipX));
    }
    IEnumerator WaitTimeChangeState(bool IsFlipX)
    {
        if (ParentObj != null)
        {
            transform.parent = ParentObj.transform;
        }
        else
        {
            Debug.Log(id);
        }
        if (IsFlipX)
        {
            FlipX();
        }
        StateGrounding();
        yield return new WaitForSeconds(0.3f);
        StateIdle();
      
    }
    public void MixStateFlyAndTouching()
    {
        StateFly();

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
        GameManager._instance._gamePlay.IsBirdMoving = true;
        StateFly();
        transform.DOMove(RealPosBird, 0.7f) ;
        StartCoroutine(WaitTimeChangeStateWhenStartGame());
    }
    IEnumerator WaitTimeChangeStateWhenStartGame()
    {
        yield return new WaitForSeconds(0.7f);
        StateGrounding();
        yield return new WaitForSeconds(0.4f);
        StateIdle();
        GameManager._instance._gamePlay.IsBirdMoving = false;
    }
    public void Renew()
    {
        TimeMove = 5f;

        StateIdle();
        _skeletonAnimation.skeleton.FlipX = false;
        ObjectPooler._instance.AddElement("Bird"+id, gameObject);
        gameObject.transform.parent = ObjectPooler._instance.transform;
        gameObject.SetActive(false);
    }
}
