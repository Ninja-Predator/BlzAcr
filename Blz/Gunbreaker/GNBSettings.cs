using CombatRoutine.View.JobView;

using System.Numerics;
using Common.Helper;

namespace Blz.Gunbreaker;

public class GNBSettings
{
    public static GNBSettings Instance;
    private static string path;

    public static void Build(string settingPath)
    {
        path = Path.Combine(settingPath, "GNBSettings.json");
        if (!File.Exists(path))
        {
            Instance = new GNBSettings();
            Instance.Save();
            return;
        }

        try
        {
            Instance = JsonHelper.FromJson<GNBSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Instance = new GNBSettings();
            LogHelper.Error(e.ToString());
        }
    }

    public void Save()
    {
        Directory.CreateDirectory(Path.GetDirectoryName(path));
        File.WriteAllText(path, JsonHelper.ToJson(this));
    }

    public int Time = 450;
    public bool RoughDivideNoMove = true;
    public bool TP = false;

    public JobViewSave JobViewSave = new() { MainColor = new Vector4(47 / 255f, 118 / 255f, 161 / 255f, 0.8f) };
}