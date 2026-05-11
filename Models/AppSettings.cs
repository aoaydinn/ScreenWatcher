using System;
using System.Collections.Generic;

namespace ScreenWatcher.Models
{
    public class AppSettings
    {
        public string SaveFolder { get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        public string SaveFormat { get; set; } = "PNG"; // PNG or PDF
        public List<string> Keywords { get; set; } = new List<string>();
        public HotkeyConfig CustomHotkey { get; set; } = new HotkeyConfig();
    }
}
