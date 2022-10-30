using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BirdSong
{ 
    public static void BirdSing(Bird BirdObj)
    {
        CheckSongBird(BirdObj);
    }
    public static void CheckSongBird(Bird BirdObj)
    {
        SoundType soundType;

        if(BirdObj.id==1)
        {
            BirdObj.Sing(SoundType.Birdtweet1);
        }
        else if(BirdObj.id == 2)
        {
            BirdObj.Sing(SoundType.Birdtweet2);
        }
        else if(BirdObj.id == 3)
        {
            BirdObj.Sing(SoundType.Birdtweet3);
        }
        else if (BirdObj.id == 4)
        {
            BirdObj.Sing(SoundType.Birdtweet4);
        }
        else if (BirdObj.id == 5)
        {
            BirdObj.Sing(SoundType.Birdtweet5);
        }
        else if (BirdObj.id == 6)
        {
            BirdObj.Sing(SoundType.Birdtweet6);
        }
        else if (BirdObj.id == 7)
        {
            BirdObj.Sing(SoundType.Birdtweet7);
        }
        else
        {
            if(Random.RandomRange(1,3)==2)
            {
                BirdObj.Sing(SoundType.Birdtweet4);
            }
            else
            {
                if (Random.RandomRange(1, 3) == 2)
                {
                    BirdObj.Sing(SoundType.Birdtweet2);
                }
                else
                {
                    if (Random.RandomRange(1, 3) == 2)
                    {
                        BirdObj.Sing(SoundType.Birdtweet3);
                    }
                    else
                    {
                        BirdObj.Sing(SoundType.Birdtweet5);
                    }
                }
            }
        }

    }
}
