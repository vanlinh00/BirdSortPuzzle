using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGamePlay : Singleton<UiGamePlay>
{
    [SerializeField] Button _addBranch;
    [SerializeField] Button _restartGame;
    [SerializeField] Button _nextLevelBtn;
    [SerializeField] Button _changeSteatsBtn;
    [SerializeField] Button _undoBtn;

    [SerializeField] GameObject _darkBgChangeSeats;
    [SerializeField] Button _darkBgBtn;

    int CountAddBranch = 2;
    protected override void Awake()
    {
        base.Awake();
        _restartGame.onClick.AddListener(RestartGame);
        _nextLevelBtn.onClick.AddListener(NextLevel);
        _changeSteatsBtn.onClick.AddListener(ChangeSeats);
        _undoBtn.onClick.AddListener(Undo);
        _addBranch.onClick.AddListener(AddBranch);
        _darkBgBtn.onClick.AddListener(DisableChangeSeats);
    }
    public void NextLevel()
    {
        // SceneManager.LoadScene(0);
        StartCoroutine(WaitTimeRenew());
    }
    IEnumerator WaitTimeRenew()
    {
        GameManager._instance.ReNewGame();
        yield return new WaitForSeconds(0.1f);
        GameManager._instance.ReplayLevel();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ChangeSeats()
    {      
        if(_changeSteatsBtn.GetComponent<ButtonGP>().IsReady())
        {
            _darkBgChangeSeats.SetActive(true);
            GameManager._instance._gamePlay.EnableBirdsCanChangeSeats();
            GameManager._instance.gameState = GameManager.GameState.ChangeSeatsBirds;
        }

    }
    public void DisableChangeSeats()
    {

        _darkBgChangeSeats.SetActive(false);
        GameManager._instance.gameState = GameManager.GameState.SortBirds;
    }
    public void Undo()
    {
        if (_undoBtn.GetComponent<ButtonGP>().IsReady())
        {
            GameManager._instance.Undo();
            _undoBtn.GetComponent<ButtonGP>().Click();
        }
    }
    public void AddBranch()
    {

        if (_addBranch.GetComponent<ButtonGP>().IsReady())
        {
            GameManager._instance.StackStateUndos.Clear();
            BranchManager._instance.AddNewBranch();
            CountAddBranch--;
            if (CountAddBranch <= 0)
            {
                _addBranch.gameObject.SetActive(false);
            }
        }
    
    }
}
