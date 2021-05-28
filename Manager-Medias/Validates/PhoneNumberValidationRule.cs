using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Manager_Medias.Validates
{
    public class PhoneNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string PhoneNumber))
            {
                return new ValidationResult(false, "Số điện thoại phải là chuỗi.");
            }

            if (string.IsNullOrEmpty(PhoneNumber))
            {
                return new ValidationResult(false, "Số điện thoại không được bỏ trống");
            }

            if (PhoneNumber.Length > 10)
            {
                return new ValidationResult(false, "Số điện thoại không hợp lệ");
            }

            return ValidationResult.ValidResult;
        }
    }
}