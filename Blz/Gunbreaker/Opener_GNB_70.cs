using CombatRoutine;
using CombatRoutine.Opener;
using Common;
using Common.Define;
using Common.Helper;


namespace Blz.Gunbreaker;

public class OpenerGNB70_Ucob : IOpener
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
        if (Core.Get<IMemApiZoneInfo>().GetCurrTerrId()!=733) return -3;
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
    };

    public Action CompeltedAction { get; set; }


    public int StepCount => 6;

    private static void Step0(Slot slot)
    {
        Core.Get<IMemApiMove>().SetPos(Core.Me.GetCurrTarget().front());
        slot.Add(new Spell(SpellsDefine.KeenEdge, SpellTargetType.Target));
        if (Qt.GetQt("爆发药")) slot.Add(Spell.CreatePotion());
    }


    private static void Step1(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.BrutalShell, SpellTargetType.Target));
        if (Core.Get<IMemApiZoneInfo>().GetCurrTerrId()==733)
        {
            slot.Add(new Spell(SpellsDefine.HeartofLight, SpellTargetType.Self));
        }
        slot.Add(new SlotAction(SlotAction.WaitType.WaitForSndHalfWindow, 0, SpellsDefine.NoMercy.GetSpell()));
    }


    private static void Step2(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.SonicBreak, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.BowShock, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.DangerZone, SpellTargetType.Target));
        
    }


    private static void Step3(Slot slot)
    {
        slot.Add(new Spell(SpellsDefine.SolidBarrel, SpellTargetType.Target));
        slot.Add(new Spell(SpellsDefine.RoughDivide, SpellTargetType.Target));
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