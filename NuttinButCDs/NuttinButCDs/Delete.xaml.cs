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
    /// Interaction logic for Delete.xaml
    /// </summary>
    public partial class Delete : Window
    {
        public Delete()
        {
            InitializeComponent();
        }

        private void DoItButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void PuntButtonClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true; // is this needed before a Close?
            this.Close();
        }
    }
}
