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

    [SerializeField] CanvasGroup _canvasGroup;
    private void Awake()
    {
        _closeBtn.onClick.AddListener(OpenGamePlay);
    }
    public void OpenGamePlay()
    {
        if(_canvasGroup.alpha ==1)
        {
            StartCoroutine(WaitDisableOut());
        }
    }
    IEnumerator WaitDisableOut()
    {
        Out();
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);

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
