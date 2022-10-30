using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBtn: MonoBehaviour
{
    public int id;
    public Button selectBtn;
    public Button backGroundBtn;
    public Button buyBtn;
    public Image image;
    public int price;
    public int idType;
    public Text priceTxt;
    public void Init(int Id, Sprite NewSprite,int Price,int IdType)
    {
        id = Id;
        image.sprite = NewSprite;
        price = Price;
        idType = IdType;
        priceTxt.text = price.ToString();
    }
    private void Awake()
    {
        selectBtn.onClick.AddListener(ClickSelectBtn);
        backGroundBtn.onClick.AddListener(ClickBackGroundBtn);
        buyBtn.onClick.AddListener(ClickBuyBtn);
    }

    private void ClickBuyBtn()
    {
        CheckConditionBuy();
    }
  
    private void ClickBackGroundBtn()
    {
        if(selectBtn.gameObject.activeSelf)
        {
            LoadData._instance.Loadata(idType, id);
        }
        else
        {
            CheckConditionBuy();
        }
    }
    void CheckConditionBuy()
    {
        int AmountCoins = DataPlayer.GetInforPlayer().countCoins;
        if (AmountCoins >= price)
        {
            DataPlayer.UpdateAmountCoins(AmountCoins - price);
            AddIdItemToDataPlayer();
            DisPlaySelectBtn();
            UiShop._instance.LoadAmountCoin();
        }
        else
        {
            UiShop._instance.EnablePopupNotice();
        }
    }
    private void ClickSelectBtn()
    {
        LoadData._instance.Loadata(idType, id);
    }

    public void AddIdItemToDataPlayer()
    {
        if (idType == 1)
        {
            DataPlayer.AddNewIdBg(id);
        }
        else if (idType == 2)
        {
            DataPlayer.AddNewIdBirds(id);
        }
        else if (idType == 3)
        {
            DataPlayer.AddNewlistIdBranchs(id);
        }

    }
    public void DisPlayBuyBtn()
    {
        selectBtn.gameObject.SetActive(false);
        buyBtn.gameObject.SetActive(true);
    }
    public void DisPlaySelectBtn()
    {
        buyBtn.gameObject.SetActive(false);
        selectBtn.gameObject.SetActive(true);
    }
}
