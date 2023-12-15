using CombatRoutine.TriggerModel;
using Common.Language;

namespace Blz.Machinist.Triggers;

public class TriggerActionSwitchButton : ITriggerAction
{
    public enum ButtonType
    {
        枪刃_闪雷弹,
        枪刃_突进开关,
        枪刃_子弹连,
        枪刃_爆发击,

    }
    public ButtonType button { get; set; }
    public bool Open { get; set; }
    public string DisplayName => "[Blz枪刃]切换开关状态".Loc();
    public string Remark { get; set; }
    public void Check()
    {
    }

    public bool Draw()
    {
        return false;
    }

    public bool Handle()
    {
        switch (button)
        {
            case ButtonType.枪刃_闪雷弹:
                BlzOptions.Instance.LightningShot = Open;
                break;
            case ButtonType.枪刃_突进开关:
                BlzOptions.Instance.ifRoughDivide = Open;
                break;
            case ButtonType.枪刃_爆发击:
                BlzOptions.Instance.BurstStrike = Open;
                break;
            case ButtonType.枪刃_子弹连:
                BlzOptions.Instance.GnashingFang = Open;
                break;
        }
        return true;
    }
}





