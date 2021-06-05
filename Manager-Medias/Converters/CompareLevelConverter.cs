using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Manager_Medias.Converters
{
    public class CompareLevelConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null) return false;

            var userLevel = values[0];
            var mediaLevel = values[1];

            if (userLevel != DependencyProperty.UnsetValue && mediaLevel != DependencyProperty.UnsetValue)
            {
                return (int)userLevel >= (int)mediaLevel ? true : false;
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}