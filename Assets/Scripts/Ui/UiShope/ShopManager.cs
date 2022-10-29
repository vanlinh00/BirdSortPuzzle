using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : Singleton<ShopManager>
{
    private int _idtypeBg = 1;
    private int _idTypeBird = 2;
    private int _idTypeBranch = 3;
    protected override void Awake()
    {
        base.Awake();
    }
    public void LoadDataShopBirds()
    {
        ResetShop();
        for (int i = 1; i <= 3; i++)
        {
            var sprite = Resources.Load<Sprite>("Ui/UiShop/Birds/Bird" + i);
            GameObject ItemBtn = ObjectPooler._instance.SpawnFromPool("ItemBtn", new Vector3(0, 0, 0), Quaternion.identity);
            ItemBtn newItem = ItemBtn.GetComponent<ItemBtn>();
            int Price = Random.RandomRange(1, 10);
            newItem.Init(i, sprite, Price, _idTypeBird);
            ItemBtn.transform.parent = transform;
            ItemBtn.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }
    public void LoadDataShopBg()
    {
        ResetShop();
        for (int i=1; i<=12;i++)
        {
            var sprite = Resources.Load<Sprite>("Ui/UiShop/Bg/BG"+i);
            GameObject ItemBtn = ObjectPooler._instance.SpawnFromPool("ItemBtn", new Vector3(0, 0, 0), Quaternion.identity);
            ItemBtn newItem = ItemBtn.GetComponent<ItemBtn>();
            int Price = Random.RandomRange(1, 10);
            newItem.Init(i, sprite, Price, _idtypeBg);

            if( DataPlayer.GetInforPlayer().listIdBg.Contains(newItem.id))
            {
                newItem.DisPlaySelectBtn();
            }
            else
            {
                newItem.DisPlayBuyBtn();
            }

            ItemBtn.transform.parent = transform;
            ItemBtn.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }
    public void LoadDataShopBranch()
    {
        ResetShop();
        for (int i = 1; i <= 4; i++)
        {
            var sprite = Resources.Load<Sprite>("Ui/UiShop/Branchs/Branch" + i);
            GameObject ItemBtn = ObjectPooler._instance.SpawnFromPool("ItemBtn", new Vector3(0, 0, 0), Quaternion.identity);
            ItemBtn newItem = ItemBtn.GetComponent<ItemBtn>();
            int Price = Random.RandomRange(1, 10);
            newItem.Init(i, sprite, Price, _idTypeBranch);

            if (DataPlayer.GetInforPlayer().listIdBg.Contains(newItem.id))
            {
                newItem.DisPlaySelectBtn();
            }
            else
            {
                newItem.DisPlayBuyBtn();
            }

            ItemBtn.transform.parent = transform;
            ItemBtn.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void ResetShop()
    {
        GameObject Itembtn;
        int NumChild = transform.childCount;
        for (int i = 0; i < NumChild; i++)
        {
            Itembtn = transform.GetChild(0).gameObject;
            Itembtn.SetActive(false);
            ObjectPooler._instance.AddElement("ItemBtn", Itembtn);
            Itembtn.transform.parent = ObjectPooler._instance.transform;
        }
    }
}
