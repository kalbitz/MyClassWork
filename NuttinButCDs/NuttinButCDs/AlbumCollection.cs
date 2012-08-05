using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;

/* TODO
 * Redesign this to be able to bind directly to the db data rather than going
 * to the extra step of moving data betwee the db and the observable collection.
 * */

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
                return default(T);
            }
            else
            {
                return (T)obj;
            }
        }

        public AlbumCollection()
        {
            //ObservableCollection<string> songs = new ObservableCollection<string>();
            //songs.Add("Song 1");
            //songs.Add("Song 2");
            //songs.Add("Song 3");

            //Uri image = new Uri("http://ecx.images-amazon.com/images/I/61QzCwpt7mL._SL75_.jpg");

            //Add(new Album("Goodbye Yellowbrick Road", "Elton John", "Rock", 1972, 4, "Best EJ there is!", null, null, songs));
            //Add(new Album("Fat Bottom Girls", "Queen", "Rock", 1974, 3, "", image, image, songs));
            //Add(new Album("The White Album", "Beatles", "Pop", 1969, 0, "", null, null, null));
            //Add(new Album("21", "Adele", "R&B", 2012, 2, "Not as good as they say", null, null, songs));

            // Pierre, you are an evil man for having suggested implementing a database! Geez!
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
            newRow.EndEdit();

            CDsDataSet.Albums.Rows.Add(newRow);

            try
            {
                // TODO: Validate data first!
                albumsTableAdapter.Update(CDsDataSet.Albums);
                CDsDataSet.Albums.AcceptChanges();
                albumDataTable = albumsTableAdapter.GetData();
                MessageBox.Show("Update successful"); // TODO: Make this a pretty box
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message);
            }

            albumDataTable = albumsTableAdapter.GetData();
            DataRowCollection albumRows = albumDataTable.Rows;

            string expression = "AlbumName = " + "\'" + (string)newRow["AlbumName"] + "\'";
            DataRow[] myRows = albumDataTable.Select(expression);

            // TODO: Do something intelligent if no rows found.
            if (myRows.Count() > 0)
            {
                album.AlbumId = (int)myRows[myRows.Count()-1]["AlbumId"];
            }
            base.Add(album);

            foreach (string song in album.Songs)
            {
                NuttinButCDsDBDataSet.SongsRow songRow = CDsDataSet.Songs.NewSongsRow();

                songRow.BeginEdit();
                songRow["SongName"] = song;
                songRow["SongName"] = album.AlbumId;
                songRow.EndEdit();

                CDsDataSet.Songs.Rows.Add(songRow);
            }

            try
            {
                // TODO: Validate data first!
                songsTableAdapter.Update(CDsDataSet.Songs);
                CDsDataSet.Songs.AcceptChanges();
                songDataTable = songsTableAdapter.GetData();
                MessageBox.Show("Update successful"); // TODO: Make this a pretty box
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Update failed: " + ex.Message);
            }

            songDataTable = songsTableAdapter.GetData();
        }

        public void Update(Album curAlbum, Album newAlbum)
        {
            Remove(curAlbum);
            Add(newAlbum);
        }
    }
}
