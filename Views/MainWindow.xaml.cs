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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            // Just hide it instead of closing, unless application is shutting down.
            e.Cancel = true;
            this.Hide();
        }
    }
}
