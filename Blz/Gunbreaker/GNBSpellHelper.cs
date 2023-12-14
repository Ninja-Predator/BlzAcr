using CombatRoutine;
using CombatRoutine.Setting;
using Common;
using Common.Define;
using Common.Helper;

namespace Blz.Gunbreaker;

public static class GNBSpellHelper
{
    public static Spell? GetOpen()
    {
        if (GNBSettings.Instance.TP && Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > 
            SettingMgr.GetSetting<GeneralSettings>().AttackRange && 
            (Core.Me.GetCurrTarget().IsBoss()||Core.Me.GetCurrTarget().IsDummy()))
            Core.Get<IMemApiMove>().SetPos(Core.Me.GetCurrTarget().canattackpos());
        if (Qt.GetQt("起手突进开怪")) return SpellsDefine.RoughDivide.GetSpell();
        return null;
    }
}