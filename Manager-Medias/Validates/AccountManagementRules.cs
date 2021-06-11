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
    public class AccountManagementRules : ValidationRule
    {
        public string property { get; set; }


        private string _patternEmail = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        private Regex rgPassword = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (property == "Email")
            {
                string email = value.ToString();
                if(email.Length == 0)
                {
                    return new ValidationResult(false, "Hãy nhập địa chỉ mail!");
                }
                if (!Regex.IsMatch(email, _patternEmail))
                {
                    return new ValidationResult(false, "Email không hợp lệ!");
                }
            }
            if (property == "Code")
            {
                if (((string)value).Length < 4)
                {
                    return new ValidationResult(false, "Code phải có 5 ký tự trở lên!");
                }
            }

            if (property == "Status")
            {
                if ((string)value != "1" && (string)value != "0")
                {
                    return new ValidationResult(false, "Chỉ nhập 1 hoặc 0 \n1: Còn hoạt động, 0: Không còn hoạt động!");
                }
            }


            if (property == "Level")
            {
                int level = 0;
                try
                {
                    if (((string)value).Length > 0)
                    {
                        level = Int32.Parse((String)value);
                    }    
                    else
                    {
                        return new ValidationResult(false, "Hãy nhập cấp độ tài khoản!");
                    }

                    if(level > 3)
                    {
                        return new ValidationResult(false, "Level phải nằm trong khoảng 0 - 3!");
                    }
                }
                catch (Exception e)
                {
                    return new ValidationResult(false, "Không được nhập ký tự ngoài chữ số! - " + e.Message);
                }
            }

            if(property == "Password")
            {
                if (!rgPassword.IsMatch(value.ToString()))
                {
                    return new ValidationResult(false, "Password có tối thiểu 8 ký tự gồm ít nhất 1 số");
                }
            }

            if (property == "NumberCard")
            {

                if (((string)value).Length == 0)
                {
                    return new ValidationResult(false, "Hãy nhập số thẻ!");
                }

                if (((string)value).Length != 16)
                {
                    return new ValidationResult(false, "Số thẻ phải có 16 ký tự!");
                }
               
            }
            if(property == "FullName")
            {
                if (((string)value).Length == 0)
                {
                    return new ValidationResult(false, "Hãy nhập họ tên!");
                }
            }
            if (property == "Exp")
            {
                if (((string)value).Length == 0)
                {
                    return new ValidationResult(false, "Hãy nhập tích lũy!");
                }
            }
            return new ValidationResult(true, null);

        }
    }
}
