using System.ComponentModel;
using System.Windows;

namespace ScreenWatcher.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Hide_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        private void SaveAndClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }

        public bool IsReallyClosing { get; set; } = false;

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (!IsReallyClosing)
            {
                e.Cancel = true;
                this.Hide();
            }
        }
    }
}
