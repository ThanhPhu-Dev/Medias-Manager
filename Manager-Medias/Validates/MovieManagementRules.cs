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
    class MovieManagementRules : ValidationRule
    {
        public string property { get; set; }

        private Regex NoNumberAndSpecialChar = new Regex(@"/[^a-zA-Z ]/g");
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (property == "Name")
            {
                if (value.ToString().Length == 0)
                {
                    return new ValidationResult(false, "Hãy nhập tên phim!");
                }
            }
            if (property == "Descrip")
            {
                if (value.ToString().Length <= 20)
                {
                    return new ValidationResult(false, "Mô tả phim phải có hơn 20 ký tự!");
                }
            }
            if (property == "Nation")
            {
                if (value.ToString().Length == 0)
                {
                    return new ValidationResult(false, "Hãy nhập tên quốc gia sản xuất phim!");
                }
                if (!NoNumberAndSpecialChar.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "Tên quốc gia không có số và ký tự đặc biệt");
                }
            }
            if (property == "Directors")
            {
                if (value.ToString().Length == 0)
                {
                    return new ValidationResult(false, "Hãy nhập hoặc cập nhật tên đạo diễn!");
                }
                if (!NoNumberAndSpecialChar.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "Tên đạo diễn không có số và ký tự đặc biệt");
                }
            }

            if (property == "Seasion")
            {
                if(value.ToString().Length > 0)
                {
                    try
                    {
                        int season = int.Parse(value.ToString());
                    }
                    catch(Exception e)
                    {
                        return new ValidationResult(false, "Phải nhập số nguyên cho mục này!");
                    }
                }     
            }

            if (property == "IMDB")
            {
                if (value.ToString().Length > 0)
                {
                    try
                    {
                        double season = double.Parse(value.ToString());
                    }
                    catch (Exception e)
                    {
                        return new ValidationResult(false, "Phải nhập số cho mục này!");
                    }
                }
            }

            if (property == "Age")
            {
                if (value.ToString().Length > 0)
                {
                    try
                    {
                        int season = int.Parse(value.ToString());
                    }
                    catch(Exception e)
                    {
                        return new ValidationResult(false, "Phải nhập số nguyên cho mục này!");
                    }
                }

            }


            return new ValidationResult(true, null);
        }
    }
}
