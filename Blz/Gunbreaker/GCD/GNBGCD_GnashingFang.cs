using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Blz.Gunbreaker.GCD;

public class GNBGCD_GnashingFang : ISlotResolver
{
    private bool CheckSpell()
    {
        if (Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.GnashingFang.GetSpell().Id).GetSpell()
            .IsReady()) return true;
        return false;
    }

    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        if (!CheckSpell())
            return -1;
        if (!Qt.GetQt("±¬·¢"))
            return -2;
        if (!Qt.GetQt("×Óµ¯Á¬"))
            return -1;
        if (Core.Me.ClassLevel >= 90)
        {
            if (SpellsDefine.DoubleDown.CoolDownInGCDs(3) && Core.Get<IMemApiGunBreaker>().Ammo < 2)
            {
                return -1;
            }
        }
        if (SpellsDefine.NoMercy.CoolDownInGCDs(2)) return -1;
        if (Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.GnashingFang.GetSpell().Id) == 16146)
            if (Core.Get<IMemApiGunBreaker>().Ammo <= 0)
                return -1;
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.GnashingFang.GetSpell().Id).GetSpell());
    }
}