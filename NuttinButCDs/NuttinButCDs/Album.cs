using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace NuttinButCDs
{
    public class Album : INotifyPropertyChanged, ICloneable
    {
        private string _albumName;
        private string _artistName;
        private string _genre;
        private int _year;
        private int _rating;
        private string _comment;
        private Uri _albumImage;
        private ObservableCollection<string> _songs = new ObservableCollection<string>();

        public string AlbumName
        {
            get { return _albumName; }
            set
            {
                _albumName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AlbumName"));
                }
            }
        }

        public string ArtistName
        {
            get { return _artistName; }
            set
            {
                _artistName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ArtistName"));
                }
            }
        }

        public string Genre
        {
            get { return _genre; }
            set
            {
                _genre = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Genre"));
                }
            }
        }

        public int Year
        {
            get { return _year; }
            set
            {
                if (value >= 1900 && value <= DateTime.Now.Year)
                {
                    _year = value;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Year"));
                }
            }
        }

        public int Rating
        {
            get { return _rating; }
            set
            {
                // 0 rating indicates no opinion
                if (value >= 0 && value <= 4)
                {
                    _rating = value;
                }
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Rating"));
                }
            }
        }

        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Comment"));
                }
            }
        }

        public Uri AlbumImage
        {
            get { return _albumImage; }
            set
            {
                _albumImage = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AlbumImage"));
                }
            }
        }

        public ObservableCollection<string> Songs
        {
            get { return _songs; }
            set
            {
                _songs = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Songs"));
                }
            }
        }

        public Album()
        {

        }

        public Album(string albumName,
                     string artistName,
                     string genre,
                     int year,
                     int rating,
                     string comment,
                     Uri albumImage,
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

        public event PropertyChangedEventHandler PropertyChanged;

        #region ICloneable Members

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
