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
    /// Interaction logic for EditCD.xaml
    /// </summary>
    public partial class EditCD : Window
    {
        public EditCD()
        {
            InitializeComponent();
            editNameTextBox.Focus();
        }
    }
}
