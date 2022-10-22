using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPause : MonoBehaviour
{
    [SerializeField] Button _homeBtn;
    [SerializeField] Button _musicBtn;
    [SerializeField] Button _soundBtn;
    [SerializeField] Button _bravible;

    [SerializeField] Button _shopeBtn;
    [SerializeField] Button _adsBtn;
    [SerializeField] Button _closeBtn;

    private void Awake()
    {
        _closeBtn.onClick.AddListener(OpenGamePlay);
    }
    public void OpenGamePlay()
    {
        Uicontroller._instance.OpenUiGamePlay();
    }

}
