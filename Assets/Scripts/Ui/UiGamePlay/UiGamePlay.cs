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

    bool IsNextLevel = false;
    bool addBranch = true;

    [SerializeField] CanvasGroup _canvasGroup;
    [SerializeField] Animator _headerUiGP;
    [SerializeField] Text _undoTxt;

    public void StateOutHeaderUiGP()
    {
        _headerUiGP.SetBool("Out", true);
    }
    public void StateInHeaderUiGP()
    {
        _headerUiGP.SetBool("Out", false);
    }
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
    private void Start()
    {
        IsNextLevel = true;
        addBranch = true;
    }
    public IEnumerator FunctionGame(int level)
    {
        if(level<=2)
        {
            _nextLevelBtn.gameObject.SetActive(true);
            _undoBtn.gameObject.SetActive(false);
            _changeSteatsBtn.gameObject.SetActive(false);
            _addBranch.gameObject.SetActive(false);
        }
        else if(level==3)
        {
            _nextLevelBtn.gameObject.SetActive(true);
            _undoBtn.gameObject.SetActive(true);
            _changeSteatsBtn.gameObject.SetActive(false);
            _addBranch.gameObject.SetActive(false);
        }
        else if(level==4)
        {
            _nextLevelBtn.gameObject.SetActive(true);
            _undoBtn.gameObject.SetActive(true);
            _addBranch.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
            _changeSteatsBtn.gameObject.SetActive(true);
            TutorialManager._instance.MoveHandToTarget(new Vector3(-0.261f, -3.046f, 0));

        }
        else if(level==5)
        {
            CountAddBranch = 2;
            _nextLevelBtn.gameObject.SetActive(true);
            _undoBtn.gameObject.SetActive(true);
            _changeSteatsBtn.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            _addBranch.gameObject.SetActive(true);
            TutorialManager._instance.MoveHandToTarget(new Vector3(-1.45f, -3.046f, 0));
        }
        else
        {
            TutorialManager._instance.SetActiveHand(false);
            _nextLevelBtn.gameObject.SetActive(true);
            _undoBtn.gameObject.SetActive(true);
            _changeSteatsBtn.gameObject.SetActive(true);

            CountAddBranch=2;
            _addBranch.gameObject.SetActive(true);
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
        if(IsNextLevel)
        {
            ResetNumberUndo();
            IsNextLevel = false;
            GameManager._instance.NextLevel();
            Uicontroller._instance.OpenUiGamePlay(false);
            StartCoroutine(WaitTimeNextLevel());
        }
    }
    IEnumerator WaitTimeNextLevel()
    {
        ResetNumberUndo();
        yield return new WaitForSeconds(3f);
        IsNextLevel = true;
    }
    public void RestartGame()
     {
        if (IsNextLevel)
        {
            IsNextLevel = false;
            StartCoroutine(GameManager._instance.WaitTimeRenew());
            StartCoroutine(SetAnimationInAndOut());
            StartCoroutine(WaitTimeNextLevel());
            StartCoroutine(FunctionGame(GameManager._instance.Getlevel()));
        }
    }
    IEnumerator SetAnimationInAndOut()
    {
        StateOutHeaderUiGP();
        yield return new WaitForSeconds(0.2f);
        StateInHeaderUiGP();
    }
    public void ChangeSeats()
    {
        TutorialManager._instance.SetActiveHand(false);
        if (_changeSteatsBtn.GetComponent<ButtonGP>().IsReady())
        {
            if(!_darkBgChangeSeats.activeSelf)
            {
                StateOutHeaderUiGP();
                _darkBgChangeSeats.SetActive(true);
                GameManager._instance._gamePlay.EnableBirdsCanChangeSeats();
                GameManager._instance.gameState = GameManager.GameState.ChangeSeatsBirds;
            }
            else
            {
                DisableChangeSeats();
            }

        }

    }
    public void DisableChangeSeats()
    {
        StateInHeaderUiGP();
        _darkBgChangeSeats.SetActive(false);
        GameManager._instance.gameState = GameManager.GameState.SortBirds;
    }
    public void Undo()
    {
        TutorialManager._instance.SetActiveHand(false);
        if (_undoBtn.GetComponent<ButtonGP>().IsReady())
        {
            GameManager._instance._gamePlay.UnTouchingAndRemoveAllBirds();
            GameManager._instance.Undo();
            _undoBtn.GetComponent<ButtonGP>().Click();
       }
    }
    public void AddBranch()
    {
        TutorialManager._instance.SetActiveHand(false);

        if(addBranch)
        {
            addBranch = false;
            StartCoroutine(WaitTimeAddBranch());
            if (_addBranch.GetComponent<ButtonGP>().IsReady())
            { 
                BranchManager._instance.AddNewBranch();
                CountAddBranch--;
                if (CountAddBranch <= 0)
                {
                    _addBranch.gameObject.SetActive(false);
                }
            }
        }
       
    
    }
    IEnumerator WaitTimeAddBranch()
    {
        yield return new WaitForSeconds(1f);
        addBranch = true;
    }
    public void In()
    {
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        float t = 0;
        while (_canvasGroup.alpha < 1)
        {
            yield return new WaitForEndOfFrame();
            _canvasGroup.alpha = t;
            t += Time.deltaTime * 1.7f;
        }
    }
    public void Out()
    {
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        float t = 1;
        while (_canvasGroup.alpha >= 0)
        {
            yield return new WaitForEndOfFrame();
            _canvasGroup.alpha = t;
            t -= Time.deltaTime * 2f;
        }
    }
    public void CountNumberUndo()
    {
        _undoBtn.GetComponent<ButtonGP>().numberClick++;
        _undoTxt.text = _undoBtn.GetComponent<ButtonGP>().numberClick.ToString();
    }
    public void MinusNumberUndo()
    {
        _undoBtn.GetComponent<ButtonGP>().numberClick--;
        _undoTxt.text = _undoBtn.GetComponent<ButtonGP>().numberClick.ToString();
    }
    public void ResetNumberUndo()
    {
        _undoBtn.GetComponent<ButtonGP>().numberClick = 0;
        _undoTxt.text = 0.ToString();
    }
}
