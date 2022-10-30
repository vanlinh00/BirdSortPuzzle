using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayer
{
    private const string ALL_DATA = "all_data";
    private static InforPlayer inforPlayer;
    static DataPlayer()
    {
        inforPlayer = JsonUtility.FromJson<InforPlayer>(PlayerPrefs.GetString(ALL_DATA));
        if (inforPlayer == null)
        {
            inforPlayer = new InforPlayer
            {
                isOnMusicBg = true,
                isOnBravible = true,
                countCoins = 0,
                isOnSound = true,
                listIdBg = new List<int>() { 1 },
                listIdBirds = new List<int>() { 1 },
                listIdBranchs = new List<int>() { 1 },
                idCurrentBranchLoading=1,
                idCurrentBgsLoading=1,
                idCurrentBirdsLoading=1,
            };
            SaveData();
        }
    }
    private static void SaveData()
    {
        var data = JsonUtility.ToJson(inforPlayer);
        PlayerPrefs.SetString(ALL_DATA, data);
    }
    public static void ChangeBackGroundLoading(int idBg)
    {
        inforPlayer.idCurrentBgsLoading = idBg;
        SaveData();
    }
    public static void ChangeBranchLoading(int idBranch)
    {
        inforPlayer.idCurrentBranchLoading = idBranch;
        SaveData();
    }
    public static void ChangeBirdLoading(int idBird)
    {
        inforPlayer.idCurrentBirdsLoading = idBird;
        SaveData();
    }
    public static void UpdateAmountCoins(int Amount)
    {
        inforPlayer.countCoins = Amount;
        SaveData();
    }
    public static void ChangeStateAudio(bool IsOnAudio)
    {
        inforPlayer.isOnMusicBg = IsOnAudio;
        SaveData();
    }
    public static void ChangeStateSound(bool IsOnAudio)
    {
        inforPlayer.isOnSound = IsOnAudio;
        SaveData();
    }
    public static void ChangeStateBravible(bool IsOnBravible)
    {
        inforPlayer.isOnBravible = IsOnBravible;
        SaveData();
    }
    public static void AddNewIdBg(int IdBg)
    {
        inforPlayer.listIdBg.Add(IdBg);
        SaveData();
    }
    public static void AddNewIdBirds(int IdBird)
    {
        inforPlayer.listIdBirds.Add(IdBird);
        SaveData();

    }
    public static void AddNewlistIdBranchs(int IdBranch)
    {
        inforPlayer.listIdBranchs.Add(IdBranch);
        SaveData();

    }

    public static InforPlayer GetInforPlayer()
    {
        return inforPlayer;
    }
}
public class InforPlayer
{
    public bool isOnMusicBg;
    public bool isOnSound;
    public bool isOnBravible;
    public int countCoins;
    public List<int> listIdBg;
    public List<int> listIdBirds;
    public List<int> listIdBranchs;
    public int idCurrentBranchLoading;
    public int idCurrentBgsLoading;
    public int idCurrentBirdsLoading;
}