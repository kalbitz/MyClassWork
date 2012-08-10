using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;

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
                else if (value.Length > Constants.maxAlbumNameLength)
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
                else if (value.Length > Constants.maxArtistNameLength)
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
                if (String.IsNullOrEmpty(value) || value.Length <= Constants.maxGenreLength)
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
                if (value >= Constants.minRating && value <= Constants.maxRating)
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
                if (!String.IsNullOrEmpty(value) && value.Length > Constants.maxCommentLength)
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
                albumName.Length <= Constants.maxAlbumNameLength &&

                ((artistName != null && artistName.Length <= Constants.maxArtistNameLength) || artistName == null) &&
                ((genre != null && genre.Length <= Constants.maxGenreLength) || genre == null) &&
                year >= Constants.earliestYear &&

                rating >= Constants.minRating &&
                rating <= Constants.maxRating &&

                ((comment != null && comment.Length <= Constants.maxCommentLength) || comment == null) &&

                ((albumImageSmall != null &&
                albumImageSmall.OriginalString.Length <= Constants.maxAlbumImageLength) ||
                albumImageSmall == null) &&

                ((albumImageLarge != null &&
                albumImageLarge.OriginalString.Length <= Constants.maxAlbumImageLength) ||
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
            return this.MemberwiseClone();
        }

        #endregion
    }
}
