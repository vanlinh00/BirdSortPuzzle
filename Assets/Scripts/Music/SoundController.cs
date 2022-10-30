using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Birdflappingwings1 = 0,
    Birdflappingwings2 = 1,
    Birdflappingwings3 = 2,
    Birdflappingwings4 = 3,
    Birdflappingwings5 = 4,
    Birdtweet1 = 5,
    Birdtweet2 = 6,
    Birdtweet3 = 7,
    Birdtweet4 = 9,
    Birdtweet5 = 10,
    Birdtweet6 = 11,
    Birdtweet7 = 12,
    ButtonClick = 13,
    Game_Win = 14,
    Summer = 15,
}
public class SoundController : MonoBehaviour
{
    public static SoundController _instance;
    public AudioSource audioFx;
    public AudioSource audioFx2;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            _instance = this;
        }
       // DontDestroyOnLoad(this);
    }
    //private void OnValidate()
    //{
    //    if (audioFx == null)
    //    {
    //        audioFx = gameObject.AddComponent<AudioSource>();
    //    }
    //}
    public void OnPlayAudio(SoundType soundType)
    {
        var audio = Resources.Load<AudioClip>($"AudioClip/{soundType.ToString()}");
        audioFx.clip = audio;
       // audioFx.Play();
        audioFx.PlayOneShot(audio);

    }

    public void OnPlayAudioFx2(string Link)
    {
        var audio = Resources.Load<AudioClip>($"AudioClip/"+ Link);
        audioFx2.clip = audio;
        // audioFx.Play();
        audioFx2.PlayOneShot(audio);

    }

    public void OfSound()
    {
        audioFx.volume = 0f;
        audioFx2.volume = 0f;
    }
    public void OnSound()
    {
        audioFx.volume = 1f;
        audioFx2.volume = 0.14f;
    }
}