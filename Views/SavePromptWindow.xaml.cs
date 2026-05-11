using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ScreenWatcher.Views
{
    public partial class SavePromptWindow : Window
    {
        public string SelectedFileName { get; private set; }
        private List<TextBox> _textBoxes = new List<TextBox>();

        public SavePromptWindow(List<string> fields)
        {
            InitializeComponent();
            
            if (fields == null || fields.Count == 0)
            {
                fields = new List<string> { "Dosya Adı" };
            }

            foreach (var field in fields)
            {
                if (string.IsNullOrWhiteSpace(field)) continue;

                Label lbl = new Label { Content = field.Trim() + ":", FontWeight = FontWeights.Bold, Margin = new Thickness(0, 0, 0, 2) };
                TextBox txt = new TextBox { Margin = new Thickness(0, 0, 0, 10), VerticalContentAlignment = VerticalAlignment.Center };
                
                txt.KeyDown += Txt_KeyDown;
                
                _textBoxes.Add(txt);
                DynamicInputPanel.Children.Add(lbl);
                DynamicInputPanel.Children.Add(txt);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_textBoxes.Count > 0)
            {
                _textBoxes[0].Focus();
            }
        }

        private void Txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var txt = sender as TextBox;
                int index = _textBoxes.IndexOf(txt);
                if (index < _textBoxes.Count - 1)
                {
                    _textBoxes[index + 1].Focus();
                    e.Handled = true; // prevent triggering the save button right away
                }
                else
                {
                    ConfirmSelection();
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            ConfirmSelection();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void ConfirmSelection()
        {
            var values = _textBoxes.Select(t => t.Text?.Trim()).Where(t => !string.IsNullOrWhiteSpace(t)).ToList();
            
            if (values.Count == 0)
            {
                MessageBox.Show("Lütfen en az bir alanı doldurun.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedFileName = string.Join("_", values);
            this.DialogResult = true;
            this.Close();
        }
    }
}
