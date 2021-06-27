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

        private Regex NoNumberAndSpecialChar = new Regex(@"^[\p{L} ]+$");
        private Regex intOrDou = new Regex(@"-?\d+(?:\.\d+)?");
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
            if (property == "Video")
            {
                string name = value.ToString();
                if (name.Substring(name.Length - 3) != "mp4")
                {
                    return new ValidationResult(false, "Bạn phải chọn file mp4");
                }
            }
            if (property == "Poster")
            {
                string name = value.ToString();
                if (name.Substring(name.Length - 3) != "jpg" && name.Substring(name.Length - 3) != "png")
                {
                    return new ValidationResult(false, "Bạn phải chọn file hình ảnh");
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

            if (property == "Season")
            {
                if (value.ToString().Length == 0)
                {
                    return new ValidationResult(true, null);
                }
                if (value.ToString().Length > 0)
                {
                    try
                    {
                        int season = int.Parse(value.ToString());
                    }
                    catch
                    {
                        return new ValidationResult(false, "Phải nhập số nguyên cho mục này!");
                    }
                }     
            }

            if (property == "IMDB")
            {
                if(value == null || value.ToString().Length == 0)
                {
                    return new ValidationResult(true, null);
                }

                if (!intOrDou.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "Phải nhập số cho trường này!");
                }
                else
                {
                    double imdb = double.Parse(value.ToString());
                    if(imdb > 10)
                    {
                        return new ValidationResult(false, "Điểm IMDB phải nhỏ hơn 10.0!");
                    }
                }
                  
            }

            if (property == "Age")
            {
                if (value.ToString().Length == 0)
                {
                    return new ValidationResult(false, "Hãy độ tuổi cho phép của phim!");
                }
                if (value.ToString().Length > 0)
                {
                    try
                    {
                        int age = int.Parse(value.ToString());
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
