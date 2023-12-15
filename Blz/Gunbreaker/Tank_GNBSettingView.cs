using CombatRoutine.View;
using Common.GUI;
using Common.Language;
using ImGuiNET;

namespace Blz.Gunbreaker;

public class TankGNBSettingView : ISettingUI
{
    public string Name => "枪刃";

    public void Draw()
    {
        ImGuiHelper.LeftInputInt("突进时间(倒数多少ms突进)",
            ref GNBSettings.Instance.Time, 100, 10000, 100);
        ImGui.Checkbox("自动的突进不位移，手动突进位移(Lv4可用)".Loc(), ref GNBSettings.Instance.RoughDivideNoMove);
        if (ImGui.Button("保存设置")) GNBSettings.Instance.Save();
    }
}