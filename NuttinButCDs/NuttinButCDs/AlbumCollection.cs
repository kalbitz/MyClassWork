using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace NuttinButCDs
{
    public class AlbumCollection
    {
        private ObservableCollection<Album> _albums = new ObservableCollection<Album>();

        public ObservableCollection<Album> Albums
        {
            get { return _albums; }
            set { _albums = value; }
        }

        public AlbumCollection()
        {
            // TODO: Fill in from database

            ObservableCollection<string> songs = new ObservableCollection<string>();
            songs.Add("Song 1");
            songs.Add("Song 2");
            songs.Add("Song 3");

            _albums.Add(new Album("Goodbye Yellowbrick Road", "Elton John", "Rock", 1972, 4, "Best EJ there is!", null, songs));
            _albums.Add(new Album("Fat Bottom Girls", "Queen", "Rock", 1974, 3, "", null, songs));
            _albums.Add(new Album("The White Album", "Beatles", "Pop", 1969, 0, "", null, null));
            _albums.Add(new Album("21", "Adele", "R&B", 2012, 2, "Not as good as they say", null, songs));
        }

        public bool RemoveAlbumFromCollection(Album album)
        {
            // TODO: remove from db too.
            return false;
        }

        public bool AddAlbumToCollection(Album album)
        {
            // TODO: add to DB too.
            _albums.Add(album);
            return true;
        }

        public bool UpdateAlbumInCollection(Album album)
        {
            // TODO: update in DB too.
            return false;
        }
    }
}
