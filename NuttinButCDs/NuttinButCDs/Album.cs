using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows;

namespace NuttinButCDs
{
    public class Album : INotifyPropertyChanged, ICloneable
    {
        private int    _albumId;
        private string _albumName;
        private string _artistName;
        private string _genre;
        private int    _year;
        private int    _rating;
        private string _comment;
        private Uri    _albumImageSmall;
        private Uri    _albumImageLarge;
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
                else if (value.Length > Constants.MaxAlbumNameLength)
                {
                    throw new ApplicationException("");
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
                else if (value.Length > Constants.MaxArtistNameLength)
                {
                    throw new ApplicationException("");
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
                if (String.IsNullOrEmpty(value) || value.Length <= Constants.MaxGenreLength)
                {
                    _genre = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Genre"));
                    }
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
                // minRating rating indicates no opinion
                if (value >= Constants.MinRating && value <= Constants.MaxRating)
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
                if (!String.IsNullOrEmpty(value) && value.Length > Constants.MaxCommentLength)
                {
                    throw new ApplicationException("");
                }
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
            if (!String.IsNullOrEmpty(albumName) &&
                albumName.Length <= Constants.MaxAlbumNameLength &&

                ((artistName != null && artistName.Length <= Constants.MaxArtistNameLength) || artistName == null) &&
                ((genre != null && genre.Length <= Constants.MaxGenreLength) || genre == null) &&
                year >= Constants.EarliestYear &&

                rating >= Constants.MinRating &&
                rating <= Constants.MaxRating &&

                ((comment != null && comment.Length <= Constants.MaxCommentLength) || comment == null) &&

                ((albumImageSmall != null &&
                albumImageSmall.OriginalString.Length <= Constants.MaxAlbumImageLength) ||
                albumImageSmall == null) &&

                ((albumImageLarge != null &&
                albumImageLarge.OriginalString.Length <= Constants.MaxAlbumImageLength) ||
                albumImageLarge == null) )
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
            else
            {
                MessageBox.Show("Failed to create an album!");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region ICloneable Members

        public object Clone()
        {
            return MemberwiseClone();
        }

        #endregion
    }
}
