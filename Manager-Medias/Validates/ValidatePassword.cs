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
    class ValidatePassword : ValidationRule
    {
        Regex rg = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
       
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string Name))
            {
                return new ValidationResult(false, "Value must be of type string.");
            }

            if (string.IsNullOrEmpty(Name))
            {
                return new ValidationResult(false, "Cần điền đủ thông tin để tiếp tục");
            }

            if (!rg.IsMatch(value.ToString()))
            {
                return new ValidationResult(false, "Tối thiểu 8 ký tự và có ít nhất một số");
            }

            return ValidationResult.ValidResult;
        }
    }
}
