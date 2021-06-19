using Manager_Medias.Commands;
using Manager_Medias.Functions;
using Manager_Medias.Models;
using Manager_Medias.Stores;
using Manager_Medias.Validates;
using Manager_Medias.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Guest
{
    public class GuestLoginViewModel : BaseViewModel
    {
        public ICommand LoginCmd { get; set; }
        public ICommand fogetPassword { get; set; }
        private string _email;
        private string _password;
        private string _error;

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

        public string Error
        {
            get => _error;
            set
            {
                ValidateProperty(value);
                _error = value;
                OnPropertyChanged();
            }
        }

        public GuestLoginViewModel()
        {
            LoginCmd = new RelayCommand<Object>(ActionLogin, (Object obj) => !HasErrors);
            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();

            this.ValidationRules.Add(nameof(this.Email), new List<ValidationRule>() { new ValidateEmail() });
            this.ValidationRules.Add(nameof(this.Password), new List<ValidationRule>() { new ValidatePassword() });
            fogetPassword = new RelayCommand<Object[]>(forgetPassword);
        }

        private void forgetPassword(object[] obj)
        {
                _userStore = null;
                _navigationStore.ContentViewModel = new ForgetPasswordViewModel();
        }

        public async void ActionLogin(object values)
        {
            if(values != null)
            {
                //UserControl win = (UserControl)values;

            }
            //if (string.IsNullOrEmpty(values.ToString()) || string.IsNullOrEmpty(values[0].ToString()) ||
            //    string.IsNullOrEmpty(values[1].ToString()))
            //{
            //    return;
            //}
            IsLoading = true;
            User currentUser = await Task.Run(() =>
            {
                using (var db = new MediasManangementEntities())
                {
                    return db.Users.Where(p => p.Email == Email).FirstOrDefault() as User;
                }
            }).ContinueWith((task) =>
            {
                IsLoading = false;

                return task.Result;
            }).ConfigureAwait(false);

            if (currentUser != null)
            {
                bool compare = HashPassword.ComparePassword(Password, currentUser.Password);

                Error = !compare ? "Mật khẩu không đúng" : null;
                if(compare == true)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        _userStore = new UserStore(currentUser);
                        if(currentUser.roleId == 2)
                        {
                            
                        }
                        else if (currentUser.Code != null)
                        {
                            _navigationStore.ContentViewModel = new GuestSetNewPasswordViewModel();
                        }else if (currentUser.Level == null)
                        {
                            _navigationStore.ContentViewModel = new GuestLevelRegisterViewModel(currentUser);
                        }
                        else if (currentUser.NumberCard == null)
                        {
                            _navigationStore.ContentViewModel = new GuestCartRegisterViewModel(currentUser);
                        }
                        else
                        {
                            _navigationStore.CurrentViewModel = new MainLayoutViewModel();
                            _navigationStore.ContentViewModel = new HomeViewModel();
                        }
                    });
                }
                else
                {
                    Error = "Mật khẩu không chính xác";
                    return;
                }
                
            }
            else
            {
                Error = "Tài khoản không tồn tại";
                return;
            }

            //User user = await Task.Run(() =>
            //{
            //    using (var db = new MediasManangementEntities())
            //    {
            //        return db.Users.Where(p => p.Email == Email).FirstOrDefault() as User;
            //    }
            //}).ContinueWith((task) =>
            //{
            //    IsLoading = false;

            //    return task.Result;
            //}).ConfigureAwait(false);


            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    _userStore = new UserStore(user);
            //    _navigationStore.CurrentViewModel = new MainLayoutViewModel();
            //    _navigationStore.ContentViewModel = new HomeViewModel();
            //    //_navigationStore.ContentViewModel = new GuestSetNewPasswordViewModel();

            //});

            //_navigationStore.ContentViewModel = new DetailAudioViewModel(1);
        }
    }
}