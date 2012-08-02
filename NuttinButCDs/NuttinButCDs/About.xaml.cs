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
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            aboutTextBlock.Text =
                "Brought to you by: Katherine R. Albitz\n\n" +
                "Special Features:\n" +
                "In Add CDs, the Artist name text box activates Find It on return.\n" +
                "If there are no songs, you get a special message in the DataGrid RowDetailsTemplate box.\n" +
                //"The backend db is Microsoft SQL Server 2012 Express LocalDB.\n" +
                "Hover over selected image cover in main window results in a larger image displayed.\n"
                ;
        }

        private void DoneButtonClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            this.Close();
        }
    }
}
