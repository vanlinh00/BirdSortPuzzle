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
    public int idSlot;

    public Vector3 _target;
    public bool _isMove = false;
    public float duration;
    public bool isMoveToSlot;

    Parabola _parabola;
    Vector3 _startPos;
    private float _preTime;

    public bool isMoveCurve;

    public ParticleSystem SprakleEffect;
    private void OnEnable()
    {
        SprakleEffect.Stop();
    }
    public void Start()
    {
        StateIdle();
    }
    private void Update()
    {
        if (_isMove)
        {
            if (((Time.time - _preTime) / duration) <= 1)
            {
                _parabola.Move(transform, _startPos, _target, (Time.time - _preTime) / duration);
            }
            else
            {
                if(!isMoveToSlot)
                {
                    transform.position = _target;
                }
                else
                {
                    transform.position = GameManager._instance._gamePlay.ListAllBranchs[idBranchStand].allSlots[idSlot].transform.position;
                }
             
                _isMove = false;
            }
        }
    }
    public void UpdateMoveMent(Vector3 Target, float Duration,float H)
    {
        _target = Target;
        duration = Duration;
        _preTime = Time.time;
        _startPos = transform.position;
        _parabola = new Parabola(H);
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
        StartCoroutine(Move(Target,IsFlipX,true));
    }

    IEnumerator Move(Vector3 Target, bool IsFlipX,bool isMoveToNextBranch)
    {
        if (isMoveToNextBranch)
        {
            UpdateMoveMent(Target, TimeMove,0.25f);
            _isMove = true;
        }
        else
        {
            UpdateMoveMent(Target, TimeMove, 0.15f);
            _isMove = true;
            //    transform.DOMove(Target, TimeMove).SetEase(Ease.Linear);
        }

        yield return new WaitForSeconds(TimeMove-0.1f);
        StartCoroutine(WaitTimeChangeState(IsFlipX));
        StartCoroutine(GameManager._instance._gamePlay.ShakyBranch(idBranchStand));

    }

    public void ChangeSeats(Vector3 Target, bool IsFlipX)
    {
        StateFly();

        if (isMoveCurve)
        {
            StartCoroutine(Move(Target, IsFlipX, true));
        }
        else
        {
            StartCoroutine(Move(Target, IsFlipX, false));
        }
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
        yield return new WaitForSeconds(0.15f);
        StateGrounding();
        yield return new WaitForSeconds(0.2f);
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
    public void MoveToOnScreen(Vector3 Target)
    {
        GameManager._instance._gamePlay.IsBirdMoving = true;
        StateFly();
        transform.DOMove(Target, 0.7f);

        StartCoroutine(WaitTimeChangeStateWhenStartGame());
    }
    IEnumerator WaitTimeChangeStateWhenStartGame()
    {
        yield return new WaitForSeconds(0.7f);
        StateGrounding();
        yield return new WaitForSeconds(0.3f);
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
