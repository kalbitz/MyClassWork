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
    /// Interaction logic for NewGenre.xaml
    /// </summary>
    /// 

    // TODO: Add ability to delete a genre
    public partial class NewGenre : Window
    {
        public NewGenre()
        {
            InitializeComponent();
            editGenreTextBox.Focus();
            if (string.IsNullOrEmpty(editGenreTextBox.Text))
            {
                doItButton.IsEnabled = false;
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
            if (!string.IsNullOrEmpty(editGenreTextBox.Text))
            {
                MainWindow.AddGenre(editGenreTextBox.Text);
                this.Close();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void EditGenreTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(editGenreTextBox.Text))
            {
                doItButton.IsEnabled = false;
            }
            else
            {
                doItButton.IsEnabled = true;
            }
        }
    }
}
