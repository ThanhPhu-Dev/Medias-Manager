using Manager_Medias.Commands;
using Manager_Medias.Functions;
using Manager_Medias.Models;
using Manager_Medias.Stores;
using Manager_Medias.Validates;
using Manager_Medias.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Guest
{
    public class GuestSetNewPasswordViewModel : BaseViewModel
    {
        public ICommand CmdConfirm { get; set; }
        private string _password;
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
        public GuestSetNewPasswordViewModel()
        {
            //_userEmail = userEmail;

            CmdConfirm = new RelayCommand<Object[]>(Confirm, (Object[] obj) => !HasErrors);
            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();

            this.ValidationRules.Add(nameof(this.Password), new List<ValidationRule>() { new ValidatePassword() });
        }

        private void Confirm(object[] obj)
        {
            
            if(string.IsNullOrEmpty(Password))
            {
                AddError(nameof(Password), "Mật khẩu không được bỏ trống!");
                return;
            }
            using (var db = new MediasManangementEntities())
            {
                //check mail da ton tai chua
                var user = db.Users.Where(u => u.Email == _userStore.CurrentUser.Email).Single() as User;

                //hash password
                string pwHash = HashPassword.Hash(Password);

                user.Password = pwHash;
                if (db.SaveChanges() > 0)
                {
                    //chuyển trang
                    _userStore = new UserStore(user);
                    _navigationStore.CurrentViewModel = new MainLayoutViewModel();
                    _navigationStore.ContentViewModel = new HomeViewModel();
                }
            }
        }
    }
}
