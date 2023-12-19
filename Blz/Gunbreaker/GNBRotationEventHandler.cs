using CombatRoutine;
using CombatRoutine.Setting;
using Common;
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
            case SpellsDefine.RoughDivide:
                if (GNBSettings.Instance.RoughDivideNoMove)
                    new Task(async () =>
                    {
                        await Task.Delay(1000);
                        Core.Get<IMemApiHack>().ChangeHack("技能无位移", false);
                        GNBBattleData.Instance.isRoughDivideInQueue = false;
                    }).Start();
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