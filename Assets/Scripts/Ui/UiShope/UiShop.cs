using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiShop : Singleton<UiShop>
{
    [SerializeField] Button shopBackGroundBtn;
    [SerializeField] Button shopBirdBtn;
    [SerializeField] Button branhBtn;
    [SerializeField] Button darkBgBtn;
    [SerializeField] Button closeBtn;

    [SerializeField] ShopManager shopManager;
    [SerializeField] Animator _animator;
    protected override void Awake()
    {
        base.Awake();
        shopBackGroundBtn.onClick.AddListener(OpenShopBackGrounds);
        shopBirdBtn.onClick.AddListener(OpenShopBrids);
        branhBtn.onClick.AddListener(OpenShopBranch);
        closeBtn.onClick.AddListener(CloseShop);
    }
    private void OnEnable()
    {
        StateIn();
        OfButtonOpenShopBirds();
        OfButtonOpenShopBranch();
        OnButtonOpenShopBgs();
        shopManager.LoadDataShopBg();
    }
    private void OnDisable()
    {
        GameManager._instance.gameState = GameManager.GameState.SortBirds;
    }
    private void CloseShop()
    {
        StartCoroutine(WaitTimeDisable());
    }
    IEnumerator WaitTimeDisable()
    {
        StateOut();
        yield return new WaitForSeconds(0.14f);
        gameObject.SetActive(false);
    }
    public void StateOut()
    {
        _animator.SetBool("Out", true);
    }
    public void StateIn()
    {
        _animator.SetBool("Out", false);
    }
    private void OpenShopBranch()
    {
     
        if (!branhBtn.transform.GetChild(0).gameObject.activeSelf)
        {
            OfButtonOpenShopBirds();
            OfButtonOpenShopBgs();
            OnButtonOpenShopBranch();
            shopManager.LoadDataShopBranch();
        }
    }

    private void OpenShopBrids()
    {
        if (!shopBirdBtn.transform.GetChild(0).gameObject.activeSelf)
        {
            OfButtonOpenShopBranch();
            OfButtonOpenShopBgs();
            OnButtonOpenShopBirds();
            shopManager.LoadDataShopBirds();
        }
    }

    private void OpenShopBackGrounds()
    {
        if (!shopBackGroundBtn.transform.GetChild(0).gameObject.activeSelf)
        {
            OfButtonOpenShopBirds();
            OfButtonOpenShopBranch();
            OnButtonOpenShopBgs();
            shopManager.LoadDataShopBg();
        }
    }

    //---------
    public void OfButtonOpenShopBirds()
    {
        var sprite = Resources.Load<Sprite>("Ui/UiShop/Buttons/shop_tag_bird_off");
        shopBirdBtn.GetComponent<Image>().sprite = sprite;
        shopBirdBtn.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        shopBirdBtn.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnButtonOpenShopBirds()
    {
        var sprite = Resources.Load<Sprite>("Ui/UiShop/Buttons/shop_tag_bird_on");
        shopBirdBtn.GetComponent<Image>().sprite = sprite;
        shopBirdBtn.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.1f, 1f);

        shopBirdBtn.transform.GetChild(0).gameObject.SetActive(true);
    }

    ///  --------
    public void OfButtonOpenShopBgs()
    {
        var sprite = Resources.Load<Sprite>("Ui/UiShop/Buttons/shop_tag_screnery_off");
        shopBackGroundBtn.GetComponent<Image>().sprite = sprite;
        shopBackGroundBtn.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        shopBackGroundBtn.transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OnButtonOpenShopBgs()
    {
        var sprite = Resources.Load<Sprite>("Ui/UiShop/Buttons/shop_tag_screnery_on");
        shopBackGroundBtn.GetComponent<Image>().sprite = sprite;
        shopBackGroundBtn.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.1f, 1f);
        shopBackGroundBtn.transform.GetChild(0).gameObject.SetActive(true);
    }

    ///--------
    public void OfButtonOpenShopBranch()
    {
        var sprite = Resources.Load<Sprite>("Ui/UiShop/Buttons/shop_tag_branch_off");
        branhBtn.GetComponent<Image>().sprite = sprite;
        branhBtn.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        branhBtn.transform.GetChild(0).gameObject.SetActive(false);

    }
    public void OnButtonOpenShopBranch()
    {
        branhBtn.transform.GetChild(0).gameObject.SetActive(true);
        var sprite = Resources.Load<Sprite>("Ui/UiShop/Buttons/shop_tag_branch_on");
        branhBtn.GetComponent<RectTransform>().localScale = new Vector3(1.2f, 1.1f, 1f);
        branhBtn.GetComponent<Image>().sprite = sprite;
    }
}
