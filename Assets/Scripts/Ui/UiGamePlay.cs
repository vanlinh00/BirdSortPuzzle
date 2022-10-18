using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiGamePlay : MonoBehaviour
{
    [SerializeField] Button _restartGame;
    [SerializeField] Button _nextLevelBtn;
    [SerializeField] Button _ChangeSteatsBtn;
    [SerializeField] Button _undoBtn;
    private void Awake()
    {
        _restartGame.onClick.AddListener(RestartGame);
        _nextLevelBtn.onClick.AddListener(NextLevel);
        _ChangeSteatsBtn.onClick.AddListener(ChangeSeats);
        _undoBtn.onClick.AddListener(Undo);
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
        GameManager._instance.gameState = GameManager.GameState.ChangeSeatsBirds;
    }
    public void Undo()
    {
        GameManager._instance.Undo();
    }
}
