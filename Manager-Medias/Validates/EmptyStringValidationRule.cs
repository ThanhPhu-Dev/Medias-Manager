using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Manager_Medias.Validates
{
    public class EmptyStringValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if ((string)value == string.Empty)
            {
                return new ValidationResult(false, "Cần điền đủ thông tin để tiếp tục");
            }

            return ValidationResult.ValidResult;
        }
    }
}