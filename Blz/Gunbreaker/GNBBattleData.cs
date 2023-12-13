namespace Blz.Gunbreaker;

public class GNBBattleData
{
    public static GNBBattleData Instance = new();

    public void Reset()
    {
        Instance = new GNBBattleData();
    }
}