using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;


namespace Blz.Gunbreaker.Ability;

public class GNBAbility_RoughDivide : ISlotResolver
{
    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!SpellsDefine.RoughDivide.IsReady()) return -1;
        if (Core.Get<IMemApiMove>().IsMoving()&&Qt.GetQt("�ƶ�ʱ��ͻ��")) return -1;
//        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > 0) return -2;
        if (!Qt.GetQt("ͻ������")) return -1;
        if (Qt.GetQt("ͻ��ȫ������") && !Core.Me.HasMyAura(AurasDefine.NoMercy)) return -1;
        if (Core.Me.HasMyAura(AurasDefine.NoMercy)) return 1;
        if (SpellsDefine.RoughDivide.GetSpell().Charges < 1.9)
            return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.RoughDivide.GetSpell().Id).GetSpell());
    }
}