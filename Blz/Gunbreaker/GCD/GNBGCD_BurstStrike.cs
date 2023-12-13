using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Blz.Gunbreaker.GCD;

public class GNBGCD_BurstStrike : ISlotResolver
{
    private Spell GetSpell()
    {
        if (Qt.GetQt("AOE"))
        {
            var aoeCount = TargetHelper.GetNearbyEnemyCount(5);
            if (aoeCount >= 2)
                if (SpellsDefine.FatedCircle.IsUnlock())
                    return SpellsDefine.FatedCircle.GetSpell();
        }

        return SpellsDefine.BurstStrike.GetSpell();
    }

    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -1;
        if (!Qt.GetQt("±¬·¢»÷"))
        {
            return -3;
        }
        if (Core.Me.HasMyAura(AurasDefine.NoMercy) && (SpellsDefine.Bloodfest.CoolDownInGCDs(3) || SpellsDefine.Bloodfest.IsReady())) return 0;
        if (Core.Get<IMemApiGunBreaker>().Ammo < 3 && Core.Me.HasMyAura(AurasDefine.NoMercy) && SpellsDefine.DoubleDown.CoolDownInGCDs(4)) return -1;
        if (Core.Me.HasMyAura(AurasDefine.NoMercy) && SpellsDefine.DoubleDown.CoolDownInGCDs(2)) return -1;
        if (Core.Me.HasMyAura(AurasDefine.NoMercy) && SpellsDefine.DoubleDown.CoolDownInGCDs(4)&& SpellsDefine.GnashingFang.CoolDownInGCDs(3)&& Core.Get<IMemApiSpell>().GetLastComboSpellId() != SpellsDefine.BrutalShell) return -1;
        if (Core.Get<IMemApiGunBreaker>().Ammo < 3 && Core.Me.HasMyAura(AurasDefine.NoMercy) && SpellsDefine.GnashingFang.CoolDownInGCDs(4)) return -1;
        if (Core.Get<IMemApiGunBreaker>().Ammo > 0 && Core.Me.HasMyAura(AurasDefine.NoMercy) &&
            !SpellsDefine.DoubleDown.IsReady()) return 0;
        if (Core.Get<IMemApiGunBreaker>().Ammo > 0 && Core.Me.HasMyAura(AurasDefine.Medicated) &&
            !SpellsDefine.DoubleDown.IsReady()) return 0;
        if (Qt.GetQt("×îÖÕ±¬·¢") && Core.Get<IMemApiGunBreaker>().Ammo > 0) return 2;
        if (Core.Me.ClassLevel<88)
        {
            if (Core.Get<IMemApiGunBreaker>().Ammo < 2) return -1;
        }
        else
        {
            if (Core.Get<IMemApiGunBreaker>().Ammo < 3) return -1;
        }
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() != SpellsDefine.BrutalShell) return -1;
        return 0;
    }

    public void Build(Slot slot)
    {
        var spell = GetSpell();
        if (spell == null)
            return;
        slot.Add(spell);
    }
}