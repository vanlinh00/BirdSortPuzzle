using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GamePlay _gamePlay;
    int CountBrach = 0;
    [SerializeField] int level;

    protected override void Awake()
    {
        base.Awake();
    }
    public enum GameState
    {
        SortBirds,
        ChangeSeatsBirds,
    }
    public GameState gameState;

    private void Start()
    {
        ReplayLevel();
    }

    public Stack<StateUndo> StackStateUndos = new Stack<StateUndo>();
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
        gameState = GameState.SortBirds;
        level = /*2*/ Random.RandomRange(1, 4);
        StartCoroutine(_gamePlay.WaitTimeLoadData(level));
    }

    public void Undo()
    {
        if (StackStateUndos.Count != 0)
        {
            StateUndo StateBirdUndos = StackStateUndos.Pop();
            int IdOldBranch;
            int IdNexBranch;
            for (int i = 0; i < StateBirdUndos.listStateBirdUndo.Count; i++)
            {
                IdOldBranch = StateBirdUndos.listStateBirdUndo[i].idOldBranch;
                IdNexBranch = StateBirdUndos.listStateBirdUndo[i].idNextBranch;
                Vector3 PosOldSlot = StateBirdUndos.listStateBirdUndo[i].posOldSlot;

                // StateBirdUndos.listStateBirdUndo[i].bird.MoveToTarget(PosOldSlot, false);
                StartCoroutine(MoveBirdsToOldBranch(IdNexBranch-1, IdOldBranch-1, StateBirdUndos.listStateBirdUndo[i].bird, PosOldSlot));
                _gamePlay.ListAllBranchs[IdOldBranch - 1].AddToListBrids(StateBirdUndos.listStateBirdUndo[i].bird);
                _gamePlay.ListAllBranchs[IdNexBranch - 1].DeleteLastBird();
            }
        }
    }
    IEnumerator MoveBirdsToOldBranch(int IndexCurrentBranch,int IndexNextBranch, Bird Bird, Vector3 PosOldSlot)
    {
            float timeWaitBirdMove;
            Bird.transform.parent = _gamePlay.ListAllBranchs[IndexNextBranch]._animator.gameObject.transform;
            bool IsMoveDown = (_gamePlay.ListAllBranchs[IndexCurrentBranch].id % 2 == _gamePlay.ListAllBranchs[IndexNextBranch].id % 2) ? true : false;

            if (IsMoveDown)
            {
                if (Bird.transform.position.x <= PosOldSlot.x)
                {
                    if (_gamePlay.ListAllBranchs[IndexCurrentBranch].id % 2 == 0)
                    {
                       Bird.FlipX();
                       Bird.MoveToTarget(PosOldSlot, true);
                    }
                    else
                    {
                        if (Bird.transform.position.x == PosOldSlot.x)
                        {
                          Bird.FlipX();
                          Bird.MoveToTarget(PosOldSlot, true);
                        }
                       Bird.MoveToTarget(PosOldSlot, false);
                    }
                }
                else
                {
                    if (_gamePlay.ListAllBranchs[IndexCurrentBranch].id % 2 == 0)
                    {
                      Bird.MoveToTarget(PosOldSlot, false);
                    }
                    else
                    {
                     Bird.FlipX();
                     Bird.MoveToTarget(PosOldSlot, true);
                    }

                }
            }
            else
            {
                 Bird.MoveToTarget(PosOldSlot, true);
            }
        timeWaitBirdMove = Random.RandomRange(0.05f, 0.1f);
        yield return new WaitForSeconds(timeWaitBirdMove);

        yield return new WaitForSeconds(timeWaitBirdMove + 0.54f);
       _gamePlay.ListAllBranchs[IndexNextBranch].StateShaky();
        yield return new WaitForSeconds(0.4f);
       _gamePlay.ListAllBranchs[IndexNextBranch].StateIdle();

    }
}
