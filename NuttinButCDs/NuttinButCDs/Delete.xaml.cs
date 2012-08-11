using System.Windows;

namespace NuttinButCDs
{
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
