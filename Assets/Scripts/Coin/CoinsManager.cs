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
    protected override void Awake()
    {
		base.Awake();
    }
  public  void Animate( int amount)
	{
		StartCoroutine(FadeAnimate(amount));
	}
	IEnumerator FadeAnimate(int amount)
    {
		for (int i = 0; i < amount; i++)
		{
			UiAndGame._instance.CoinsMidlle--;
			GameObject coin = ObjectPooler._instance.SpawnFromPool("Coin", desPosition, Quaternion.identity);
			coin.transform.position = desPosition;// + new Vector3(Random.Range(-spread, spread), 0f, 0f);
			float duration = 1f;//Random.Range(minAnimDuration, maxAnimDuration);
			coin.transform.DOMove(targetPosition, duration)
			.SetEase(easeType)
			.OnComplete(() => {
				coin.SetActive(false);
				ObjectPooler._instance.AddElement("Coin", coin);
				UiAndGame._instance.Coins++;
				DataPlayer.UpdateAmountCoins(UiAndGame._instance.Coins);
			});
			yield return new WaitForSeconds(0.1f);
		}
	}

}
