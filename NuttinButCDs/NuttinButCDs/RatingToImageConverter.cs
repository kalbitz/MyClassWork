using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;

namespace NuttinButCDs
{
    class RatingToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int rating = (int) value;
            Image ratingImage = new Image();
            switch (rating)
            {
                case 0:
                {
                    return ratingImage;
                }
                case 1:
                {
                    ratingImage.FindResource("oneStar");
                    return ratingImage;
                }
                case 2:
                {
                    ratingImage.FindResource("twoStar");
                    return ratingImage;
                }
                case 3:
                {
                    ratingImage.FindResource("threeStar");
                    return ratingImage;
                }
                case 4:
                {
                    ratingImage.FindResource("fourStar");
                    return ratingImage;
                }
            } ;
            return ratingImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
