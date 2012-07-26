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

namespace NuttinButCDs
{
    /// <summary>
    /// Interaction logic for AddCD.xaml
    /// </summary>
    public partial class AddCd : Window
    {
        private MusicServiceSoapClient musicService = new MusicServiceSoapClient();

        public AddCd()
        {
            InitializeComponent();
        }

        private void FindItButtonClick(object sender, RoutedEventArgs e)
        {
            InitiateFind();
            e.Handled = true;
        }


        private void DoItButtonClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void PuntButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            e.Handled = true; // is this needed?
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

            albumListBox.ItemsSource = e.Result;
        }
    }
}
