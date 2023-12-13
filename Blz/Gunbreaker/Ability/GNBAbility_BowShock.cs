using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Blz.Gunbreaker.Ability;

public class GNBAbility_BowShock : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!SpellsDefine.BowShock.IsReady()) return -1;
        if (!Qt.GetQt("±¬·¢"))
            return -2;
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange + 2) return -1;
        if (SpellsDefine.NoMercy.CoolDownInGCDs(2)) return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.BowShock.GetSpell().Id).GetSpell());
    }
}