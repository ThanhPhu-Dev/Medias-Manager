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
    internal class ValidateEmail : ValidationRule
    {
        private Regex rg = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

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

            if (!rg.IsMatch(Name.ToString()))
            {
                return new ValidationResult(false, "Định dạng email không đúng");
            }

            return ValidationResult.ValidResult;
        }
    }
}