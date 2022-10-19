using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
   public GamePlay _gamePlay;
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
        StackStateUndos.Clear();
        gameState = GameState.SortBirds;
        level = Random.RandomRange(1, 4);
        StartCoroutine(_gamePlay.WaitTimeLoadData(level));
    }
    int numberUndo = 1;

    public void Undo()
    {
        numberUndo = 1;
        if (StackStateUndos.Count != 0)
        {
            StateUndo StateBirdUndos = StackStateUndos.Peek();
            int IdOldBranch = StateBirdUndos.listStateBirdUndo[StateBirdUndos.listStateBirdUndo.Count - 1].idOldBranch;
            int IdNexBranch = StateBirdUndos.listStateBirdUndo[StateBirdUndos.listStateBirdUndo.Count - 1].idNextBranch;
            bool IsChangeSeats = StateBirdUndos.listStateBirdUndo[StateBirdUndos.listStateBirdUndo.Count - 1].isChangeSeats;

            if (IsChangeSeats)
            {
                _gamePlay.ListAllBranchs[IdOldBranch - 1].listBirds.Clear();
                _gamePlay.ListAllBranchs[IdOldBranch - 1].listBirds = StateBirdUndos.listBirdsChangeState;
            }
            else
            {
                if (IdOldBranch == IdNexBranch)
                {
                    numberUndo = 2;
                }
            }
        }
        for (int i = 0; i < numberUndo; i++)
        {
            StartCoroutine(WaitTimeUndo());
        }
    }

    IEnumerator WaitTimeUndo()
    {
        if (StackStateUndos.Count != 0)
        {
            float timeWaitBirdMove;
            StateUndo StateBirdUndos = StackStateUndos.Pop();
            int IdOldBranch;
            int IdNexBranch;
            bool IsChangeSeats;
            Vector3 PosOldSlot;

            for (int i = StateBirdUndos.listStateBirdUndo.Count - 1; i >= 0; i--)
            {
                timeWaitBirdMove = Random.RandomRange(0.05f, 0.1f);
                IdOldBranch = StateBirdUndos.listStateBirdUndo[i].idOldBranch;
                IdNexBranch = StateBirdUndos.listStateBirdUndo[i].idNextBranch;
                IsChangeSeats = StateBirdUndos.listStateBirdUndo[StateBirdUndos.listStateBirdUndo.Count - 1].isChangeSeats;
                PosOldSlot = StateBirdUndos.listStateBirdUndo[i].posOldSlot;
                if (IsChangeSeats)
                {

                }
                else
                {
                    _gamePlay.ListAllBranchs[IdOldBranch - 1].AddToListBrids(StateBirdUndos.listStateBirdUndo[i].bird);
                    if (IdOldBranch != IdNexBranch && numberUndo == 1)
                    {
                        _gamePlay.ListAllBranchs[IdNexBranch - 1].DeleteLastBird();
                    }
                }

                StartCoroutine(MoveBirdsToOldBranch(IdNexBranch - 1, IdOldBranch - 1, StateBirdUndos.listStateBirdUndo[i].bird, PosOldSlot, timeWaitBirdMove));
                yield return new WaitForSeconds(timeWaitBirdMove);

                //yield return new WaitForSeconds(0f);
            }
        }

    }
 public   IEnumerator MoveBirdsToOldBranch(int IndexCurrentBranch, int IndexNextBranch, Bird Bird, Vector3 PosOldSlot,float timeWaitBirdMove)
    {

        Bird.transform.parent = null;
        Bird.ParentObj = _gamePlay.ListAllBranchs[IndexNextBranch]._animator.gameObject;
        bool IsMoveDown = (_gamePlay.ListAllBranchs[IndexCurrentBranch].id % 2 == _gamePlay.ListAllBranchs[IndexNextBranch].id % 2) ? true : false;

        float DistanceBirdMove = Vector3.Distance(Bird.transform.position, PosOldSlot);
        float TimeMove = 0f;
        if (IndexCurrentBranch!= IndexNextBranch)
        {
            TimeMove = 0.44f * DistanceBirdMove / 1.863758f;
            if (IsMoveDown)
            {
                if (Bird.transform.position.x <= PosOldSlot.x)
                {
                    if (_gamePlay.ListAllBranchs[IndexCurrentBranch].id % 2 == 0)
                    {
                        Bird.FlipX();
                        Bird.ChangeSeats(PosOldSlot, true, TimeMove);
                    }
                    else
                    {
                        if (Bird.transform.position.x == PosOldSlot.x)
                        {
                            Bird.FlipX();
                            Bird.ChangeSeats(PosOldSlot, true, TimeMove);
                        }
                        Bird.ChangeSeats(PosOldSlot, false, TimeMove);
                    }
                }
                else
                {
                    if (_gamePlay.ListAllBranchs[IndexCurrentBranch].id % 2 == 0)
                    {
                        Bird.ChangeSeats(PosOldSlot, false, TimeMove);
                    }
                    else
                    {
                        Bird.FlipX();
                        Bird.ChangeSeats(PosOldSlot, true, TimeMove);
                    }
                }
            }
            else
            {
                Bird.ChangeSeats(PosOldSlot, true, TimeMove);
            }
        }
        else
        {
            TimeMove = 0.7f;
            Bird.ChangeSeats(PosOldSlot, false, TimeMove);
        }
        yield return new WaitForSeconds(timeWaitBirdMove - 0.12f);
        // _gamePlay.ListAllBranchs[IndexNextBranch].StateShaky();
        // yield return new WaitForSeconds(0.15f);
        //_gamePlay.ListAllBranchs[IndexNextBranch].StateIdle();
        // yield return new WaitForSeconds(0.2f);
    }
    public void ReNewGame()
    {
        _gamePlay.Renew();
    }

}
