using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Blz.Gunbreaker.Ability;

public class GNBAbility_Continuation : ISlotResolver
{
    public bool CheckSpell()
    {
        if (Core.Me.HasAura(AurasDefine.ReadytoBlast)) return true;
        if (Core.Me.HasAura(AurasDefine.ReadytoGouge)) return true;
        if (Core.Me.HasAura(AurasDefine.ReadytoRip)) return true;
        if (Core.Me.HasAura(AurasDefine.ReadytoTear)) return true;
        return false;
    }

    public SlotMode SlotMode { get; } = SlotMode.OffGcd;

    public int Check()
    {
        if (!CheckSpell()) return -1;
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange + 2) return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        slot.Add(Core.Get<IMemApiSpell>().CheckActionChange(SpellsDefine.Continuation.GetSpell().Id).GetSpell());
    }
}