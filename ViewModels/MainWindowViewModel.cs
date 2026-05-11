using ScreenWatcher.Models;
using ScreenWatcher.Services;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Forms; // for FolderBrowserDialog

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

        public Array AvailableModifiers => Enum.GetValues(typeof(ModifierKeys));
        public Array AvailableKeys => Enum.GetValues(typeof(Key));

        public ModifierKeys CustomHotkeyModifier
        {
            get => _settings.CustomHotkey.Modifier;
            set { _settings.CustomHotkey.Modifier = value; OnPropertyChanged("CustomHotkeyModifier"); }
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
            using (var dialog = new FolderBrowserDialog())
            {
                dialog.SelectedPath = SaveFolder;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SaveFolder = dialog.SelectedPath;
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

            SettingsManager.Save(_settings);
            
            // Inform user or close (we can handle closing in the view)
            System.Windows.MessageBox.Show("Ayarlar kaydedildi. Uygulamayı yeniden başlatmanız kısayol değişiklikleri için gerekebilir.", "Bilgi", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
