using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

namespace NuttinButCDs
{
    public class AlbumCollection : ObservableCollection<Album>
    {
        private NuttinButCDsDBDataSet CDsDataSet = new NuttinButCDsDBDataSet();
        private DataTable albumDataTable = new DataTable("myAlbums");
        private DataTable songDataTable = new DataTable("mySongs");
        private NuttinButCDsDBDataSetTableAdapters.AlbumsTableAdapter albumsTableAdapter =
            new NuttinButCDsDBDataSetTableAdapters.AlbumsTableAdapter();
        private NuttinButCDsDBDataSetTableAdapters.SongsTableAdapter songsTableAdapter =
            new NuttinButCDsDBDataSetTableAdapters.SongsTableAdapter();

        public static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else
            {
                return (T)obj;
            }
        }

        public AlbumCollection()
        {
            // TODO: Fill in from database
            

            //ObservableCollection<string> songs = new ObservableCollection<string>();
            //songs.Add("Song 1");
            //songs.Add("Song 2");
            //songs.Add("Song 3");

            //Uri image = new Uri("http://ecx.images-amazon.com/images/I/61QzCwpt7mL._SL75_.jpg");

            //Add(new Album("Goodbye Yellowbrick Road", "Elton John", "Rock", 1972, 4, "Best EJ there is!", null, null, songs));
            //Add(new Album("Fat Bottom Girls", "Queen", "Rock", 1974, 3, "", image, image, songs));
            //Add(new Album("The White Album", "Beatles", "Pop", 1969, 0, "", null, null, null));
            //Add(new Album("21", "Adele", "R&B", 2012, 2, "Not as good as they say", null, null, songs));

            // TODO: Presmuably there is a better way to fill the collection from the db...

            albumsTableAdapter.Fill(CDsDataSet.Albums);
            songsTableAdapter.Fill(CDsDataSet.Songs);

            albumDataTable = albumsTableAdapter.GetData();
            songDataTable = songsTableAdapter.GetData();

            DataRowCollection albumRows = albumDataTable.Rows;
            foreach (DataRow aRow in albumRows)
            {
                ObservableCollection<string> songs = new ObservableCollection<string>();

                string expression = String.Format("AlbumID = {0}", (int)aRow["AlbumID"]);
                DataRow[] songRows = songDataTable.Select(expression);

                foreach (DataRow sRow in songRows)
                {
                    songs.Add(ConvertFromDBVal<string>(sRow["SongName"]));
                }
                base.Add(new Album(
                    (int)aRow["AlbumId"],
                    ConvertFromDBVal<string>(aRow["AlbumName"]),
                    ConvertFromDBVal<string>(aRow["ArtistName"]),
                    ConvertFromDBVal<string>(aRow["Genre"]),
                    (int)aRow["Year"],
                    (int)aRow["Rating"],
                    ConvertFromDBVal<string>(aRow["Comment"]),
                    new Uri(ConvertFromDBVal<string>(aRow["AlbumImageSmall"])), 
                    new Uri(ConvertFromDBVal<string>(aRow["AlbumImageLarge"])),
                    songs));

            }
        }

        public new bool Remove(Album album)
        {
            // TODO: remove from db too.
            return base.Remove(album);
        }

        public new void Add(Album album)
        {
            // TODO: add to DB too.
            NuttinButCDsDBDataSet.AlbumsRow newRow = CDsDataSet.Albums.NewAlbumsRow();

            newRow.BeginEdit();
            newRow["AlbumName"] = album.AlbumName;
            newRow["ArtistName"] = album.ArtistName;
            newRow["Genre"] = album.Genre;
            newRow["Year"] = album.Year;
            newRow["Rating"] = album.Rating;
            newRow["Comment"] = album.Comment;
            newRow["AlbumImageSmall"] = album.AlbumImageSmall.ToString();
            newRow["AlbumImageLarge"] = album.AlbumImageLarge.ToString();
            //newRow.RowState = DataRowState.Modified;
            newRow.EndEdit();

            //CDsDataSet.Albums.AddAlbumsRow(newRow);

            CDsDataSet.Albums.Rows.Add(newRow);
            CDsDataSet.Albums.AcceptChanges();

            try
            {
                //this.Validate();
                //this.customersBindingSource.EndEdit();
                //this.customersTableAdapter.Update(this.northwindDataSet.Customers);
                albumsTableAdapter.Update(CDsDataSet.Albums);
                MessageBox.Show("Update successful");
                albumsTableAdapter.Fill(CDsDataSet.Albums);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Update failed");
            }

            //album.AlbumId = (int)newRow["AlbumId"];
            string expression = "AlbumName = " + "\'" + (string)newRow["AlbumName"] + "\'";
            DataRow[] myRows = albumDataTable.Select(expression);
            albumsTableAdapter.GetData();
            DataRowCollection albumRows = albumDataTable.Rows;

            //NorthwindDataSet.CustomersRow newCustomersRow =
            //northwindDataSet1.Customers.NewCustomersRow();

            //newCustomersRow.CustomerID = "ALFKI";
            //newCustomersRow.CompanyName = "Alfreds Futterkiste";

            //northwindDataSet1.Customers.Rows.Add(newCustomersRow);

            base.Add(album);
        }

        public void Update(Album curAlbum, Album newAlbum)
        {
            // TODO: update in DB too.
            Remove(curAlbum);
            Add(newAlbum);
        }
    }
}
