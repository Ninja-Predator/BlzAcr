using CombatRoutine;
using CombatRoutine.Setting;
using Common.Define;

namespace Blz.Gunbreaker;

public class GNBRotationEventHandler : IRotationEventHandler
{
    public void OnResetBattle()
    {
        GNBBattleData.Instance.Reset();
        if (SettingMgr.GetSetting<AutoResetSetting>().ResetButton)
        {
            Qt.Reset();
            BlzOptions.Instance.Reset();
        }
    }

    public Task OnPreCombat()
    {
        return Task.CompletedTask;
    }

    public Task OnNoTarget()
    {
        return Task.CompletedTask;
    }

    public void AfterSpell(Slot slot, Spell spell)
    {
        switch (spell.Id)
        {
            case SpellsDefine.NoMercy:
                AI.Instance.BattleData.LimitAbility = true;
                break;
            default:
                AI.Instance.BattleData.LimitAbility = false;
                break;
        }
    }

    public void OnBattleUpdate(int currTime)
    {
    }
}