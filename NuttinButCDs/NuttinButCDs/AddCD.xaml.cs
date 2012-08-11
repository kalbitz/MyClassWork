using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using NuttinButCDs.MusicServiceReference;
using System.Collections.ObjectModel;

/* TODO
 * Defect: When you invoke the empty Artist Name validation, then you click away from that box,
 * you can not click back in the Artist Name box.
 * */

namespace NuttinButCDs
{
    public partial class AddCd : Window
    {
        private MusicServiceSoapClient musicService = new MusicServiceSoapClient();
        private List<MusicServiceReference.Album> foundAlbums = new List<MusicServiceReference.Album>();
        private Storyboard metronome;

        public AddCd()
        {
            InitializeComponent();

            metronome = (Storyboard)FindResource("Metronome");
            MetronomeGrid.Visibility = System.Windows.Visibility.Collapsed;

            foundAlbums.Clear();

            editArtistTextBox.Focus();
            if (string.IsNullOrEmpty(editArtistTextBox.Text))
            {
                findItButton.IsEnabled = false;
            }
            doItButton.IsEnabled = false;
            addedTextBox.Height = 0;
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

            // TODO: separate into disks instead of lumping all songs together
            // you can always tell who learned to program in Fortran...
            for (int i = 0; i < alb.Disks.Count(); i++)
            {
                for (int j = 0; j < alb.Disks[i].Tracks.Count(); j++)
                    songs.Add(alb.Disks[i].Tracks[j].Title);
            }

            Uri uriSmall = (alb.SmallImageUrl == null) ? null : new Uri(alb.SmallImageUrl);
            Uri uriLarge = (alb.LargeImageUrl == null) ? null : new Uri(alb.LargeImageUrl);

            Album newAlbum = new Album(0,
                                       alb.Title,
                                       alb.Artist.Name,
                                       null,
                                       alb.ReleaseDate.Year,
                                       0, null,
                                       uriSmall,
                                       uriLarge, 
                                       songs);
            MainWindow.MyAlbums.Add(newAlbum);
            e.Handled = true;

            DoubleAnimation heightAnimation = new DoubleAnimation();
            heightAnimation.From = 0;
            heightAnimation.To = 30;
            heightAnimation.AutoReverse = true;
            heightAnimation.Duration = TimeSpan.FromMilliseconds(1000);
            addedTextBox.BeginAnimation(HeightProperty, heightAnimation);
        }

        private void PuntButtonClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
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

                metronome.Begin();
                MetronomeGrid.Visibility = System.Windows.Visibility.Visible;

                musicService.FindAlbumsByArtistAsync(editArtistTextBox.Text);
                musicService.FindAlbumsByArtistCompleted += MusicServiceFindAlbumsByArtistCompleted;
            }
        }

        private void MusicServiceFindAlbumsByArtistCompleted(object sender, FindAlbumsByArtistCompletedEventArgs e)
        {
            MetronomeGrid.Visibility = System.Windows.Visibility.Collapsed;
            metronome.Stop();

            findItButton.IsEnabled = true;
            puntButton.IsEnabled   = true;
            doItButton.IsEnabled   = false;

            if (e.Error != null)
            {
                return;
            }

            foundAlbums.Clear();
            foundAlbums = e.Result.ToList();
            albumListBox.ItemsSource = e.Result;
        }

        private void EditArtistTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(editArtistTextBox.Text))
            {
                findItButton.IsEnabled = false;
            }
            else
            {
                findItButton.IsEnabled = true;
            }
        }

        private void AlbumListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (albumListBox.SelectedItems.Count > 0 && albumListBox.SelectedItems[0] != null)
            {
                doItButton.IsEnabled = true;
            }
            else
            {
                doItButton.IsEnabled = false;
            }
        }
    }
}
