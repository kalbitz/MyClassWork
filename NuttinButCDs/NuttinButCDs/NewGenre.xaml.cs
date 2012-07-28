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
    public partial class NewGenre : Window
    {
        public NewGenre()
        {
            InitializeComponent();
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
            e.Handled = true; // is this needed before a Close?
            this.Close();
        }

        private bool AddGenre()
        {
            if (!string.IsNullOrEmpty(editGenreTextBox.Text))
            {
                MainWindow.Genres.Add(editGenreTextBox.Text);
                MainWindow.Genres.Sort();
            }
            return true;
        }
    }
}
