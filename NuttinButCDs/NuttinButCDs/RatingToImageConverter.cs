using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NuttinButCDs.Properties;

namespace NuttinButCDs
{
    class RatingToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int rating = (int)value;

            switch (rating)
            {
                case 1:
                    {
                        return System.Windows.Application.Current.TryFindResource("oneStar");
                    }
                case 2:
                    {
                        return System.Windows.Application.Current.TryFindResource("twoStar");
                    }
                case 3:
                    {
                        return System.Windows.Application.Current.TryFindResource("threeStar");
                    }
                case 4:
                    {
                        return System.Windows.Application.Current.TryFindResource("fourStar");
                    }
                case 0:
                default:
                    {
                        return System.Windows.Application.Current.TryFindResource("noStar");
                    }
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
