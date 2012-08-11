using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace NuttinButCDs
{
    public partial class EditCD : Window, INotifyPropertyChanged
    {
        private List<int> _years;
        private List<string> _genres;
        private List<int> _ratings;
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

        public EditCD(Album album)
        {
            InitializeComponent();
            _years   = MainWindow.Years;
            _genres  = MainWindow.Genres;
            _ratings = MainWindow.Ratings;
            editNameTextBox.Focus();
            oldAlbum = album;
            EditableAlbum = (Album)album.Clone();
            addGenreButton.Visibility = Visibility.Collapsed; // See AddGenreButtonClick() below
            DataContext = this;

            // web says there's a timing defect requiring this...?
            ratingComboBox.SelectedValue = EditableAlbum.Rating; 
        }

        private void DoItButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.UpdateAlbum(oldAlbum, EditableAlbum);
            e.Handled = true;
            this.Close();
        }

        private void PuntButtonClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            this.Close();
        }

        private void AddGenreButtonClick(object sender, RoutedEventArgs e)
        {
            NewGenre newGenre = new NewGenre();
            newGenre.ShowDialog();
            /*
             * I'm having problems getting genreComboBox to update by itself. I think it is 
             * related to the fact that I can't implement INotifyPropertyChanged on MainWindow
             * because it is static. Probably would have to make it a singleton.
             * 
             * Even so, I cannot get the combo box to update explicitly either. I tried the two
             * stmts below. Just doing the first one did not work. Adding the second one causes
             * a null exception in MainWindow.EditAlbum. So for now, I'm going to remove
             * the addGenreButton from the EditCD dialog. Bummer.
             * */

            //genreComboBox.ItemsSource = Genres;
            //genreComboBox.GetBindingExpression(ComboBox.ItemsSourceProperty).UpdateSource();
        }

    public event PropertyChangedEventHandler PropertyChanged;
    }
}
