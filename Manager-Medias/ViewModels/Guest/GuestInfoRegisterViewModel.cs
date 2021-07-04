using Manager_Medias.Commands;
using Manager_Medias.Functions;
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

        public GuestInfoRegisterViewModel()
        {
            CmdContinue = new RelayCommand<Object[]>(Continue, (Object[] obj) => !HasErrors);
            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();

            this.ValidationRules.Add(nameof(this.Email), new List<ValidationRule>() { new ValidateEmailRegister() });
            this.ValidationRules.Add(nameof(this.Password), new List<ValidationRule>() { new ValidatePassword() });

            //khoi tao erro là null
            ErrorMS = null;
        }

        private void Continue(object[] obj)
        {
            using (var db = new MediasManangementEntities())
            {
                //check mail da ton tai chua
                var n_user = db.Users.Where(u => u.Email == Email).Count();

                if (n_user > 0)
                {
                    AddError(nameof(Email), "Email đã được sử dụng");
                }
                else
                {
                    //hash password
                    string pwHash = HashPassword.Hash(Password);

                    //tạo user
                    User user = new User()
                    {
                        Email = Email,
                        Password = pwHash,
                        roleId = 1,
                    };

                    db.Users.Add(user);
                    if (db.SaveChanges() > 0)
                    {
                        //chuyển trang
                        _navigationStore.ContentViewModel = new GuestLevelRegisterViewModel(user);
                    }
                }
                
            }
        }
    }
}