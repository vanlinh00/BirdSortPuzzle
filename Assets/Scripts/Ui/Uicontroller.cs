using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Uicontroller : Singleton<Uicontroller>
{
    [SerializeField] GameObject _uiGamePlay;
    [SerializeField] GameObject _UiPauseGame;
    [SerializeField] GameObject _uiAndGame;

    protected override void Awake()
    {
        base.Awake();
    }
    public void Start()
    {
        OpenUiGamePlay(false);
    }
    IEnumerator WaitOutAndGame()
    {
        _uiAndGame.GetComponent<UiAndGame>().StateOut();
       yield return new WaitForSeconds(0.4f);
        _UiPauseGame.SetActive(false);
        _uiAndGame.SetActive(false);
        _uiGamePlay.SetActive(true);
        _uiGamePlay.GetComponent<UiGamePlay>().In();
        _uiGamePlay.GetComponent<UiGamePlay>().UpdateLevel(GameManager._instance.Getlevel());
        StartCoroutine(_uiGamePlay.GetComponent<UiGamePlay>().FunctionGame(GameManager._instance.Getlevel()));
    }
    public void OpenUiGamePlay(bool AndGameToGamePlay)
    {
        if(AndGameToGamePlay)
        {
            StartCoroutine(WaitOutAndGame());
        }
        else
        {
            _UiPauseGame.SetActive(false);
            _uiAndGame.SetActive(false);
            _uiGamePlay.SetActive(true);
            _uiGamePlay.GetComponent<UiGamePlay>().In();
            _uiGamePlay.GetComponent<UiGamePlay>().UpdateLevel(GameManager._instance.Getlevel());
            StartCoroutine(_uiGamePlay.GetComponent<UiGamePlay>().FunctionGame(GameManager._instance.Getlevel()));
        }
       
    }
    public void OpenUiPauseGame()
    {
        _uiAndGame.SetActive(false);
        _uiGamePlay.SetActive(true);
        _UiPauseGame.SetActive(true);
    }
    public void OpenUiAndGame()
    {
        _uiGamePlay.SetActive(false);
        _UiPauseGame.SetActive(false);
        _uiAndGame.SetActive(true);
        TutorialManager._instance.SetActiveHand(false);
    }

}
