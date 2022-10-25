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
    private void OnEnable()
    {
        GameManager._instance.gameState = GameManager.GameState.PauseGame;
        StateIn();
    }
    private void OnDisable()
    {
        GameManager._instance.gameState = GameManager.GameState.SortBirds;
    }
    private void Awake()
    {
        _closeBtn.onClick.AddListener(OpenGamePlay);
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
