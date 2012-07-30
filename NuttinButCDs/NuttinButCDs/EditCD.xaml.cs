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

        public EditCD(Album album)
        {
            InitializeComponent();
            _years = MainWindow.Years;
            editNameTextBox.Focus();
            EditableAlbum = album;
            oldAlbum = (Album)EditableAlbum.Clone();
            DataContext = this;
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
    }
}
