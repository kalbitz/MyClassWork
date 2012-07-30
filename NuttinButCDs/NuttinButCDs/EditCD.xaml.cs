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
        private List<int> _years;
        private List<string> _genres;
        private List<int> _ratings;
        private Album _editableAlbum;
        private Album oldAlbum;

        public Album EditableAlbum
        {
            get { return _editableAlbum; }
            set { _editableAlbum = value; }
        }

        public List<int> Years
        {
            get { return _years; }
            set { _years = value; }
        }

        public List<string> Genres
        {
            get { return _genres; }
            set { _genres = value; }
        }

        public List<int> Ratings
        {
            get { return _ratings; }
            set { _ratings = value; }
        }

        public EditCD(Album album)
        {
            InitializeComponent();
            _years = MainWindow.Years;
            _genres = MainWindow.Genres;
            _ratings = MainWindow.Ratings;
            editNameTextBox.Focus();
            EditableAlbum = album;
            oldAlbum = (Album)EditableAlbum.Clone();
            DataContext = this;
        }


        private void DoItButtonClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            this.Close();
        }

        private void PuntButtonClick(object sender, RoutedEventArgs e)
        {
            // Restore old album
            MainWindow.UpdateAlbum(EditableAlbum, oldAlbum);
            e.Handled = true;
            this.Close();
        }
    }
}
