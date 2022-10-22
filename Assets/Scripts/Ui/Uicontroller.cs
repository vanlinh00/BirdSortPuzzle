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
        OpenUiGamePlay();
    }
    public void OpenUiGamePlay()
    {
        _UiPauseGame.SetActive(false);
        _uiAndGame.SetActive(false);
        _uiGamePlay.SetActive(true);
        _uiGamePlay.GetComponent<UiGamePlay>().UpdateLevel(GameManager._instance.Getlevel());
        _uiGamePlay.GetComponent<UiGamePlay>().FunctionGame(GameManager._instance.Getlevel());
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

    }

}
