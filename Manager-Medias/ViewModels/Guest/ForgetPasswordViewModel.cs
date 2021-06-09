using Manager_Medias.Commands;
using Manager_Medias.Functions;
using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Guest
{
    class ForgetPasswordViewModel: BaseViewModel
    {
        public ICommand Submit { get; set; }

        private string passwordRamdom;
        public string PasswordRamdom
        {
            get => passwordRamdom;
            set
            {
                passwordRamdom = value;
                OnPropertyChanged();
            }
        }

        private string mess;
        public string Mess
        {
            get => mess;
            set
            {
                mess = value;
                OnPropertyChanged();
            }
        }

        public ForgetPasswordViewModel()
        {
            Submit = new RelayCommand<Object[]>(ChangePassword);
        }

        private void ChangePassword(object[] obj)
        {
            string email = obj[0].ToString();
            string tenprofile = obj[1].ToString();
            using (var db = new MediasManangementEntities())
            {
                Profile profile = db.Profiles.Where(p => p.Email == email && p.Name == tenprofile).FirstOrDefault() as Profile;
                User u = db.Users.Where(p => p.Email == email).FirstOrDefault() as User;
                if (profile != null)
                {
                    Randompassword();
                    string haspass = HashPassword.Hash(PasswordRamdom);
                    u.Password = haspass;
                    u.Code = haspass;
                    db.SaveChanges();
                    Mess = "Mật khẩu này chỉ dùng được 1 lần sau khi đăng nhập";
                }
                else
                {
                    Mess = "Email hoặc tên không tồn tại";
                }
            }
        }
        private void Randompassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomNumber(1000, 9999));
            builder.Append(RandomString(2, false));
            PasswordRamdom = builder.ToString();
        }
        private int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        private string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
    }
}
