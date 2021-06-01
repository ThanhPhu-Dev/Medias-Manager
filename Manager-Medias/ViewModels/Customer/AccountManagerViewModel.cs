using Manager_Medias.Commands;
using Manager_Medias.Stores;
using Manager_Medias.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Globalization;
using Manager_Medias.Validates;
using Manager_Medias.Models;
using System.Windows;

namespace Manager_Medias.ViewModels.Customer
{
    public class AccountManagerViewModel : BaseViewModel
    {
        #region Command

        public ICommand SaveCmd { get; }
        public ICommand NavigateHomeCmd { get; }

        #endregion Command

        #region BindingProperty

        private string _cardNumber;

        public string CardNumber
        {
            get => _cardNumber;
            set
            {
                ValidateProperty(value);
                _cardNumber = value;
                OnPropertyChanged();
            }
        }

        private string _expires;

        public string Expires
        {
            get => _expires;
            set
            {
                ValidateProperty(value);
                _expires = value;
                OnPropertyChanged();
            }
        }

        #endregion BindingProperty

        public AccountManagerViewModel()
        {
            NavigateHomeCmd = new NavigateCommand<HomeViewModel>(new NavigationService<HomeViewModel>(_navigationStore, () => new HomeViewModel()));

            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();

            // Create a Dictionary of validation rules for fast lookup.
            // Each property name of a validated property maps to one or more ValidationRule.
            //this.ValidationRules.Add(nameof(this.CardNumber), new List<ValidationRule>() { new () });
            this.ValidationRules.Add(nameof(this.Expires), new List<ValidationRule>() { new DateExpirationValidationRule() });
            InitValidate();
            GetCurrentData();

            SaveCmd = new RelayCommand<Object>(ActionSave, (Object) => !HasErrors);
        }

        public void InitValidate()
        {
            ValidateProperty(CardNumber, "CardNumber");
            ValidateProperty(Expires, "Expires");
        }

        public void GetCurrentData()
        {
            using (var db = new MediasManangementEntities())
            {
                var account = db.Users.Single(u => u.Email == _userStore.Email);
                CardNumber = account.NumberCard;
            }
        }

        public void ActionSave(Object o)
        {
            using (var db = new MediasManangementEntities())
            {
                var account = db.Users.Single(u => u.Email == _userStore.Email);
                account.NumberCard = CardNumber;
                db.SaveChanges();
                MessageBox.Show("Cập nhật thông tin thành công", "Thành công", MessageBoxButton.OK);
            }
        }
    }
}