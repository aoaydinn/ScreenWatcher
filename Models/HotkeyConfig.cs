using System.Windows.Input;

namespace ScreenWatcher.Models
{
    public class HotkeyConfig
    {
        public ModifierKeys Modifier { get; set; } = ModifierKeys.Control | ModifierKeys.Shift;
        public Key Key { get; set; } = Key.F12;
    }
}
