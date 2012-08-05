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
        private Album _deletableAlbum;

        public Delete(Album album)
        {
            InitializeComponent();
            _deletableAlbum = album;
            deleteWindow.reallyDeleteTextBlock.Text = "Do you really want to delete\n" + album.AlbumName + "?";
        }

        private void DoItButtonClick(object sender, RoutedEventArgs e)
        {
            if (_deletableAlbum != null)
            {
                MainWindow.MyAlbums.Remove(_deletableAlbum);
            }
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
