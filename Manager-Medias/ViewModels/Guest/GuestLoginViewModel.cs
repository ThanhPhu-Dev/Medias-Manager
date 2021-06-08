﻿using Manager_Medias.Commands;
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
            LoginCmd = new RelayCommand<Object[]>(ActionLogin, (Object[] obj) => !HasErrors);
            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();

            this.ValidationRules.Add(nameof(this.Email), new List<ValidationRule>() { new ValidateEmail() });
            this.ValidationRules.Add(nameof(this.Password), new List<ValidationRule>() { new ValidatePassword() });
            fogetPassword = new RelayCommand<Object[]>(forgetPassword);
        }

        private void forgetPassword(object[] obj)
        {
            //_navigationStore.ContentViewModel = new ForgetPasswordViewModel();
                _userStore = null;
                _navigationStore.CurrentViewModel = new GuestMainViewModel();
                _navigationStore.ContentViewModel = new ForgetPasswordViewModel();
        }

        public async void ActionLogin(object[] values)
        {
            //if (string.IsNullOrEmpty(values.ToString()) || string.IsNullOrEmpty(values[0].ToString()) ||
            //    string.IsNullOrEmpty(values[1].ToString()))
            //{
            //    return;
            //}

            //User currentUser;
            //using (var db = new MediasManangementEntities())
            //{
            //    currentUser = db.Users.Where(p => p.Email == Email).FirstOrDefault() as User;
            //}

            //if (currentUser != null)
            //{
            //    bool compare = HashPassword.ComparePassword(Password, currentUser.Password);

            //    Error = !compare ? "Mật khẩu không đúng" : null;

            //    UserStore userStore = new UserStore(currentUser);
            //    if (currentUser.Level == null)
            //    {
            //        _navigationStore.ContentViewModel = new GuestLevelRegisterViewModel(currentUser, _navigationStore);
            //    }
            //    else if (currentUser.NumberCard == null)
            //    {
            //        _navigationStore.ContentViewModel = new GuestCartRegisterViewModel(currentUser, _navigationStore);
            //    }
            //    else
            //    {
            //        _navigationStore.CurrentViewModel = new MainLayoutViewModel(userStore, _navigationStore);
            //        _navigationStore.ContentViewModel = new HomeMovieViewModel(_navigationStore);
            //    }
            //}
            //else
            //{
            //    Error = "Tài khoản không tồn tại";
            //    return;
            //}
            IsLoading = true;

            User user = await Task.Run(() =>
            {
                using (var db = new MediasManangementEntities())
                {
                    return db.Users.Single(u => u.Email == "nghiadx2001@gmail.c");
                }
            }).ContinueWith((task) =>
            {
                IsLoading = false;

                return task.Result;
            }).ConfigureAwait(false);

            //Application.Current.Dispatcher.Invoke(() =>
            //{
            //    _userStore = new UserStore(user);
            //    _navigationStore.CurrentViewModel = new MainLayoutViewModel();
            //    _navigationStore.ContentViewModel = new HomeViewModel();
            //});
            _navigationStore.ContentViewModel = new GuestSetNewPasswordViewModel();

            //_navigationStore.ContentViewModel = new DetailAudioViewModel(1);
        }
    }
}