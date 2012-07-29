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
        private Album editableAlbum;
        private Album oldAlbum;

        public EditCD(Album album)
        {
            InitializeComponent();
            editNameTextBox.Focus();
            editableAlbum = album;
            oldAlbum = (Album)editableAlbum.Clone();
            DataContext = editableAlbum;
        }

        private void DoItButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.UpdateAlbum(oldAlbum, editableAlbum);
            e.Handled = true;
            this.Close();
        }

        private void PuntButtonClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            this.Close();
        }
    }
}
