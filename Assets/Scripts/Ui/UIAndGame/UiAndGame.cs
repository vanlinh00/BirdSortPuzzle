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

    [SerializeField] CanvasGroup _canvasGroup;
    private void Awake()
    {
        _continueGmaeBtn.onClick.AddListener(ContinueGame);
    }
    public void ContinueGame()
    {
        if(_canvasGroup.alpha==1)
        {
            GameManager._instance.NextLevel();
            Uicontroller._instance.OpenUiGamePlay(true);
        }

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

}
