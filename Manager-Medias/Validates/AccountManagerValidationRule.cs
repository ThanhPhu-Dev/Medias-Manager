using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
                return new ValidationResult(false, "Value must be of type string.");
            }

            if (!Name.StartsWith("@"))
            {
                return new ValidationResult(false, "Input must start with '@'.");
            }

            return ValidationResult.ValidResult;
        }
    }
}