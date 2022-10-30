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
    [SerializeField] TMP_Text coinMidlleText;
    private int _coinMidlle;
    [SerializeField] GameObject _effectWinGame;
    [SerializeField] GameObject _fireWorkPs;
    [SerializeField] Animator _animator;
    [SerializeField] Image _coinImg;
    [SerializeField] Camera _camera;
    private void OnEnable()
    {
       ///CoinsManager._instance.SetSpread(0.2f);
        _continueGameBtn.gameObject.SetActive(false);
        CoinsMidlle = 12;
        StartCoroutine(WaitTimeBornCoins());
    }

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
    public int CoinsMidlle
    {
        get { return _coinMidlle; }
        set
        {
            _coinMidlle = value;
            coinMidlleText.text ="+"+ _coinMidlle.ToString();
        }
    }
    public void ContinueGame()
    {
        if(_canvasGroup.alpha==1)
        {
            SoundController._instance.OnPlayAudio(SoundType.ButtonClick);
            UiGamePlay._instance.ResetNumberUndo();
            _effectWinGame.SetActive(false);
            _fireWorkPs.SetActive(false);
            GameManager._instance.NextLevel();
            Uicontroller._instance.OpenUiGamePlay(true);

        }
    }

    IEnumerator WaitTimeBornCoins()
    {
        yield return new WaitForSeconds(0.4f);
        _effectWinGame.SetActive(true);
        _fireWorkPs.SetActive(true);
        SoundController._instance.OnPlayAudio(SoundType.Game_Win);
        CoinsManager._instance.Animate(12, _coinImg.transform.position);
        yield return new WaitForSeconds(1.5f);
        _continueGameBtn.gameObject.SetActive(true);
    
    }
    public void StateOut()
    {
        _animator.SetBool("Out", true);
    }
}
