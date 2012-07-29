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
        public static AlbumCollection MyAlbums = new AlbumCollection();
        public static List<string> Genres = new List<string>() {
            "Rock", "Pop", "Classical", "Hip Hop", "Jazz", "Blues" };

        public MainWindow()
        {
            InitializeComponent();
            Genres.Sort();
            albumListView.ItemsSource = MyAlbums;
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            Album album = (Album)albumListView.SelectedItems[0];
            if (album != null)
            {
                EditCD editCd = new EditCD(album);
                editCd.ShowDialog();
            }
        }

        private void NewGenreButtonClick(object sender, RoutedEventArgs e)
        {
            NewGenre newGenre = new NewGenre();
            newGenre.ShowDialog();
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            Delete delete = new Delete();
            delete.ShowDialog();
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            AddCd addCd = new AddCd();
            addCd.ShowDialog();
        }

        private void AboutButtonClick(object sender, RoutedEventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        public static void AddGenre(string newGenre)
        {
            if (!string.IsNullOrEmpty(newGenre))
            {
                Genres.Add(newGenre);
                Genres.Sort();
            }
        }

        public static void UpdateAlbum(Album oldAlbum, Album newAlbum)
        {
            MyAlbums.Update(oldAlbum, newAlbum);
        }
    }
}
