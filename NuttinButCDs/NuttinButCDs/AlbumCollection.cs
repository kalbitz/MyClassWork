using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NuttinButCDs
{
    public class AlbumCollection : ObservableCollection<Album>
    {
        public AlbumCollection()
        {
            // TODO: Fill in from database

            ObservableCollection<string> songs = new ObservableCollection<string>();
            songs.Add("Song 1");
            songs.Add("Song 2");
            songs.Add("Song 3");

            Add(new Album("Goodbye Yellowbrick Road", "Elton John", "Rock", 1972, 4, "Best EJ there is!", null, songs));
            Add(new Album("Fat Bottom Girls", "Queen", "Rock", 1974, 3, "", null, songs));
            Add(new Album("The White Album", "Beatles", "Pop", 1969, 0, "", null, null));
            Add(new Album("21", "Adele", "R&B", 2012, 2, "Not as good as they say", null, songs));
        }

        public new bool Remove(Album album)
        {
            // TODO: remove from db too.
            base.Remove(album);
            return false;
        }

        public new bool Add(Album album)
        {
            // TODO: add to DB too.
            base.Add(album);
            return true;
        }

        public bool Update(Album oldAlbum, Album newAlbum)
        {
            // TODO: update in DB too.
            Remove(oldAlbum);
            Add(newAlbum);
            
            return false;
        }
    }
}
