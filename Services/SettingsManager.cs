using Newtonsoft.Json;
using ScreenWatcher.Models;
using System;
using System.IO;

namespace ScreenWatcher.Services
{
    public static class SettingsManager
    {
        private static readonly string SettingsFile = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "ScreenWatcher",
            "settings.json");

        public static AppSettings Load()
        {
            if (File.Exists(SettingsFile))
            {
                try
                {
                    string json = File.ReadAllText(SettingsFile);
                    return JsonConvert.DeserializeObject<AppSettings>(json) ?? new AppSettings();
                }
                catch
                {
                    return new AppSettings();
                }
            }

            return new AppSettings();
        }

        public static bool Save(AppSettings settings)
        {
            try
            {
                var directory = Path.GetDirectoryName(SettingsFile);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText(SettingsFile, json);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
