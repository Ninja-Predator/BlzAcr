using CombatRoutine;
using CombatRoutine.Opener;
using Common;
using Common.Define;
using Common.Helper;

namespace Blz.Gunbreaker;

public class OpenerGNB90 : IOpener
{
    public int StartCheck()
    {
        //return -1;
        if (!Core.Me.GetCurrTarget().IsBoss() && !Core.Me.GetCurrTarget().IsDummy())
            return -100;
        if (!SpellsDefine.NoMercy.IsReady())
            return -4;
        if (!SpellsDefine.Bloodfest.IsReady())
            return -4;
        if (Core.Get<IMemApiGunBreaker>().Ammo > 0)
            return -1;
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
        Step7,
        Step8
    };

    public Action CompeltedAction { get; set; }


    public int StepCount => 6;

    private static void Step0(Slot slot)
    {
        /*        if (Qt.GetQt("TP开怪")&& Core.Me.DistanceMelee(Core.Me.GetCurrTarget()) > SettingMgr.GetSetting<GeneralSettings>().AttackRange)
                    Core.Get<IMemApiMove>().SetPos(Core.Me.GetCurrTarget().front());*/
        slot.Add(new Spell(SpellsDefine.KeenEdge, SpellTargetType.Target));
        slot.Add(new SlotAction(SlotAction.WaitType.WaitInMs, 800, SpellsDefine.NoMercy.GetSpell()));
    }


    private static void Step1(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.BrutalShell, SpellTargetType.Target));
        if (Qt.GetQt("爆发药")) slot.Add(Spell.CreatePotion());
    }


    private static void Step2(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.SolidBarrel, SpellTargetType.Target));
        if (GNBSettings.Instance.RoughDivideNoMove)
            Core.Get<IMemApiHack>().ChangeHack("技能无位移 (Lv4)", true);
        if (SpellsDefine.RoughDivide.IsReady()) slot.Add(new Spell(SpellsDefine.RoughDivide, SpellTargetType.Target));
    }


    private static void Step3(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.BurstStrike, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.Bloodfest, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.Hypervelocity, SpellTargetType.Target));
    }


    private static void Step4(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.GnashingFang, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.BlastingZone, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.JugularRip, SpellTargetType.Target));
    }


    private static void Step5(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.SonicBreak, SpellTargetType.Target));
        if (SpellsDefine.RoughDivide.IsReady())
        {
            if (GNBSettings.Instance.RoughDivideNoMove)
                Core.Get<IMemApiHack>().ChangeHack("技能无位移 (Lv4)", true);
            slot.Add(new Spell(SpellsDefine.RoughDivide, SpellTargetType.Target));
        }
        slot.Add(new Spell(SpellsDefine.BowShock, SpellTargetType.Target));
    }

    private static void Step6(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.DoubleDown, SpellTargetType.Target));
        if (SpellsDefine.RoughDivide.IsReady())
        {
            if (GNBSettings.Instance.RoughDivideNoMove)
                Core.Get<IMemApiHack>().ChangeHack("技能无位移 (Lv4)", true);
            slot.Add(new Spell(SpellsDefine.RoughDivide, SpellTargetType.Target));
        }
    }

    private static void Step7(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.SavageClaw, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.AbdomenTear, SpellTargetType.Target));
    }

    private static void Step8(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.WickedTalon, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.EyeGouge, SpellTargetType.Target));
    }

    public uint Level { get; } = 90;

    public void InitCountDown(CountDownHandler countDownHandler)
    {
        countDownHandler.AddAction(GNBSettings.Instance.Time, () => GNBSpellHelper.GetOpen());
    }
}