using ScreenWatcher.Models;
using ScreenWatcher.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ScreenWatcher.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private AppSettings _settings;
        private string _keywordsText;

        public MainWindowViewModel()
        {
            _settings = SettingsManager.Load();
            _keywordsText = string.Join(Environment.NewLine, _settings.Keywords);

            SaveCommand = new RelayCommand(ExecuteSave);
            BrowseFolderCommand = new RelayCommand(ExecuteBrowseFolder);
        }

        public string SaveFolder
        {
            get => _settings.SaveFolder;
            set { _settings.SaveFolder = value; OnPropertyChanged("SaveFolder"); }
        }

        public string SaveFormat
        {
            get => _settings.SaveFormat;
            set { _settings.SaveFormat = value; OnPropertyChanged("SaveFormat"); }
        }

        public string KeywordsText
        {
            get => _keywordsText;
            set { _keywordsText = value; OnPropertyChanged("KeywordsText"); }
        }

        public Array AvailableKeys 
        {
            get
            {
                // Return only useful keys instead of all ~150 keys.
                var allKeys = Enum.GetValues(typeof(Key)).Cast<Key>();
                return allKeys.Where(k => 
                    (k >= Key.A && k <= Key.Z) || 
                    (k >= Key.D0 && k <= Key.D9) || 
                    (k >= Key.F1 && k <= Key.F24) ||
                    k == Key.Space || k == Key.Enter || k == Key.Tab || k == Key.Escape
                ).Distinct().ToArray();
            }
        }

        public bool IsCtrlSelected
        {
            get => _settings.CustomHotkey.Modifier.HasFlag(ModifierKeys.Control);
            set { UpdateModifier(ModifierKeys.Control, value); OnPropertyChanged("IsCtrlSelected"); }
        }

        public bool IsAltSelected
        {
            get => _settings.CustomHotkey.Modifier.HasFlag(ModifierKeys.Alt);
            set { UpdateModifier(ModifierKeys.Alt, value); OnPropertyChanged("IsAltSelected"); }
        }

        public bool IsShiftSelected
        {
            get => _settings.CustomHotkey.Modifier.HasFlag(ModifierKeys.Shift);
            set { UpdateModifier(ModifierKeys.Shift, value); OnPropertyChanged("IsShiftSelected"); }
        }

        public bool IsWinSelected
        {
            get => _settings.CustomHotkey.Modifier.HasFlag(ModifierKeys.Windows);
            set { UpdateModifier(ModifierKeys.Windows, value); OnPropertyChanged("IsWinSelected"); }
        }

        private void UpdateModifier(ModifierKeys mod, bool isAdd)
        {
            if (isAdd)
                _settings.CustomHotkey.Modifier |= mod;
            else
                _settings.CustomHotkey.Modifier &= ~mod;
        }

        public Key CustomHotkeyKey
        {
            get => _settings.CustomHotkey.Key;
            set { _settings.CustomHotkey.Key = value; OnPropertyChanged("CustomHotkeyKey"); }
        }

        public ICommand SaveCommand { get; }
        public ICommand BrowseFolderCommand { get; }

        private void ExecuteBrowseFolder(object obj)
        {
            using (var dialog = new CommonOpenFileDialog())
            {
                dialog.IsFolderPicker = true;
                dialog.InitialDirectory = SaveFolder;
                dialog.Title = "Kaydedilecek Klasörü Seçin (Ağ dizinleri desteklenir)";
                
                if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    SaveFolder = dialog.FileName;
                }
            }
        }

        private void ExecuteSave(object obj)
        {
            _settings.Keywords = KeywordsText
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(k => k.Trim())
                .Where(k => !string.IsNullOrEmpty(k))
                .ToList();

            if (!SettingsManager.Save(_settings))
            {
                System.Windows.MessageBox.Show(
                    "Ayarlar dosyaya kaydedilemedi. Klasör izinlerini veya disk alanını kontrol edin.",
                    "Ayarlar",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning);
                return;
            }

            var app = System.Windows.Application.Current as App;
            app?.UpdateHotkey();

            System.Windows.MessageBox.Show("Ayarlar kaydedildi ve kısayol güncellendi.", "Bilgi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
