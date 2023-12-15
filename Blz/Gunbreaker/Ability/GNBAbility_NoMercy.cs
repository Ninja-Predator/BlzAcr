using CombatRoutine;
using Common;
using Common.Define;

namespace Blz.Gunbreaker.Ability;

public class GNBAbility_NoMercy : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!SpellsDefine.NoMercy.IsReady()) return -1;
        if (!Qt.GetQt("±¬·¢"))
            return -2;
        if (AI.Instance.GetGCDCooldown() > 1600) return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.NoMercy.GetSpell().Id).GetSpell());
    }
}