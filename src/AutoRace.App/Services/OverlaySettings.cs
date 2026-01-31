using System;
using System.IO;
using System.Text.Json;

namespace AutoRace.App.Services;

public sealed class OverlaySettings
{
    public double? Left { get; set; }
    public double? Top { get; set; }

    private static string SettingsPath
    {
        get
        {
            var dir = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "AutoRace");
            return Path.Combine(dir, "overlay.json");
        }
    }

    public static OverlaySettings Load()
    {
        try
        {
            var path = SettingsPath;
            if (!File.Exists(path)) return new OverlaySettings();

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<OverlaySettings>(json) ?? new OverlaySettings();
        }
        catch
        {
            return new OverlaySettings();
        }
    }

    public void Save()
    {
        try
        {
            var path = SettingsPath;
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            var json = JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            File.WriteAllText(path, json);
        }
        catch
        {
            // ignore
        }
    }
}
