using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Blz.Gunbreaker.GCD;

public class GNBGCD_BaseGCD : ISlotResolver
{
    private Spell GetSpell()
    {
        if (Qt.GetQt("AOE"))
        {
            var aoeCount = TargetHelper.GetNearbyEnemyCount(5);
            if (aoeCount >= 2 && (Core.Get<IMemApiSpell>().GetLastComboSpellId() == SpellsDefine.DemonSlice ||
                                  (Core.Get<IMemApiSpell>().GetLastComboSpellId() != SpellsDefine.BrutalShell &&
                                   Core.Get<IMemApiSpell>().GetLastComboSpellId() != SpellsDefine.KeenEdge)))
            {
                if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == SpellsDefine.DemonSlice)
                    return SpellsDefine.DemonSlaughter.GetSpell();
                return SpellsDefine.DemonSlice.GetSpell();
            }
        }
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == SpellsDefine.KeenEdge)
        {
            if(Core.Me.ClassLevel < 88) {
                if (Core.Get<IMemApiGunBreaker>().Ammo == 2)
                {
                    if (SpellsDefine.NoMercy.GetSpell().CoolDownInGCDs(2) && !SpellsDefine.NoMercy.GetSpell().CoolDownInGCDs(1) && Qt.GetQt("±¬·¢"))
                        return SpellsDefine.KeenEdge.GetSpell();
                }
            }
            else
            {
                if (Core.Get<IMemApiGunBreaker>().Ammo == 3)
                {
                    if (SpellsDefine.NoMercy.GetSpell().CoolDownInGCDs(2) && !SpellsDefine.NoMercy.GetSpell().CoolDownInGCDs(1) && Qt.GetQt("±¬·¢"))
                        return SpellsDefine.KeenEdge.GetSpell();
                }
            }
            
        }
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == SpellsDefine.BrutalShell)
            return SpellsDefine.SolidBarrel.GetSpell();
        if (Core.Get<IMemApiSpell>().GetLastComboSpellId() == SpellsDefine.KeenEdge)
            return SpellsDefine.BrutalShell.GetSpell();
        return SpellsDefine.KeenEdge.GetSpell();
    }

    public SlotMode SlotMode { get; } = SlotMode.Gcd;

    public int Check()
    {
        if (Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) >
            SettingMgr.GetSetting<GeneralSettings>().AttackRange) return -1;
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