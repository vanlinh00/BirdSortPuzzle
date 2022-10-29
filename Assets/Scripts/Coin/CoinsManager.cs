using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : Singleton<CoinsManager>
{   

    [Space]
    [Header("aniamtion setting")]
    [SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;

    [SerializeField] Vector3 targetPosition;
	[SerializeField] Vector3 desPosition;
	[SerializeField] Ease easeType;
	[SerializeField] float spread;
	[SerializeField] float _scaleCoin;
    protected override void Awake()
    {
		base.Awake();
    }
    public void SetSpread(float value)
    {
        spread = value;
    }
    
    public  void Animate( int amount, Vector3 TargetPosition)
	{
        targetPosition = TargetPosition;

        StartCoroutine(FadeAnimate(amount));
	}
	IEnumerator FadeAnimate(int amount)
    {

        List<Coin> ListCoins = new List<Coin>();
        for (int i = 0; i < amount; i++)
        {
            GameObject coin = ObjectPooler._instance.SpawnFromPool("Coin", desPosition, Quaternion.identity);
            coin.transform.position = desPosition;
            coin.transform.localScale = new Vector3(0f, 0f, 0);
            coin.transform.DOScale(_scaleCoin, 0.1f);
            coin.transform.DOMove(desPosition + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread), 0f), 0.3f).SetEase(Ease.OutBack);
            ListCoins.Add(coin.GetComponent<Coin>());
        }
        yield return new WaitForSeconds(0.25f);
        for (int i = 0; i < ListCoins.Count; i++)
        {
            UiAndGame._instance.CoinsMidlle--;
            float duration = Random.Range(minAnimDuration, maxAnimDuration);
            GameObject CoinPrefab = ListCoins[i].gameObject;
            CoinPrefab.transform.DOMove(targetPosition, duration)
            .SetEase(easeType)
            .OnComplete(() =>
            {
                CoinPrefab.SetActive(false);
                ObjectPooler._instance.AddElement("Coin", CoinPrefab);
                UiAndGame._instance.Coins++;
                DataPlayer.UpdateAmountCoins(UiAndGame._instance.Coins);
            });
            yield return new WaitForSeconds(0.05f);
        }
        // neu thay doi co the thay thoi time wait = 0.2 - 0.2 - 0.15

        //float a = spread;
        //yield return new WaitForSeconds(0f);
        //List<Coin> ListCoins = new List<Coin>();
        //int layer = 95;
        //for (int i = 0; i < amount; i++)
        //{
        //    if (i == 0)
        //    {
        //        spread = 0;
        //    }
        //    //else
        //    //{
        //    //    spread = a;
        //    //}
        //    GameObject coin = ObjectPooler._instance.SpawnFromPool("Coin", desPosition, Quaternion.identity);
        //    coin.transform.position = desPosition + new Vector3(Random.Range(-spread, spread), Random.Range(-spread, spread));
        //    spread = -0.01f;
        //    coin.GetComponent<SpriteRenderer>().sortingOrder = layer;
        //    layer--;
        //    ListCoins.Add(coin.GetComponent<Coin>());
        //}
        //yield return new WaitForSeconds(0.05f);
        //float Spread2 = a * 7.5f;
        //for (int i = ListCoins.Count - 1; i > 0; i--)
        //{
        //    ListCoins[i].transform.DOMove(ListCoins[i].transform.position + new Vector3(Random.Range(-Spread2, Spread2), Random.Range(-Spread2, Spread2), 0f), 0.2f).SetEase(Ease.OutBack);
        //    Spread2 = Spread2 - 0.014f;

        //}

        //yield return new WaitForSeconds(0.15f);
        //for (int i = ListCoins.Count - 1; i >= 0; i--)
        //{
        //    UiAndGame._instance.CoinsMidlle--;
        //    float duration = Random.Range(minAnimDuration + 0.1f, maxAnimDuration);
        //    GameObject CoinPrefab = ListCoins[i].gameObject;
        //    CoinPrefab.transform.DOMove(targetPosition, duration)
        //    .SetEase(easeType)
        //    .OnComplete(() =>
        //    {
        //        CoinPrefab.SetActive(false);
        //        ObjectPooler._instance.AddElement("Coin", CoinPrefab);
        //        UiAndGame._instance.Coins++;
        //        DataPlayer.UpdateAmountCoins(UiAndGame._instance.Coins);
        //    });
        //    yield return new WaitForSeconds(0.01f);
        //}
        //spread = 0.1f;
    }

}
