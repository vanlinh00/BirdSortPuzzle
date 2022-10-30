using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupNotice : Singleton<PopupNotice>
{
    [SerializeField] Button _darkBgBtn;
    [SerializeField] Button _okBtn;
    [SerializeField] Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _darkBgBtn.onClick.AddListener(Exit);
        _okBtn.onClick.AddListener(Exit);
    }
    private void OnEnable()
    {
        StateIn();
    }
    public void Exit()
    {
        SoundController._instance.OnPlayAudio(SoundType.ButtonClick);
        StartCoroutine(WaitTimeDisable());
    }
    IEnumerator WaitTimeDisable()
    {
        StateOut();
        yield return new WaitForSeconds(0.14f);
        gameObject.SetActive(false);
    }
    public void StateOut()
    {
        _animator.SetBool("Out", true);
    }
    public void StateIn()
    {
        _animator.SetBool("Out", false);
    }
}
