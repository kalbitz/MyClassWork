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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NuttinButCDs
{
    /// <summary>
    /// Interaction logic for EditCD.xaml
    /// </summary>
    public partial class EditCD : Window, INotifyPropertyChanged
    {
        private List<int> _years;
        private List<string> _genres;
        private List<int> _ratings;
       // private Object[] _ratingImages;
        private Album _editableAlbum;
        private Album oldAlbum;

        public Album EditableAlbum
        {
            get { return _editableAlbum; }
            set
            {
                _editableAlbum = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("EditableAlbum"));
                }
            }
        }

        public List<int> Years
        {
            get { return _years; }
            set
            {
                _years = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Years"));
                }
            }
        }

        public List<string> Genres
        {
            get { return _genres; }
            set
            {
                _genres = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Genres"));
                }
            }
        }

        public List<int> Ratings
        {
            get { return _ratings; }
            set
            {
                _ratings = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Ratings"));
                }
            }
        }

        //public Object[] RatingImages
        //{
        //    get { return _ratingImages; }
        //    set { _ratingImages = value; }
        //}

        public EditCD(Album album)
        {
            InitializeComponent();
            _years = MainWindow.Years;
            _genres = MainWindow.Genres;
            _ratings = MainWindow.Ratings;
            //_ratingImages = new Object[5];
            //_ratingImages[0] = System.Windows.Application.Current.TryFindResource("noStar");
            //_ratingImages[1] = System.Windows.Application.Current.TryFindResource("oneStar");
            //_ratingImages[2] = System.Windows.Application.Current.TryFindResource("twoStar");
            //_ratingImages[3] = System.Windows.Application.Current.TryFindResource("threeStar");
            //_ratingImages[4] = System.Windows.Application.Current.TryFindResource("fourStar");
            editNameTextBox.Focus();
            EditableAlbum = album;
            oldAlbum = (Album)EditableAlbum.Clone();
            DataContext = this;
            ratingComboBox.SelectedValue = EditableAlbum.Rating; // web says there's a timing defect requiring this...?
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

        private void AddGenreButtonClick(object sender, RoutedEventArgs e)
        {
            NewGenre newGenre = new NewGenre();
            newGenre.ShowDialog();
        }

    public event PropertyChangedEventHandler PropertyChanged;
    }

}
