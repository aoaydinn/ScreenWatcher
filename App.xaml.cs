using Hardcodet.Wpf.TaskbarNotification;
using ScreenWatcher.Models;
using ScreenWatcher.Services;
using ScreenWatcher.Views;
using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Interop;

namespace ScreenWatcher
{
    public partial class App : Application
    {
        private TaskbarIcon _notifyIcon;
        private MainWindow _mainWindow;
        private ScreenshotService _screenshotService;
        private FileSaveService _fileSaveService;
        private HotkeyService _hotkeyService;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            _notifyIcon = (TaskbarIcon)FindResource("MyNotifyIcon");
            
            try
            {
                _notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
            catch
            {
                _notifyIcon.Icon = System.Drawing.SystemIcons.Application;
            }
            
            _screenshotService = new ScreenshotService();
            _fileSaveService = new FileSaveService();
            _hotkeyService = new HotkeyService();

            _mainWindow = new MainWindow();
            
            // To register global hotkey, we need the window handle.
            // We force window to create handle without showing it.
            var helper = new WindowInteropHelper(_mainWindow);
            helper.EnsureHandle();

            var settings = SettingsManager.Load();
            _hotkeyService.Register(helper.Handle, settings.CustomHotkey.Modifier, settings.CustomHotkey.Key);
            _hotkeyService.HotkeyPressed += HotkeyService_HotkeyPressed;

            // Start minimized / hidden
            // _mainWindow.Show(); _mainWindow.Hide();
        }

        private void HotkeyService_HotkeyPressed(object sender, EventArgs e)
        {
            var settings = SettingsManager.Load();
            using (Bitmap screenshot = _screenshotService.CaptureFullScreen())
            {
                if (screenshot == null) return;

                // Create a clone of the bitmap so we don't dispose it while waiting for UI thread
                Bitmap screenshotClone = new Bitmap(screenshot);

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Views.SavePromptWindow prompt = new Views.SavePromptWindow(settings.Keywords);
                    if (prompt.ShowDialog() == true)
                    {
                        string fileNamePrefix = prompt.SelectedFileName;
                        string file = _fileSaveService.Save(screenshotClone, settings.SaveFolder, settings.SaveFormat, fileNamePrefix);
                        if (file != null)
                        {
                            _notifyIcon.ShowBalloonTip("Ekran Görüntüsü Kaydedildi", $"Dosya: {System.IO.Path.GetFileName(file)}", BalloonIcon.Info);
                        }
                    }
                    screenshotClone.Dispose();
                }));
            }
        }

        private void MenuSettings_Click(object sender, RoutedEventArgs e)
        {
            if (_mainWindow == null)
                _mainWindow = new MainWindow();
            
            _mainWindow.Show();
            _mainWindow.Activate();
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            new AboutWindow().ShowDialog();
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            Current.Shutdown();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _hotkeyService?.Dispose();
            _notifyIcon?.Dispose();
        }
    }
}
