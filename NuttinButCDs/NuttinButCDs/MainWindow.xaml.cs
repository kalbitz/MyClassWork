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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NuttinButCDs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            EditCD editCd = new EditCD();
            editCd.ShowDialog();
        }

        private void newGenreButton_Click(object sender, RoutedEventArgs e)
        {
            NewGenre newGenre = new NewGenre();
            newGenre.ShowDialog();
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            Delete delete = new Delete();
            delete.ShowDialog();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            AddCD addCD = new AddCD();
            addCD.ShowDialog();
        }
    }
}
