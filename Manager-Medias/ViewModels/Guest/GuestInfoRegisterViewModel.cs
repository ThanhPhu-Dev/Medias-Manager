using Manager_Medias.Commands;
using Manager_Medias.Models;
using Manager_Medias.Stores;
using Manager_Medias.Validates;
using Manager_Medias.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Guest
{
    internal class GuestInfoRegisterViewModel : BaseViewModel
    {
        public ICommand CmdContinue { get; }

        private string _email;
        private string _password;
        private string _errorMS;

        #region BindingProperty

        public string ErrorMS
        {
            get => _errorMS;
            set
            {
                _errorMS = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                ValidateProperty(value);
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                ValidateProperty(value);
                _password = value;
                OnPropertyChanged();
            }
        }

        #endregion BindingProperty

        public GuestInfoRegisterViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            CmdContinue = new RelayCommand<Object[]>(Continue, (Object[] obj) => !HasErrors);
            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();

            this.ValidationRules.Add(nameof(this.Email), new List<ValidationRule>() { new ValidateEmail() });
            this.ValidationRules.Add(nameof(this.Password), new List<ValidationRule>() { new ValidatePassword() });

            //khoi tao erro là null
            ErrorMS = null;
        }

        private void Continue(object[] obj)
        {
            //hash password
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, salt.Length);
            Array.Copy(hash, 0, hashBytes, salt.Length, hash.Length);

            string pwHash = Convert.ToBase64String(hashBytes);

            //tạo user
            User user = new User()
            {
                Email = Email,
                Password = pwHash,
            };
            using (var db = new MediasManangementEntities())
            {
                //check mail da ton tai chua
                var n_user = db.Users.Where(u => u.Email == user.Email).Count();

                if (n_user > 0)
                {
                    ErrorMS = "Email đã tồn tại!!";
                }
                else
                {
                    db.Users.Add(user);
                    if (db.SaveChanges() > 0)
                    {
                        //chuyển trang
                        _navigationStore.ContentViewModel = new GuestLevelRegisterViewModel(user);
                    }
                    else
                    {
                        ErrorMS = "Đăng ký tài khoản lỗi, vui lòng thử lại!";
                    }
                }
            }
        }
    }
}