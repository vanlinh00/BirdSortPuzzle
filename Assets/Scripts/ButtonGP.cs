using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGP : MonoBehaviour
{
    [SerializeField] GameObject _darkBg;
    [SerializeField] int idButton;
    public  int numberClick;
    public void Update()
    {
        //if(/*GameManager._instance._gamePlay.IsBirdMoving|| */numberClick<=0)
        //{
        //    _darkBg.SetActive(true);
        //}
        //else
        //{
        //    _darkBg.SetActive(false);

        //}
    }
    public void Click()
    {
        _darkBg.SetActive(true);
        StartCoroutine(WaitTimeEnableDarkBg());
    }
    IEnumerator WaitTimeEnableDarkBg()
    {
        yield return new WaitForSeconds(0.2f);
        _darkBg.SetActive(false);
    }
    public bool IsReady()
    {
        if(!_darkBg.activeSelf)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
