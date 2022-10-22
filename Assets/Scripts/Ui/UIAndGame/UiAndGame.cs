using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiAndGame : MonoBehaviour
{
    [SerializeField] Button _homeBtn;
    [SerializeField] Text _totalcointTxt;
    [SerializeField] Button _continueGmaeBtn;
    [SerializeField] Text _countCoinTxt;
    private void Awake()
    {
        _continueGmaeBtn.onClick.AddListener(NextLevel);
    }
    public void NextLevel()
    {
        GameManager._instance.NextLevel();
        Uicontroller._instance.OpenUiGamePlay();
    }

}
