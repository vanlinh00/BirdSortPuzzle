using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Uicontroller : MonoBehaviour
{
    [SerializeField] Button _restartBtn;
    private void Awake()
    {
        _restartBtn.onClick.AddListener(RestartGame);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
