using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Manager_Medias.Validates
{
    public class DateExpirationValidationRule : ValidationRule
    {
        Regex rg = new Regex(@"^(0[1-9]|1[0-2])\/?([0-9]{2})$");
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string Name))
            {
                return new ValidationResult(false, "Ngày hết hạn thẻ là chuỗi.");
            }

            if (string.IsNullOrEmpty(Name))
            {
                return new ValidationResult(false, "Ngày hết hạn thẻ không được bỏ trống");
            }

            if (!rg.IsMatch(Name.ToString()))
            {
                return new ValidationResult(false, "Ngày hết hạn có phải có dạng MM/YY");
            }

            return ValidationResult.ValidResult;
        }
    }
}
