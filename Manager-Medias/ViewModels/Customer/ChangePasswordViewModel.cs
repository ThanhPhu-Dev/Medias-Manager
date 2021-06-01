using Manager_Medias.Commands;
using Manager_Medias.Functions;
using Manager_Medias.Models;
using Manager_Medias.Services;
using Manager_Medias.Stores;
using Manager_Medias.Validates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        private readonly UserStore _userStore;

        #region Command

        public ICommand SaveCmd { get; }
        public ICommand NavigateHomeCmd { get; }

        #endregion Command

        #region Binding

        private string _currentPw;

        public string CurrentPw
        {
            get => _currentPw;
            set
            {
                ValidateProperty(value);
                _currentPw = value;
                OnPropertyChanged();
            }
        }

        private string _newPw;

        public string NewPw
        {
            get => _newPw;
            set
            {
                ValidateProperty(value);
                _newPw = value;
                OnPropertyChanged();
            }
        }

        private string _confirmPw;

        public string ConfirmPw
        {
            get => _confirmPw;
            set
            {
                ValidateProperty(value);
                _confirmPw = value;
                OnPropertyChanged();
            }
        }

        #endregion Binding

        public ChangePasswordViewModel(UserStore userStore, NavigationStore navigationStore)
        {
            _userStore = userStore;
            _navigationStore = navigationStore;

            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();

            // Create a Dictionary of validation rules for fast lookup.
            // Each property name of a validated property maps to one or more ValidationRule.
            this.ValidationRules.Add(nameof(this.CurrentPw), new List<ValidationRule>() { new ValidatePassword() });
            this.ValidationRules.Add(nameof(this.NewPw), new List<ValidationRule>() { new ValidatePassword() });
            this.ValidationRules.Add(nameof(this.ConfirmPw), new List<ValidationRule>() { new ValidatePassword() });

            SaveCmd = new RelayCommand<Object>(ActionChangePassword, (Object) => !HasErrors);
            NavigateHomeCmd = new NavigateCommand<HomeViewModel>(new NavigationService<HomeViewModel>(_navigationStore, () => new HomeViewModel()));
        }

        private void ActionChangePassword(object obj)
        {
            string hashedPassword = _userStore.CurrentUser.Password;
            bool compare = HashPassword.ComparePassword(CurrentPw, hashedPassword);

            if (NewPw != ConfirmPw)
            {
                AddError("ConfirmPw", "Mật khẩu xác nhận không đúng");
            }

            if (!compare)
            {
                AddError("CurrentPw", "Mật khẩu không chính xác");
            }
            else
            {
                string pwHash = HashPassword.Hash(NewPw);
                using (var db = new MediasManangementEntities())
                {
                    var account = db.Users.Single(u => u.Email == _userStore.Email);
                    account.Password = pwHash;
                    db.SaveChanges();
                }
            }
        }
    }
}