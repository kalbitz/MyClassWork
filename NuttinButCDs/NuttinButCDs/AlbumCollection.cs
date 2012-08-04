using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;

namespace NuttinButCDs
{
    public class AlbumCollection : ObservableCollection<Album>
    {
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
            

            ObservableCollection<string> songs = new ObservableCollection<string>();
            songs.Add("Song 1");
            songs.Add("Song 2");
            songs.Add("Song 3");

            Uri image = new Uri("http://ecx.images-amazon.com/images/I/61QzCwpt7mL._SL75_.jpg");

            Add(new Album("Goodbye Yellowbrick Road", "Elton John", "Rock", 1972, 4, "Best EJ there is!", null, null, songs));
            Add(new Album("Fat Bottom Girls", "Queen", "Rock", 1974, 3, "", image, image, songs));
            Add(new Album("The White Album", "Beatles", "Pop", 1969, 0, "", null, null, null));
            Add(new Album("21", "Adele", "R&B", 2012, 2, "Not as good as they say", null, null, songs));


            NuttinButCDsDBDataSet CDsDataSet = new NuttinButCDsDBDataSet();

            NuttinButCDsDBDataSetTableAdapters.AlbumsTableAdapter albumsTableAdapter =
                new NuttinButCDsDBDataSetTableAdapters.AlbumsTableAdapter();
            NuttinButCDsDBDataSetTableAdapters.SongsTableAdapter songsTableAdapter =
                new NuttinButCDsDBDataSetTableAdapters.SongsTableAdapter();

            albumsTableAdapter.Fill(CDsDataSet.Albums);
            songsTableAdapter.Fill(CDsDataSet.Songs);

            DataTable albumDataTable = new DataTable("myAlbums");
            albumDataTable = albumsTableAdapter.GetData();

            //DataTable songDataTable = new DataTable("mySongs");
            //songDataTable = songsTableAdapter.GetData();

            System.Data.DataRowCollection rows = albumDataTable.Rows;
            foreach (System.Data.DataRow row in rows)
            {
                this.Add(new Album(
                    ConvertFromDBVal<string>(row["AlbumName"]),
                    ConvertFromDBVal<string>(row["ArtistName"]),
                    ConvertFromDBVal<string>(row["Genre"]),
                    (int)row["Year"],
                    (int)row["Rating"],
                    ConvertFromDBVal<string>(row["Comment"]),
                    new Uri(ConvertFromDBVal<string>(row["AlbumImageSmall"])), 
                    new Uri(ConvertFromDBVal<string>(row["AlbumImageLarge"])),
                    null));

            }

            //var songs2 = new ObservableCollection<string>(
            //    CDsDataSet.Tables["Songs"].AsEnumerable().Select(p => new String(p.Field<char[]>("SongName"))));

        }

        public new bool Remove(Album album)
        {
            // TODO: remove from db too.
            return base.Remove(album);
        }

        public new void Add(Album album)
        {
            // TODO: add to DB too.
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
