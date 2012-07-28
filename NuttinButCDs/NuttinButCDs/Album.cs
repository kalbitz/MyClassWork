using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace NuttinButCDs
{
    public class Album
    {
        private string _albumName;
        private string _artistName;
        private string _genre;
        private int    _year;
        private int    _rating;
        private string _comment;
        private Uri    _albumImage;
        private ObservableCollection<string> _songs = new ObservableCollection<string>();

        public string AlbumName
        {
            get { return _albumName; }
            set { _albumName = value; }
        }

        public string ArtistName
        {
            get { return _artistName; }
            set { _artistName = value; }
        }

        public string Genre
        {
            get { return _genre; }
            set { _genre = value; }
        }

        public int Year
        {
            get { return _year; }
            set
            {
                if (_year >= 1900 && _year <= DateTime.Now.Year)
                {
                     _year = value;
                }
            }
        }

        public int Rating
        {
            get { return _rating; }
            set
            {
                // 0 rating indicates no opinion
                if (_rating >= 0 && _rating <= 4)
                {
                 _rating = value;
                }
            }
        }

        public string Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        public Uri AlbumImage
        {
            get { return _albumImage; }
            set { _albumImage = value; }
        }

        public ObservableCollection<string> Songs
        {
            get { return _songs; }
            set
            {
                _songs = value;
            }
        }

        public Album(string albumName,
                     string artistName,
                     string genre,
                     int    year,
                     int    rating,
                     string comment,
                     Uri    albumImage,
                     ObservableCollection<string> songs)
        {
            AlbumName = albumName;
            ArtistName = artistName;
            Genre = genre;
            Year = year;
            Rating = rating;
            Comment = comment;
            AlbumImage = albumImage;
            Songs = songs;
        }
    }
}
