using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Blz.Gunbreaker.GCD;

public class GNBGCD_LightningShot : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > SettingMgr.GetSetting<GeneralSettings>().AttackRange)
            if (Qt.GetQt("ÉÁÀ×µ¯"))
                return 0;
/*        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == SpellsDefine.BrutalShell)
            if (Core.Get<IMemApiGunBreaker>().Ammo == 3)
                if (SpellsDefine.NoMercy.GetSpell().CoolDownInGCDs(1) && Qt.GetQt("±¬·¢"))
                    return 0;*/
        return -1;
    }

    public void Build(Slot slot)
    {
        slot.Add(SpellsDefine.LightningShot.GetSpell());
    }
}