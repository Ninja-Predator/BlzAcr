using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Blz.Gunbreaker.Ability;

public class GNBAbility_DangerZone : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!SpellsDefine.DangerZone.IsReady()) return -1;
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -1;
        if (!Qt.GetQt("±¬·¢"))
            return -2;
        if (SpellsDefine.NoMercy.CoolDownInGCDs(2)) return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.DangerZone.GetSpell().Id).GetSpell());
    }
}