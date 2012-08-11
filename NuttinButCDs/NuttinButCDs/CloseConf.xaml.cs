using System.Windows;

namespace NuttinButCDs
{
    public partial class CloseConf : Window
    {
        public CloseConf()
        {
            InitializeComponent();
        }

        private void DoItButtonClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            MainWindow.CloseWindow();
            this.Close();
        }

        private void PuntButtonClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            this.Close();
        }
    }
}
