namespace Blz;

public class BlzOptions
{
    public static BlzOptions Instance = new();

    public void Reset()
    {
        Instance = new BlzOptions();
    }
    public bool RoughDivide = true;
    public bool ifRoughDivide = true;
    public bool LightningShot = true;
    public bool GnashingFang = true;
    public bool BurstStrike = true;
    public bool UsePotion = true;
}