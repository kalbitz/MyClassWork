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

            Uri image = new Uri("http://ecx.images-amazon.com/images/I/61QzCwpt7mL._SL75_.jpg");

            Add(new Album("Goodbye Yellowbrick Road", "Elton John", "Rock", 1972, 4, "Best EJ there is!", null, null, songs));
            Add(new Album("Fat Bottom Girls", "Queen", "Rock", 1974, 3, "", image, image, songs));
            Add(new Album("The White Album", "Beatles", "Pop", 1969, 0, "", null, null, null));
            Add(new Album("21", "Adele", "R&B", 2012, 2, "Not as good as they say", null, null, songs));
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
