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
    public class CartNumberValidationRule : ValidationRule
    {
        Regex rg = new Regex(@"^[0-9]{16}$");
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string Name))
            {
                return new ValidationResult(false, "Số thẻ là chuỗi.");
            }

            if (string.IsNullOrEmpty(Name))
            {
                return new ValidationResult(false, "Số thẻ không được bỏ trống");
            }

            if (!rg.IsMatch(Name.ToString()))
            {
                return new ValidationResult(false, "Số thẻ phải là số dài 16 ký tự");
            }

            return ValidationResult.ValidResult;
        }
    }
}
