using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
public class GameManager : Singleton<GameManager>
{
    public GamePlay _gamePlay;
    [SerializeField] int level;
    public int CountNumberShuffle = 0;
    protected override void Awake()
    {
        base.Awake();
    }
    public enum GameState
    {
        SortBirds,
        ChangeSeatsBirds,
        FinishGame,
        PauseGame,
    }
    public GameState gameState;

    private void Start()
    {
      ReplayLevel();
    }

    public Stack<StateUndo> StackStateUndos = new Stack<StateUndo>();
    public void ReplayLevel()
    {
        StackStateUndos.Clear();
        gameState = GameState.SortBirds; 
        StartCoroutine(_gamePlay.WaitTimeLoadData(level));

    }
    public void NextLevel()
    {
        level++;
        StartCoroutine(WaitTimeRenew());
    }
    public int  Getlevel()
    {
        return level;
    }
    public  IEnumerator WaitTimeRenew()
    {
         ReNewGame();
        yield return new WaitForSeconds(0.1f);
         ReplayLevel();
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
                _gamePlay.ListAllBranchs[IdOldBranch - 1].SetOrderBirdsAndBrands(20);
            }
            else
            {
                if (IdOldBranch == IdNexBranch)
                {
                    _gamePlay.AmountListBirdsFinishGame++;
                    numberUndo = 2;
                }
            }
        }
        for(int i=0;i<numberUndo;i++)
        {
            UiGamePlay._instance.MinusNumberUndo();
            StartCoroutine(WaitTimeUndo());
        }    
    }
    IEnumerator WaitTimeUndo()
    {
        if (StackStateUndos.Count != 0)
        {
            float timeWaitBirdMove;
            StateUndo StateBirdUndos = StackStateUndos.Pop();
            int IdOldBranch = StateBirdUndos.listStateBirdUndo[0].idOldBranch;
            int IdNexBranch= StateBirdUndos.listStateBirdUndo[0].idNextBranch;
            bool IsChangeSeats= StateBirdUndos.listStateBirdUndo[StateBirdUndos.listStateBirdUndo.Count - 1].isChangeSeats;
            int  IdSlot = 0;
            Vector3 PosOldSlot;

            for (int i = StateBirdUndos.listStateBirdUndo.Count - 1; i >= 0; i--)
            {
                PosOldSlot = StateBirdUndos.listStateBirdUndo[i].posOldSlot;
                IdSlot = StateBirdUndos.listStateBirdUndo[i].idSlot;
                StateBirdUndos.listStateBirdUndo[i].bird.idSlot = IdSlot;
                Debug.Log("Day la comeback" + IdSlot);
                if (IsChangeSeats)
                {
                    StateBirdUndos.listStateBirdUndo[i].bird.idBranchStand = IdNexBranch - 1;
                    StateBirdUndos.listStateBirdUndo[i].bird.ChangeSeats(PosOldSlot, false);

                }
                else
                {
                    _gamePlay.ListAllBranchs[IdOldBranch - 1].AddToListBrids(StateBirdUndos.listStateBirdUndo[i].bird);
                    if (IdOldBranch != IdNexBranch && numberUndo == 1)
                    {
                        _gamePlay.ListAllBranchs[IdNexBranch - 1].DeleteLastBird();
                    }
                    StartCoroutine(MoveBirdsToOldBranch(IdNexBranch - 1, IdOldBranch - 1, StateBirdUndos.listStateBirdUndo[i].bird, PosOldSlot));
                    yield return new WaitForSeconds(Random.RandomRange(0.05f, 0.1f));
                }
            }

        }

    }
    int OrderLayer = 40;
 public   IEnumerator MoveBirdsToOldBranch(int IndexCurrentBranch, int IndexNextBranch, Bird Bird, Vector3 PosOldSlot)
    {
        GameManager._instance._gamePlay.IsBirdMoving = true;
        Bird.transform.parent = null;
        OrderLayer++;
        Bird.SetOrderLayer(OrderLayer);
        Bird.ParentObj = _gamePlay.ListAllBranchs[IndexNextBranch]._animator.gameObject;
        bool IsMoveDown = (_gamePlay.ListAllBranchs[IndexCurrentBranch].id % 2 == _gamePlay.ListAllBranchs[IndexNextBranch].id % 2) ? true : false;
        int idNextBranch = _gamePlay.ListAllBranchs[IndexNextBranch].id-1;
        Bird.idBranchStand = idNextBranch;
        Bird.isMoveNextBranch = true;
        float DistanceBirdMove = Vector3.Distance(Bird.transform.position, PosOldSlot);

        if (IsMoveDown)
          {
            if (IndexCurrentBranch == IndexNextBranch)
            {
                Bird.TimeMove = 0.8f;
            }
            else
            {
                Bird.TimeMove = 0.7f * DistanceBirdMove / 2.5f;

            }

            if (Bird.transform.position.x <= PosOldSlot.x)
                {
                    if (_gamePlay.ListAllBranchs[IndexCurrentBranch].id % 2 == 0)
                    {
                        Bird.FlipX();
                        Bird.ChangeSeats(PosOldSlot, true);
                    }
                    else
                    {
                        if (Bird.transform.position.x == PosOldSlot.x)
                        {
                            Bird.FlipX();
                            Bird.ChangeSeats(PosOldSlot, true);
                        }
                        Bird.ChangeSeats(PosOldSlot, false);
                    }
                }
                else
                {
                    if (_gamePlay.ListAllBranchs[IndexCurrentBranch].id % 2 == 0)
                    {
                        Bird.ChangeSeats(PosOldSlot, false);
                    }
                    else
                    {
                        Bird.FlipX();
                        Bird.ChangeSeats(PosOldSlot, true);
                    }
                }
            }
            else
            {
              if (IndexCurrentBranch == IndexNextBranch)
              {
                Bird.TimeMove = 0.5f;
              }
              else
              {
                Bird.TimeMove = 0.55f * DistanceBirdMove / 2.5f;
                }
            Bird.ChangeSeats(PosOldSlot, true);
            }
        yield return new WaitForSeconds(1f);
        _gamePlay.ListAllBranchs[IndexNextBranch].SetOrderBirdsAndBrands(20);
        GameManager._instance._gamePlay.IsBirdMoving = false;
    }
    public void ReNewGame()
    {
        _gamePlay.Renew();
    }
    public void AddCountNumberShuffle()
    {
       // CountNumberShuffle +;
    }
}
