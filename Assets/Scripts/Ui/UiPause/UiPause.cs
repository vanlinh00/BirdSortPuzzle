using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPause : MonoBehaviour
{
    [SerializeField] Button _homeBtn;
    [SerializeField] Button _musicBtn;
    [SerializeField] Button _soundBtn;
    [SerializeField] Button _bravible;
    [SerializeField] Button _shopeBtn;
    [SerializeField] Button _adsBtn;
    [SerializeField] Button _closeBtn;
    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Animator _animator;

    [SerializeField] UiShop _uiShop;
    private void OnEnable()
    {
        GameManager._instance.gameState = GameManager.GameState.PauseGame;
        StateIn();
    }
    private void Awake()
    {
        _closeBtn.onClick.AddListener(OpenGamePlay);
        _shopeBtn.onClick.AddListener(OpenShop);
    }

    private void OpenShop()
    {
        GameManager._instance.gameState = GameManager.GameState.PauseGame;
        _uiShop.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OpenGamePlay()
    {
        if(_canvasGroup.alpha ==1)
        {
            StartCoroutine(WaitDisableOut());
        }
    }
    IEnumerator WaitDisableOut()
    {
        StateOut();
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);
        GameManager._instance.gameState = GameManager.GameState.SortBirds;
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
