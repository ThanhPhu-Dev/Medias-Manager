using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Manager_Medias.Validates
{
    public class CheckAppyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(bool)value)
            {
                return new ValidationResult(false, "Bạn chưa đồng ý với điều khoản!");
            }

            return ValidationResult.ValidResult;
        }
    }
}
