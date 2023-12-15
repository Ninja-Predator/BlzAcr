using Blz.Gunbreaker;
using Blz.Gunbreaker.Ability;
using Blz.Gunbreaker.GCD;
using Blz.Gunbreaker.Triggers;
using CombatRoutine;
using CombatRoutine.Opener;
using CombatRoutine.View.JobView;
using Common;
using Common.Define;
using Common.Language;

namespace Blz;

public class GunbreakerRotationEntry : IRotationEntry
{
    private GunbreakerOverlay _lazyOverlay = new();
    public static JobViewWindow JobViewWindow;
    public string OverlayTitle { get; } = "Blz GNB";

    public AcrType AcrType { get; } = AcrType.HighEnd;

    public string AuthorName { get; } = "Blz";
    public Jobs TargetJob { get; } = Jobs.Gunbreaker;

    public List<ISlotResolver> SlotResolvers = new()
    {
        new GNBGCD_SonicBreak(),
        new GNBGCD_DoubleDown(),
        new GNBGCD_LightningShot(),
        new GNBGCD_GnashingFang(),
        new GNBGCD_BurstStrike(),
        new GNBGCD_BaseGCD(),
        new GNBAbility_NoMercy(),
        new GNBAbility_Bloodfest(),
        new GNBAbility_Continuation(),
        new GNBAbility_BowShock(),
        new GNBAbility_DangerZone(),
        new GNBAbility_RoughDivide()
    };

    public Rotation Build(string settingFolder)
    {
        GNBSettings.Build(settingFolder);
        return new Rotation(this, () => SlotResolvers)
            .AddOpener(GetOpener)
            .SetRotationEventHandler(new GNBRotationEventHandler())
            .AddSettingUIs(new TankGNBSettingView())
            .AddSlotSequences()
            .AddTriggerAction(new BlzGNBQt());
    }

    private IOpener opener90 = new OpenerGNB90();
    private IOpener opener70_ucob = new OpenerGNB70_Ucob();
    private IOpener opener70_uwu = new OpenerGNB70_UwU();

    private IOpener? GetOpener(uint level)
    {
        if (level >= 90)
            return opener90;
        if (level >= 70)
        {
            if (Core.Get<IMemApiZoneInfo>().GetCurrTerrId() == 733)
            {
                return opener70_ucob;
            }

            if (Core.Get<IMemApiZoneInfo>().GetCurrTerrId() == 777)
            {
                return opener70_uwu;
            }
            if (Core.Get<IMemApiZoneInfo>().GetCurrTerrId() == 612)
            {
                return opener70_uwu;
            }
        }

        return null;
    }

    public void OnLanguageChanged(LanguageType languageType)
    {
    }

    public bool BuildQt(out JobViewWindow jobViewWindow)
    {
        jobViewWindow = new JobViewWindow(GNBSettings.Instance.JobViewSave, GNBSettings.Instance.Save, OverlayTitle);
        JobViewWindow = jobViewWindow; // 这里设置一个静态变量.方便其他地方用
        jobViewWindow.AddTab("通用", _lazyOverlay.DrawGeneral);
        jobViewWindow.AddTab("时间轴", _lazyOverlay.DrawTimeLine);
        jobViewWindow.AddTab("DEV", _lazyOverlay.DrawDev);
        jobViewWindow.AddQt("爆发药", true);
        jobViewWindow.AddQt("AOE", true);
        jobViewWindow.AddQt("爆发", true);
        jobViewWindow.AddQt("突进开关", true);
        jobViewWindow.AddQt("子弹连", true);
        jobViewWindow.AddQt("爆发击", true);
        jobViewWindow.AddQt("闪雷弹", true);
        jobViewWindow.AddQt("最终爆发", false);
        jobViewWindow.AddQt("起手突进开怪", false);
        jobViewWindow.AddQt("突进全进无情", true);
        jobViewWindow.AddQt("移动时不突进", true);
        jobViewWindow.AddHotkey("LB", new HotKeyResolver_LB());
        jobViewWindow.AddHotkey("超火流星!", new HotKeyResolver_NormalSpell(16152, SpellTargetType.Self, true));
        jobViewWindow.AddHotkey("刚玉自己", new HotKeyResolver_NormalSpell(25758, SpellTargetType.Self, true));
        jobViewWindow.AddHotkey("刚玉tt", new HotkeyResolver_General(@"../../RotationPlugin/Blz/Resources/刚玉tt.png", () =>
        {
            if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
            {
                foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_OffGCD)
                {
                    if (spell.Id == SpellsDefine.HeartOfCorundum)
                    {
                        return;
                    }
                }
            }
            AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(SpellHelper.GetSpell(SpellsDefine.HeartOfCorundum, SpellTargetType.TargetTarget));
        }));
        jobViewWindow.AddHotkey("刚玉大残", new HotkeyResolver_General(@"../../RotationPlugin/Blz/Resources/刚玉大残.jpg", () =>
        {
            if (AI.Instance.BattleData.HighPrioritySlots_OffGCD.Count > 0)
            {
                foreach (var spell in AI.Instance.BattleData.HighPrioritySlots_OffGCD)
                {
                    if (spell.Id == SpellsDefine.HeartOfCorundum)
                    {
                        return;
                    }
                }
            }
            if (SpellsDefine.HeartOfCorundum.IsReady())
            {
                AI.Instance.BattleData.HighPrioritySlots_OffGCD.Enqueue(SpellHelper.GetSpell(SpellsDefine.HeartOfCorundum, WhoNeedsHealing()));
            }
        }
        ));
        return true;
    }

    private SpellTargetType WhoNeedsHealing()
    {
        int i = 1;
        int LowIndex = 1;
        int LowHealth = 300000;
        List<CharacterAgent> agents = Core.Get<IMemApiParty>().GetParty();
        agents.ForEach(p =>
        {
            if (p.CurrentHealth < LowHealth && p.CurrentHealthPercent < 99.9 && !p.IsDead)
            {
                LowHealth = ((int)p.CurrentHealth);
                LowIndex = i;
            }
            i++;
        });
        SpellTargetType t = (SpellTargetType)Enum.Parse(typeof(SpellTargetType), (LowIndex + 3).ToString());
        return t;
    }
}