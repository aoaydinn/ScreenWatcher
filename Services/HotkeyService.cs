using System;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Interop;

namespace ScreenWatcher.Services
{
    public class HotkeyService : IDisposable
    {
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int WM_HOTKEY = 0x0312;
        private IntPtr _hWnd;
        private HwndSource _source;

        public event EventHandler HotkeyPressed;

        private const int PRINT_SCREEN_ID = 9000;
        private const int CUSTOM_HOTKEY_ID = 9001;

        public void Register(IntPtr hWnd, ModifierKeys customModifier, Key customKey)
        {
            _hWnd = hWnd;
            _source = HwndSource.FromHwnd(_hWnd);
            _source.AddHook(HwndHook);

            // Register PrintScreen (no modifiers, Key.PrintScreen)
            RegisterHotKey(_hWnd, PRINT_SCREEN_ID, 0, (uint)KeyInterop.VirtualKeyFromKey(Key.PrintScreen));

            // Register Custom Hotkey
            uint modifiers = (uint)customModifier;
            uint vk = (uint)KeyInterop.VirtualKeyFromKey(customKey);
            RegisterHotKey(_hWnd, CUSTOM_HOTKEY_ID, modifiers, vk);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY)
            {
                int id = wParam.ToInt32();
                if (id == PRINT_SCREEN_ID || id == CUSTOM_HOTKEY_ID)
                {
                    HotkeyPressed?.Invoke(this, EventArgs.Empty);
                    handled = true;
                }
            }
            return IntPtr.Zero;
        }

        public void Dispose()
        {
            if (_hWnd != IntPtr.Zero)
            {
                UnregisterHotKey(_hWnd, PRINT_SCREEN_ID);
                UnregisterHotKey(_hWnd, CUSTOM_HOTKEY_ID);
                _source?.RemoveHook(HwndHook);
                _hWnd = IntPtr.Zero;
            }
        }
    }
}
