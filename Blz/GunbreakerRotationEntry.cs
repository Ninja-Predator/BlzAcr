using System.Reflection;
using CombatRoutine;
using CombatRoutine.Opener;
using Common;
using Common.Define;
using Common.Language;
using Blz.Gunbreaker;
using Blz.Gunbreaker.Ability;
using Blz.Gunbreaker.GCD;
using CombatRoutine.View.JobView;
using System.Security.AccessControl;
using Common.Helper;
using Blz.Gunbreaker.Triggers;

namespace Blz;

public class GunbreakerRotationEntry : IRotationEntry
{
    private GunbreakerOverlay _lazyOverlay = new();
    public static JobViewWindow JobViewWindow;
    public string OverlayTitle { get; } = "Blz GNB";

    public AcrType AcrType { get; } = AcrType.HighEnd;

    public void DrawOverlay()
    {

    }

    public string AuthorName { get; } = "Blz";
    public Jobs TargetJob { get; } = Jobs.Gunbreaker;

    public List<ISlotResolver> SlotResolvers = new()
    {
        new GNBGCD_DoubleDown(),
        new GNBGCD_SonicBreak(),
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
            if (Core.Get<IMemApiZoneInfo>().GetCurrTerrId()==733)
            {
                return opener70_ucob;
            }

            if (Core.Get<IMemApiZoneInfo>().GetCurrTerrId()==777)
            {
                return opener70_uwu;
            }
            if (Core.Get<IMemApiZoneInfo>().GetCurrTerrId()==612)
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

        jobViewWindow.AddHotkey("超火流星!", new HotKeyResolver_NormalSpell(16152, SpellTargetType.Self, true));
        jobViewWindow.AddHotkey("刚玉tt", new HotKeyResolver_NormalSpell(25758, SpellTargetType.TargetTarget, true));
        jobViewWindow.AddHotkey("刚玉自己", new HotKeyResolver_NormalSpell(25758, SpellTargetType.Self, true));
        return true;
    }
}