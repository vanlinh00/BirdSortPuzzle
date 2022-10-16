using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Uicontroller : MonoBehaviour
{
    [SerializeField] Button _restartBtn;
    [SerializeField] Button _ChangeSteatsBtn;
    [SerializeField] Button _undo;
    private void Awake()
    {
        _restartBtn.onClick.AddListener(RestartGame);
        _ChangeSteatsBtn.onClick.AddListener(ChangeSeatsLevel);
        _undo.onClick.AddListener(Undo);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void ChangeSeatsLevel()
    {
        GameManager._instance.gameState = GameManager.GameState.ChangeSeatsBirds;
    }
    public void Undo()
    {
        GameManager._instance.Undo();
    }
}
