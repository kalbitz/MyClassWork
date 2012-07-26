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
    public partial class AddCD : Window
    {
        private MusicServiceSoapClient musicService = new MusicServiceSoapClient();
        public AddCD()
        {
            InitializeComponent();
        }

        private void findItButton_Click(object sender, RoutedEventArgs e)
        {
            initiateFind();
            e.Handled = true;
        }


        private void doItButton_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }

        private void puntButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            e.Handled = true; // is this needed?
        }

        private void editArtistTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
            {
                return;
            }

            initiateFind();
            e.Handled = true;
        }

        private void initiateFind()
        {
            if (!string.IsNullOrEmpty(editArtistTextBox.Text))
            {
                findItButton.IsEnabled = false;
                puntButton.IsEnabled   = false;
                doItButton.IsEnabled   = false;
                musicService.FindAlbumsByArtistAsync(editArtistTextBox.Text);
                musicService.FindAlbumsByArtistCompleted += musicService_FindAlbumsByArtistCompleted;
            }
        }

        private void musicService_FindAlbumsByArtistCompleted(object sender, FindAlbumsByArtistCompletedEventArgs e)
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
