using CombatRoutine;
using CombatRoutine.Opener;
using Common;
using Common.Define;
using Common.Helper;

namespace Blz.Gunbreaker;

public class OpenerGNB70_UwU : IOpener
{
    public int StartCheck()
    {
        //return -1;
        if (PartyHelper.NumMembers <= 4 && !Core.Me.GetCurrTarget().IsDummy())
            return -100;
        if (!SpellsDefine.NoMercy.IsReady())
            return -4;
        if (!SpellsDefine.BowShock.IsReady())
            return -4;
        if (!SpellsDefine.DangerZone.IsReady())
            return -4;
        if (Core.Get<IMemApiZoneInfo>().GetCurrTerrId()!=777) return -3;
        return 0;
    }

    public int StopCheck(int index)
    {
        return -1;
    }

    public List<Action<Slot>> Sequence { get; } = new()
    {
        Step0,
        Step1,
        Step2,
        Step3,
        Step4,
        Step5,
        Step6,
        Step8,
    };

    public Action CompeltedAction { get; set; }


    public int StepCount => 6;

    private static void Step0(Slot slot)
    {
        Core.Get<IMemApiMove>().SetPos(Core.Me.GetCurrTarget().front());
        slot.Add(new Spell(SpellsDefine.KeenEdge, SpellTargetType.Target));//1
        slot.Add(new Spell(SpellsDefine.Reprisal, SpellTargetType.Target));//雪仇
        
    }


    private static void Step1(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.BrutalShell, SpellTargetType.Target));//2
        if (Qt.GetQt("爆发药")) slot.Add(Spell.CreatePotion());//爆发药
    }


    private static void Step2(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.SolidBarrel, SpellTargetType.Target));//3
        slot.Add(new Spell(SpellsDefine.RoughDivide, SpellTargetType.Target));//粗分斩
    }

    private static void Step3(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.KeenEdge, SpellTargetType.Target));//1
    }
    private static void Step4(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.BrutalShell, SpellTargetType.Target));//2
        slot.Add(new SlotAction(SlotAction.WaitType.WaitForSndHalfWindow, 0, SpellsDefine.NoMercy.GetSpell()));//无情
    }
    private static void Step5(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.GnashingFang, SpellTargetType.Target));//子弹连1
        slot.Add(new Spell(SpellsDefine.JugularRip, SpellTargetType.Target));//子弹连1续剑
        slot.Add(new Spell(SpellsDefine.DangerZone, SpellTargetType.Target));//危险领域
    }
    private static void Step6(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.SonicBreak, SpellTargetType.Target));//音速破
        slot.Add(new Spell(SpellsDefine.RoughDivide, SpellTargetType.Target));//粗分斩
    }
    private static void Step7(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.SonicBreak, SpellTargetType.Target));//音速破
        slot.Add(new Spell(SpellsDefine.RoughDivide, SpellTargetType.Target));//粗分斩
    }
    private static void Step8(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.SavageClaw, SpellTargetType.Target));//子弹连2
        slot.Add(new Spell(SpellsDefine.AbdomenTear, SpellTargetType.Target));//子弹连2续剑
        slot.Add(new Spell(SpellsDefine.Rampart, SpellTargetType.Self));//铁壁
    }

    public uint Level { get; } = 70;

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        //countDownHandler.AddPotionAction(1500);
        if (Core.Me.HasAura(AurasDefine.RoyalGuard))
        {
            countDownHandler.AddAction(4000, 32068, SpellTargetType.Self);
        }
        countDownHandler.AddAction(GNBSettings.Instance.Time, () => GNBSpellHelper.GetOpen());
    }
}