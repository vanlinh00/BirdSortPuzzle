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
    [SerializeField] Button _pauseGameBtn;
    [SerializeField] Text _levelTxt;
    protected override void Awake()
    {
        base.Awake();
        _restartGame.onClick.AddListener(RestartGame);
        _nextLevelBtn.onClick.AddListener(NextLevel);
        _changeSteatsBtn.onClick.AddListener(ChangeSeats);
        _undoBtn.onClick.AddListener(Undo);
        _addBranch.onClick.AddListener(AddBranch);
        _darkBgBtn.onClick.AddListener(DisableChangeSeats);
        _pauseGameBtn.onClick.AddListener(OpenPauseGame);
    }
    public void FunctionGame(int level)
    {
        if(level<=2)
        {
            _nextLevelBtn.gameObject.SetActive(true);
            _undoBtn.gameObject.SetActive(false);
            _changeSteatsBtn.gameObject.SetActive(false);
            _addBranch.gameObject.SetActive(false);
        }
        else if(level<=3)
        {
            _nextLevelBtn.gameObject.SetActive(true);
            _undoBtn.gameObject.SetActive(true);
            _changeSteatsBtn.gameObject.SetActive(false);
            _addBranch.gameObject.SetActive(false);

        }
        else if(level<6)
        {
            _nextLevelBtn.gameObject.SetActive(true);
            _undoBtn.gameObject.SetActive(true);
            _changeSteatsBtn.gameObject.SetActive(true);
            _addBranch.gameObject.SetActive(false);

        }
        else if(level<=100)
        {
            _nextLevelBtn.gameObject.SetActive(true);
            _undoBtn.gameObject.SetActive(true);
            _changeSteatsBtn.gameObject.SetActive(true);
            _addBranch.gameObject.SetActive(true);
        }

    }


    public void UpdateLevel(int level)
    {
        _levelTxt.text = "LEVEL " + level;
    }
    public void OpenPauseGame()
    {
        Uicontroller._instance.OpenUiPauseGame();
    }
    public void NextLevel()
    {
        GameManager._instance.NextLevel();
    }
        public void RestartGame()
     {  
        StartCoroutine(GameManager._instance.WaitTimeRenew());
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
