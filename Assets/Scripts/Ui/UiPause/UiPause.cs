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
    [SerializeField] Text _amountCoinTxt;

    //private bool _isBravibleOn;

    private void OnEnable()
    {
        //    _isBravibleOn = DataPlayer.GetInforPlayer().isOnBravible;

        GameManager._instance.gameState = GameManager.GameState.PauseGame;
        GameManager._instance._gamePlay.UnTouchingAndRemoveAllBirdsMoveCurrentBranch();
        StateIn();
        _amountCoinTxt.text = DataPlayer.GetInforPlayer().countCoins.ToString();
    }
    private void Awake()
    {
        ChangeStateMusic();
        _closeBtn.onClick.AddListener(OpenGamePlay);
        _shopeBtn.onClick.AddListener(OpenShop);
        _musicBtn.onClick.AddListener(ClickMusicBtn);
        _soundBtn.onClick.AddListener(ClickSoundBtn);
        _bravible.onClick.AddListener(ClickBravibleBtn);
    }
    void ClickMusicBtn()
    {
        DataPlayer.ChangeStateAudio(!DataPlayer.GetInforPlayer().isOnMusicBg);
        ChangeStateMusic();
    }
    public void ChangeStateMusic()
    {
       bool _isMusicOn = DataPlayer.GetInforPlayer().isOnMusicBg;
       Sprite sprite;
        if (_isMusicOn)
        {
           sprite = Resources.Load<Sprite>("Ui/UiPause/Settings/Settings_btn_music_on");
        }
        else
        {
            sprite = Resources.Load<Sprite>("Ui/UiPause/Settings/Settings_btn_music_off");
        }
        _musicBtn.GetComponent<Image>().sprite = sprite;
        Uicontroller._instance.ChangeStateSoundBackGround();
    }
    void ClickSoundBtn()
    {
        DataPlayer.ChangeStateSound(!DataPlayer.GetInforPlayer().isOnSound);
        ChangeStateSound();
    }
   public void ChangeStateSound()
    {
        bool _isSoundOn = DataPlayer.GetInforPlayer().isOnSound;
        Sprite sprite;
        if (_isSoundOn)
        {
            sprite = Resources.Load<Sprite>("Ui/UiPause/Settings/Settings_btn_sound_on");
        }
        else
        {
            sprite = Resources.Load<Sprite>("Ui/UiPause/Settings/Settings_btn_sound_off");
        }
        _soundBtn.GetComponent<Image>().sprite = sprite;
    }
    void ClickBravibleBtn()
    {

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
