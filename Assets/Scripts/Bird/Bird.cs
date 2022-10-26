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

    Parabola _parabola;
    Vector3 _startPos;
    private float _preTime;
    public bool isMoveNextBranch;
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
                _isMove = false;
                transform.position = GameManager._instance._gamePlay.ListAllBranchs[idBranchStand].allSlots[idSlot].transform.position;
            }
        }
    }
    public void UpdateMoveMent( float Duration,float H)
    {
        _target = GameManager._instance._gamePlay.ListAllBranchs[idBranchStand].allSlots[idSlot].transform.position;
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

    public void MoveToTarget(bool IsFlipX)
    {
        MixStateFlyAndTouching();
        StartCoroutine(Move(IsFlipX,true));
    }

    IEnumerator Move(bool IsFlipX,bool isMoveToNextBranch)
    {
        if (isMoveToNextBranch)
        {
            UpdateMoveMent( TimeMove,0.4f);
             _isMove = true;
             //.transform.position, TimeMove).SetEase(Ease.Linear);
        }
        else
        {
            UpdateMoveMent( TimeMove, 0.15f);
            _isMove = true;
        }
        yield return new WaitForSeconds(TimeMove-0.1f);
        StartCoroutine(WaitTimeChangeState(IsFlipX));
        float TimeWait = (id != 2) ? 0.7f : 0.25f;
        StartCoroutine(GameManager._instance._gamePlay.ShakyBranch(idBranchStand,TimeWait));

    }

    public void ChangeSeats( bool IsFlipX)
    {
        StateFly();

        if (isMoveNextBranch)
        {
            StartCoroutine(Move(IsFlipX, true));
        }
        else
        {
            StartCoroutine(Move( IsFlipX, false));
        }
    }
    IEnumerator WaitTimeChangeState(bool IsFlipX)
    {
        if (ParentObj != null)
        {
            transform.parent = ParentObj.transform;
        }
        if (IsFlipX)
        {
            FlipX();
        }
        StateGrounding();
        yield return new WaitForSeconds(0.9f);
        StateIdle();
        yield return new WaitForSeconds(0.2f);
    }
    public void MixStateFlyAndTouching()
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, "fly", true);
        _skeletonAnimation.AnimationState.AddAnimation(1, "touching", false, 0).MixDuration =0f;
    }
    public void MixStateIdleAndTouching()
    {
        _skeletonAnimation.AnimationState.SetAnimation(0, "idle", true);
        _skeletonAnimation.AnimationState.AddAnimation(1, "touching", false, 0).MixDuration = 0f;
    }
    public void FlipX()
    {
        if (_skeletonAnimation.skeleton.FlipX)
        {
            _skeletonAnimation.skeleton.FlipX = false;
            SprakleEffect.transform.localRotation = Quaternion.Euler(143.8f, -90f, 0);
        }
        else
        {
            _skeletonAnimation.skeleton.FlipX = true;
            SprakleEffect.transform.localRotation = Quaternion.Euler(61.3f, -90f, 0);
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
