namespace Blz.Gunbreaker;

public class GNBBattleData
{
    public static GNBBattleData Instance = new();
    public bool isRoughDivideInQueue = false;

    public void Reset()
    {
        Instance = new GNBBattleData();
    }
}