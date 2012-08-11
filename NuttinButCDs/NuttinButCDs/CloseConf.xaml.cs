using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NuttinButCDs
{
    /// <summary>
    /// Interaction logic for CloseConf.xaml
    /// </summary>
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
