using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiAndGame : Singleton<UiAndGame>
{
    [SerializeField] Button _homeBtn;
    [SerializeField] Text _totalcointTxt;
    [SerializeField] Button _continueGameBtn;

    [SerializeField] CanvasGroup _canvasGroup;

    [Header("UI references")]
    [SerializeField] TMP_Text coinUIText;
    private int _c;
    protected override void Awake()
    {
        base.Awake();
        _continueGameBtn.onClick.AddListener(ContinueGame);
    }
    private void Start()
    {
        coinUIText.text = DataPlayer.GetInforPlayer().countCoins.ToString() ;
        _c = DataPlayer.GetInforPlayer().countCoins;

    }
    public int Coins
    {
        get { return _c; }
        set
        {
            _c = value;
            coinUIText.text = Coins.ToString();
        }
    }
    public void ContinueGame()
    {
        if(_canvasGroup.alpha==1)
        {
            GameManager._instance.NextLevel();
            Uicontroller._instance.OpenUiGamePlay(true);
        }

    }
    private void OnEnable()
    {
        StartCoroutine(WaitTimeBornCoins());
    }
    IEnumerator WaitTimeBornCoins()
    {
        yield return new WaitForSeconds(0.4f);
        CoinsManager._instance.Animate(12);
        yield return new WaitForSeconds(0.5f);
        _continueGameBtn.gameObject.SetActive(true);
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
        _continueGameBtn.gameObject.SetActive(false);
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
