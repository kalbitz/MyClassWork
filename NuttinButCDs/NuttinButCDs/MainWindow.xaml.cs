using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

/* TODO
 * Better validation on stored data
 * Handle multiple disks of songs better
 * Add tooltips
 * Figure out how to sort the Rating column
 * Error checking and handling could be a lot better!
 * Better sanitization of SQL data
 * Add a confirmation to the Close button
 * 
 * */

namespace NuttinButCDs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static GenresList _genres;
        private static List<int> _ratings = new List<int>();
        private static List<int> _years = new List<int>();

        public static AlbumCollection MyAlbums;

        public static List<int> Years
        {
            get { return _years; }
            set { _years = value; }
        }

        public static List<int> Ratings
        {
            get { return _ratings; }
            set { _ratings = value; }
        }

        public static GenresList Genres
        {
            get { return _genres; }
            set { _genres = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            LargeAlbumCover.Width = 0;

            for (int year = Constants.earliestYear; year <= DateTime.Now.Year; year++) { _years.Add(year); }
            for (int rating = Constants.minRating; rating <= Constants.maxRating; rating++) { _ratings.Add(rating); }

            Genres = new GenresList();
            Genres.Sort();
            MyAlbums = new AlbumCollection();
            albumDataGrid.ItemsSource = MyAlbums;
            if (albumDataGrid.SelectedItems.Count == 0)
            {
                EditButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
            // default the sort to the Artist column, 2
            var sortCol = albumDataGrid.Columns[2];
            sortCol.SortDirection = ListSortDirection.Ascending;
            albumDataGrid.Items.SortDescriptions.Add(new SortDescription(sortCol.SortMemberPath, ListSortDirection.Ascending));
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
            if (albumDataGrid.SelectedItems.Count > 0 && albumDataGrid.SelectedItems[0] != null)
            {
                Delete delete = new Delete((Album)albumDataGrid.SelectedItems[0]);
                delete.ShowDialog();
            }
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
            CloseConf closeConf = new CloseConf();
            closeConf.ShowDialog();
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

        private void ImageMouseEnter(object sender, MouseEventArgs e)
        {
            // TODO: Is there a better way to test if hover sender is SelectedItem?

            if (albumDataGrid.SelectedItems.Count > 0  &&
                albumDataGrid.SelectedItems[0] != null &&
                ((Album)albumDataGrid.SelectedItems[0]).AlbumImageSmall != null &&
                ((Album)albumDataGrid.SelectedItems[0]).AlbumImageLarge != null &&
                // is the sender the selected image:
                ((System.Windows.Controls.Image)sender).Source.ToString() == ((Album)albumDataGrid.SelectedItems[0]).AlbumImageSmall.OriginalString)
            {
                Album alb = (Album)albumDataGrid.SelectedItems[0];

                BitmapImage bmi = new BitmapImage();
                bmi.BeginInit();
                bmi.UriSource =  alb.AlbumImageLarge;
                bmi.EndInit();

                LargeImage.Source = bmi;

                DoubleAnimation widthAnimation = new DoubleAnimation();
                widthAnimation.From = 0;
                widthAnimation.To = 210;
                widthAnimation.Duration = TimeSpan.FromMilliseconds(100);
                LargeAlbumCover.BeginAnimation(WidthProperty, widthAnimation);
            }
        }

        private void ImageMouseLeave(object sender, MouseEventArgs e)
        {
            // TODO: 
            // MouseLeave isn't a good way to do this because there
            // are ways to not get the MouseLeave event...
            // Need more robustness here.

            DoubleAnimation widthAnimation = new DoubleAnimation();
            widthAnimation.To = 0;
            widthAnimation.Duration = TimeSpan.FromMilliseconds(100);
            LargeAlbumCover.BeginAnimation(WidthProperty, widthAnimation);
        }

        private void AlbumDataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (albumDataGrid.SelectedItems.Count > 0 && albumDataGrid.SelectedItems[0] != null)
            {
                EditButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }
            else
            {
                EditButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
        }

        public static void CloseWindow()
        {
            Application.Current.MainWindow.Close();
        }
    }
}
