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
        private static List<string> _genres = new List<string>() {
            "Rock", "Pop", "Classical", "Hip Hop", "Jazz", "Blues" };
        private static List<int> _ratings = new List<int>() {0,1,2,3,4};
        private static List<int> _years = new List<int>();

        public static AlbumCollection MyAlbums;

        public static List<int> Years
        {
            get { return _years; }
            set { _years = value; }
        }

        public static List<string> Genres
        {
            get { return _genres; }
            set { _genres = value; }
        }

        public static List<int> Ratings
        {
            get { return _ratings; }
            set { _ratings = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            Genres.Sort();
	
            for (int year = 1900; year <= DateTime.Now.Year; year++) { _years.Add(year); }

            MyAlbums = new AlbumCollection();
            albumDataGrid.ItemsSource = MyAlbums;
        }

        private void EditButtonClick(object sender, RoutedEventArgs e)
        {
            EditAlbum(); 
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

        private void AlbumListViewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            EditAlbum(); 
        }

        private void EditAlbum()
        {
            if (albumDataGrid.SelectedItems.Count > 0 && albumDataGrid.SelectedItems[0] != null)
            {
                EditCD editCd = new EditCD((Album)albumDataGrid.SelectedItems[0]);
                editCd.ShowDialog();
            }
        }
    }
}
