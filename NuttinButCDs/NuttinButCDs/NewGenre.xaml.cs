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
using System.Windows.Shapes;

namespace NuttinButCDs
{
    /// <summary>
    /// Interaction logic for NewGenre.xaml
    /// </summary>
    /// 

    // TODO: Add ability to delete a genre
    public partial class NewGenre : Window, INotifyPropertyChanged
    {
        private string _genre;

        public NewGenre()
        {
            InitializeComponent();
            DataContext = this;
            addedTextBox.Height = 0;
            editGenreTextBox.Focus();
            doItButton.IsEnabled = false;
        }

        public string Genre
        {
            get { return _genre; }
            set {
                if (!String.IsNullOrEmpty(value))
                {
                    if (value.Length > Constants.maxGenreLength)
                    {
                        doItButton.IsEnabled = false;
                        throw new ApplicationException("");
                    }
                    else
                    {
                        doItButton.IsEnabled = true;
                    }
                }
                else
                {
                    doItButton.IsEnabled = false;
                }
                _genre = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Genre"));
                }
            }
        }

        private void EditGenreTextBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter)
            {
                return;
            }

            AddGenre();
            e.Handled = true;
        }

        private void DoItButtonClick(object sender, RoutedEventArgs e)
        {
            AddGenre();
            e.Handled = true;
        }

        private void PuntButtonClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            this.Close();
        }

        private bool AddGenre()
        {
            if (!String.IsNullOrEmpty(editGenreTextBox.Text) && editGenreTextBox.Text.Length <= Constants.maxGenreLength)
            {
                MainWindow.AddGenre(editGenreTextBox.Text);

                DoubleAnimation heightAnimation = new DoubleAnimation();
                heightAnimation.From = 0;
                heightAnimation.To = 30;
                heightAnimation.AutoReverse = true;
                heightAnimation.Duration = TimeSpan.FromMilliseconds(1000);
                addedTextBox.BeginAnimation(HeightProperty, heightAnimation);
                editGenreTextBox.Text = "";

                return true;
            }
            else
            {
                return false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
