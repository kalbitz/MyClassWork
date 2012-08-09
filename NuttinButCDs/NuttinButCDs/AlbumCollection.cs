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
 * And/Or design in the db from the beginning with EntityDataModel
 * */

namespace NuttinButCDs
{
    public class AlbumCollection : ObservableCollection<Album>
    {
        private NuttinButCDsDBDataSet CDsDataSet = new NuttinButCDsDBDataSet();
        private DataTable albumDataTable = new DataTable("myAlbums");
        private DataTable songDataTable = new DataTable("mySongs");
        private DataTable genreDataTable = new DataTable("myGenres");
        private NuttinButCDsDBDataSetTableAdapters.AlbumsTableAdapter albumsTableAdapter =
            new NuttinButCDsDBDataSetTableAdapters.AlbumsTableAdapter();
        private NuttinButCDsDBDataSetTableAdapters.SongsTableAdapter songsTableAdapter =
            new NuttinButCDsDBDataSetTableAdapters.SongsTableAdapter();
        private NuttinButCDsDBDataSetTableAdapters.GenresTableAdapter genresTableAdapter =
            new NuttinButCDsDBDataSetTableAdapters.GenresTableAdapter();

        // TODO: make this a centrally located tool rather than replicate the code!
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
            // Pierre, you are an evil man for having suggested implementing a database! Geez!

            albumsTableAdapter.Fill(CDsDataSet.Albums);
            songsTableAdapter.Fill(CDsDataSet.Songs);
            genresTableAdapter.Fill(CDsDataSet.Genres);

            albumDataTable = albumsTableAdapter.GetData();
            songDataTable = songsTableAdapter.GetData();
            genreDataTable = genresTableAdapter.GetData();

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

                string genre = null;
                if (aRow["GenreID"] != null && aRow["GenreID"] != DBNull.Value)
                {
                    expression = String.Format("GenreID = {0}", (int)aRow["GenreID"]);
                    DataRow[] genreRows = genreDataTable.Select(expression);

                    if (genreRows != null && genreRows.Count() > 0)
                    {
                        genre = ConvertFromDBVal<string>(genreRows[0]["GenreName"]);
                    }                    
                }

                string imageSmall = ConvertFromDBVal<string>(aRow["AlbumImageSmall"]);
                Uri uriSmall = (imageSmall == null) ? null : new Uri(imageSmall);
                string imageLarge = ConvertFromDBVal<string>(aRow["AlbumImageLarge"]);
                Uri uriLarge = (imageLarge == null) ? null : new Uri(imageLarge);

                base.Add(new Album(
                    (int)aRow["AlbumID"],
                    ConvertFromDBVal<string>(aRow["AlbumName"]),
                    ConvertFromDBVal<string>(aRow["ArtistName"]),
                    genre,
                    (int)aRow["Year"],
                    (int)aRow["Rating"],
                    ConvertFromDBVal<string>(aRow["Comment"]),
                    uriSmall,
                    uriLarge,
                    songs));
            }
        }

        public new bool Remove(Album album)
        {
            bool result = false;
            DataRow theRow;

            if (album == null)
            {
                MessageBox.Show("Remove failed: null album");
                return false;
            }
                
            theRow = CDsDataSet.Albums.Rows.Find(album.AlbumID);

            if (theRow != null)
            {
                theRow.Delete();

                try
                {
                    albumsTableAdapter.Update(CDsDataSet.Albums);
                    CDsDataSet.Albums.AcceptChanges();
                    albumsTableAdapter.Fill(CDsDataSet.Albums);
                    songsTableAdapter.Fill(CDsDataSet.Songs);
                    albumDataTable = albumsTableAdapter.GetData();
                    //MessageBox.Show("Update album successful");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Update album failed: " + ex.Message);
                }

                result = base.Remove(album);
            }
            else
            {
                {
                    MessageBox.Show("Can't find album in DB: " + album.AlbumName);
                }
            }
            return result; // TODO: set this better and have callers check it.
        }

        public new void Add(Album album)
        {
            NuttinButCDsDBDataSet.AlbumsRow newRow;
            string expression = "";

            if (album == null)
            {
                MessageBox.Show("Add failed: null album");
                return;
            }

            int genreID = 0;
            if (album.Genre != null)
            {
                expression = "GenreName = " + "\'" + album.Genre + "\'";
                DataRow[] genreRows = genreDataTable.Select(expression);
                if (genreRows != null && genreRows.Count() > 0 &&
                    genreRows[0]["GenreID"] != null && genreRows[0]["GenreID"] != DBNull.Value)
                {
                    genreID = (int)genreRows[0]["GenreID"];
                }
            }
            newRow = CDsDataSet.Albums.NewAlbumsRow();

            newRow.BeginEdit();
            newRow["AlbumName"] = album.AlbumName;
            newRow["ArtistName"] = album.ArtistName;
            if (genreID != 0)
            {
                newRow["GenreID"] = genreID;
            }
            newRow["Year"] = album.Year;
            newRow["Rating"] = album.Rating;
            newRow["Comment"] = album.Comment;
            newRow["AlbumImageSmall"] = (album.AlbumImageSmall == null) ? null : album.AlbumImageSmall.ToString();
            newRow["AlbumImageLarge"] = (album.AlbumImageLarge == null) ? null : album.AlbumImageLarge.ToString();
            newRow.EndEdit();

            CDsDataSet.Albums.Rows.Add(newRow);

            try
            {
                // TODO: Validate data first!
                albumsTableAdapter.Update(CDsDataSet.Albums);
                CDsDataSet.Albums.AcceptChanges();
                albumsTableAdapter.Fill(CDsDataSet.Albums);
                albumDataTable = albumsTableAdapter.GetData();
                //MessageBox.Show("Update successful");
                base.Add(album);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Update album failed: " + ex.Message);
            }

            expression = "AlbumName = " + "\'" + album.AlbumName + "\'";
            DataRow[] myRows = albumDataTable.Select(expression);

            // TODO: Do something intelligent if no rows found.
            if (myRows.Count() > 0)
            {
                album.AlbumID = (int)myRows[myRows.Count()-1]["AlbumID"];
            }

            DataRow theRow = CDsDataSet.Albums.Rows.Find(album.AlbumID);

            foreach (string song in album.Songs)
            {
                NuttinButCDsDBDataSet.SongsRow songRow = CDsDataSet.Songs.NewSongsRow();

                songRow.BeginEdit();
                songRow["SongName"] = song;
                songRow["AlbumID"] = album.AlbumID;
                songRow.EndEdit();

                CDsDataSet.Songs.Rows.Add(songRow);
            }

            try
            {
                // TODO: Validate data first!
                songsTableAdapter.Update(CDsDataSet.Songs);
                CDsDataSet.Songs.AcceptChanges();
                songsTableAdapter.Fill(CDsDataSet.Songs);
                songDataTable = songsTableAdapter.GetData();
                //MessageBox.Show("Update successful");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Update songs failed: " + ex.Message);
            }
        }

        public void Update(Album curAlbum, Album newAlbum)
        {
            if (curAlbum == null || newAlbum == null)
            {
                MessageBox.Show("Update failed: null album: " +
                    ((curAlbum == null) ? "curAlbum" : "") + " " +
                    ((newAlbum == null) ? "newAlbum" : "")) ;
                return;
            }
            if (Remove(curAlbum) == true)
            {
                Add(newAlbum);
            }
        }
    }
}
