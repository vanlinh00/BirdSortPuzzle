using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundsManager : Singleton<BackGroundsManager>
{
    [SerializeField] SpriteRenderer _backGround;

    protected override void Awake()
    {
        base.Awake();
        LoadBackGround();
    }
    public void LoadBackGround()
    { 
        int IdBg = DataPlayer.GetInforPlayer().idCurrentBgsLoading;
        var sprite = Resources.Load<Sprite>("Shop/BG/BG" + IdBg);
        _backGround.sprite = sprite;
    }
}
