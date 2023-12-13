using CombatRoutine;
using Common.Define;

namespace Blz.Gunbreaker;

public static class GNBSpellHelper
{
    public static Spell GetOpen()
    {
        if (Qt.GetQt("ͻ������")) return SpellsDefine.RoughDivide.GetSpell();
        return SpellsDefine.LightningShot.GetSpell();
    }
}