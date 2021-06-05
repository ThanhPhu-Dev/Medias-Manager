using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Manager_Medias.Converters
{
    public class MoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null)
            {
                return null;
            }
            var money = int.Parse(value.ToString());
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            string result = money.ToString("#,###", cul.NumberFormat) + " VND";
            //string yourValue = (money / 100m).ToString("C1");
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
