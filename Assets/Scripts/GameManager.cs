using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] GamePlay _gamePlay;
    int CountBrach = 0;
    [SerializeField] int level;

    private void Start()
    {
        ReplayLevel();
    }
    public void ReplayLevel()
    {
        /// cho nay chua chuan load level thi no co canh CountBranch luon roi
        //if (level == 1)
        //{
        //    CountBrach = 2;
        //}
        //if (level == 2 || level == 3)
        //{
        //    CountBrach = 5;
        //}
        level = Random.RandomRange(1, 4);
        StartCoroutine(_gamePlay.WaitTimeLoadData(level));
    }
}
