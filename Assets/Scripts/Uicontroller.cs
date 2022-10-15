using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Uicontroller : MonoBehaviour
{
    [SerializeField] Button _restartBtn;
    [SerializeField] Button _nextLevelBtn;
    private void Awake()
    {
        _restartBtn.onClick.AddListener(RestartGame);
        _nextLevelBtn.onClick.AddListener(NextLevel);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public void NextLevel()
    {

    }
}
