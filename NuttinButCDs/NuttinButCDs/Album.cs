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
        private int _albumId;
        private string _albumName;
        private string _artistName;
        private string _genre;
        private int _year;
        private int _rating;
        private string _comment;
        private Uri _albumImageSmall;
        private Uri _albumImageLarge;
        private ObservableCollection<string> _songs = new ObservableCollection<string>();

        public string AlbumName
        {
            get { return _albumName; }
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    string errorMsg = "No album name??";
                    throw new ApplicationException(errorMsg);
                }
                else if (value.Length > 200)
                {
                    string errorMsg = "Too long!";
                    throw new ApplicationException(errorMsg);
                }
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
                if (String.IsNullOrEmpty(value))
                {
                    string errorMsg = "Surely someone made this album!";
                    throw new ApplicationException(errorMsg);
                }
                else if (value.Length > 100)
                {
                    string errorMsg = "Too long!";
                    throw new ApplicationException(errorMsg);
                }
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
                // TODO: Validate
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
                if (value >= MainWindow.Years.First() && value <= MainWindow.Years.Last())
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
                // TODO: Validate
                _comment = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Comment"));
                }
            }
        }

        public Uri AlbumImageSmall
        {
            get { return _albumImageSmall; }
            set
            {
                // TODO: Validate
                _albumImageSmall = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AlbumImageSmall"));
                }
            }
        }

        public Uri AlbumImageLarge
        {
            get { return _albumImageLarge; }
            set
            {
                // TODO: Validate
                _albumImageLarge = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AlbumImageLarge"));
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

        public int AlbumID
        {
            get { return _albumId; }
            set { _albumId = value; }
        }


        public Album()
        {

        }

        public Album(int id,
                     string albumName,
                     string artistName,
                     string genre,
                     int year,
                     int rating,
                     string comment,
                     Uri albumImageSmall,
                     Uri albumImageLarge,
                     ObservableCollection<string> songs)
        {
            AlbumID = id;
            AlbumName = albumName;
            ArtistName = artistName;
            Genre = genre;
            Year = year;
            Rating = rating;
            Comment = comment;
            AlbumImageSmall = albumImageSmall;
            AlbumImageLarge = albumImageLarge;
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
