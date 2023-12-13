using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Blz.Gunbreaker.GCD;

public class GNBGCD_DoubleDown : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        if (!Core.Me.HasMyAura(AurasDefine.NoMercy)) return -1;
        if (!SpellsDefine.DoubleDown.IsReady()) return -1;
        if (Core.Get<IMemApiGunBreaker>().Ammo < 2) return -1;
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange + 2) return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.DoubleDown.GetSpell().Id).GetSpell());
    }
}