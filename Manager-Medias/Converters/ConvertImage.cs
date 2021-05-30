using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Manager_Medias.Converters
{
    internal class ConvertImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            string val = value.ToString();
            var type = val.Split('_');
            StringBuilder url = new StringBuilder(AppDomain.CurrentDomain.BaseDirectory + "Images" + Path.DirectorySeparatorChar);
            switch (type[0].Trim())
            {
                case "album":
                    url.Append("Album" + Path.DirectorySeparatorChar);
                    break;
                case "audiomp3":
                    url.Append("Audio" + Path.DirectorySeparatorChar + "mp3" + Path.DirectorySeparatorChar);
                    break;
                case "audioposter":
                    url.Append("Audio" + Path.DirectorySeparatorChar + "Poster" + Path.DirectorySeparatorChar);
                    break;
                case "movieposter":
                    url.Append("Movie" + Path.DirectorySeparatorChar + "Poster" + Path.DirectorySeparatorChar);
                    break;
                case "movievideo":
                    url.Append("Movie" + Path.DirectorySeparatorChar + "Video" + Path.DirectorySeparatorChar);
                    break;

            }
            return url.Append(val).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}