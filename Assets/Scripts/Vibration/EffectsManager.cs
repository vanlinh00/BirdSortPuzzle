using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : Singleton<EffectsManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
  
    public void VibrationWithDelay(long milliseconds, float timer) // #param1 Duration, #param2 Delay
    {
        if(DataPlayer.GetInforPlayer().isOnBravible)
        {
            StartCoroutine(VibrateDelay(milliseconds, timer));
        }
      
    }

    IEnumerator VibrateDelay(long milliseconds, float timer)
    {
        yield return new WaitForSeconds(timer);
        Vibration.Vibrate(milliseconds);
    }
}