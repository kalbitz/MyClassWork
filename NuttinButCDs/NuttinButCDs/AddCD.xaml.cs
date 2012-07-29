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
using NuttinButCDs.MusicServiceReference;
using System.Collections.ObjectModel;

namespace NuttinButCDs
{
    /// <summary>
    /// Interaction logic for AddCD.xaml
    /// </summary>
    public partial class AddCd : Window
    {
        private MusicServiceSoapClient musicService = new MusicServiceSoapClient();
        private List<MusicServiceReference.Album> foundAlbums = new List<MusicServiceReference.Album>();

        public AddCd()
        {
            InitializeComponent();
            foundAlbums.Clear();
            editArtistTextBox.Focus();
            editArtistTextBox.Text = "Queen";  // TEMPORARY
        }

        private void FindItButtonClick(object sender, RoutedEventArgs e)
        {
            InitiateFind();
            e.Handled = true;
        }

        private void DoItButtonClick(object sender, RoutedEventArgs e)
        {
            MusicServiceReference.Album alb = (MusicServiceReference.Album)albumListBox.SelectedItem;
            ObservableCollection<string> songs = new ObservableCollection<string>();

            if (alb == null)
            {
                return;
            }

            // you can always tell who learned to program in Fortran...
            for (int i = 0; i < alb.Disks.Count(); i++)
            {
                for (int j = 0; j < alb.Disks[i].Tracks.Count(); j++)
                    songs.Add(alb.Disks[i].Tracks[j].Title);
            }
            
            Album newAlbum = new Album(alb.Title,
                                       alb.Artist.Name,
                                       null,
                                       alb.ReleaseDate.Year,
                                       0, null,
                                       new Uri(alb.SmallImageUrl), 
                                       songs);
            MainWindow.MyAlbums.Add(newAlbum);
            e.Handled = true;
        }

        private void PuntButtonClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true; // is this needed before a Close?
            this.Close();
        }

        private void EditArtistTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
            {
                return;
            }

            InitiateFind();
            e.Handled = true;
        }

        private void InitiateFind()
        {
            if (!string.IsNullOrEmpty(editArtistTextBox.Text))
            {
                findItButton.IsEnabled = false;
                puntButton.IsEnabled   = false;
                doItButton.IsEnabled   = false;
                musicService.FindAlbumsByArtistAsync(editArtistTextBox.Text);
                musicService.FindAlbumsByArtistCompleted += MusicServiceFindAlbumsByArtistCompleted;
            }
        }

        private void MusicServiceFindAlbumsByArtistCompleted(object sender, FindAlbumsByArtistCompletedEventArgs e)
        {
            findItButton.IsEnabled = true;
            puntButton.IsEnabled   = true;
            doItButton.IsEnabled   = true;
            if (e.Error != null)
            {
                return;
            }
            //BusyProgressBar.Visibility = Visibility.Collapsed;

            foundAlbums.Clear();
            foundAlbums = e.Result.ToList();
            albumListBox.ItemsSource = e.Result;
        }
    }
}
