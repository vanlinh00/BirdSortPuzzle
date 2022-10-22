[System.Serializable]
public class DataBirdOnBranchs
{
      public int AmountBranch;
     public BirdOnBranch[] BirdOnBranch;
}
[System.Serializable]
public class BirdOnBranch
{
    public int id;
    public int idBird;
    public int idBranch;
    public int slotBird;
}
