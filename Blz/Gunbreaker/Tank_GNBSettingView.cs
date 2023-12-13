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

        if (ImGui.Button("保存设置")) GNBSettings.Instance.Save();
    }
}