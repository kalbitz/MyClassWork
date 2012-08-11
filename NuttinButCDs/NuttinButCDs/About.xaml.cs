using System.Windows;

namespace NuttinButCDs
{
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
            aboutTextBlock.Text =
                "Special Features:\n\n" +
                "\u2022 In Add Albums and New Genre, the text box activates the action on return.\n" +
                "\u2022 If there are no songs, you get a special message in the DataGrid RowDetailsTemplate box.\n" +
                "\u2022 The backend db is Microsoft SQL Server 2012 Express LocalDB.\n" +
                "\u2022 Hover over selected image cover in main window results in a larger image displayed.\n" +
                "\u2022 Both Add Albums and New Genre feature an animated feedback text when you add an item." +
                "\n\n" +
                "Brought to you by: Katherine R. Albitz" 
                ;
        }

        private void DoneButtonClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            Close();
        }
    }
}
