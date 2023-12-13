using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;


namespace Blz.Gunbreaker.Ability;

public class GNBAbility_Bloodfest : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!SpellsDefine.Bloodfest.IsReady()) return -1;
        if (!Qt.GetQt("±¬·¢"))
            return -2;
        if (Core.Get<IMemApiGunBreaker>().Ammo > 0) return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Bloodfest.GetSpell().Id).GetSpell());
    }
}