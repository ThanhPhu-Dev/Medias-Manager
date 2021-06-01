using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Manager_Medias.Converters
{
    public class ProfileImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currentfolder = AppDomain.CurrentDomain.BaseDirectory;
            if (value == null)
            {
                return currentfolder + "Images\\Profile\\" + "default_avatar.png";
            }
            string img = value.ToString();
            
            if (Path.IsPathRooted(img))
            {
                return img;
            }

            string url = currentfolder + "Images\\Profile\\" + img;
            if (!File.Exists(url))
            {
                return currentfolder + "Images\\Profile\\" + "default_avatar.png";
            }
            return url;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}