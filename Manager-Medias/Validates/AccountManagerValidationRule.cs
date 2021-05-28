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
    public class AccountManagerValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string Name))
            {
                return new ValidationResult(false, "Họ tên phải là chuỗi.");
            }

            if (string.IsNullOrEmpty(Name))
            {
                return new ValidationResult(false, "Họ tên không được bỏ trống");
            }

            if (Name.Length > 100)
            {
                return new ValidationResult(false, "Họ tên quá dài");
            }

            return ValidationResult.ValidResult;
        }
    }
}