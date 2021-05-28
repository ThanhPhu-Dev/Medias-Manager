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
        private readonly UserStore _userStore;

        #region Command

        public ICommand SaveCmd { get; }
        public ICommand NavigateHomeCmd { get; }

        #endregion Command

        #region BindingProperty

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                ValidateProperty(value);
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _phoneNumber;

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                ValidateProperty(value);
                _phoneNumber = value;
                OnPropertyChanged();
            }
        }

        #endregion BindingProperty

        // Change email to name
        //public string Name => _userStore.CurrentUser.Email;

        //public string PhoneNumber => _userStore.CurrentUser.PhoneNumber;

        public AccountManagerViewModel(UserStore userStore, NavigationStore navigationStore)
        {
            _userStore = userStore;
            _navigationStore = navigationStore;
            NavigateHomeCmd = new NavigateCommand<HomeViewModel>(new NavigationService<HomeViewModel>(_navigationStore, () => new HomeViewModel()));

            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();

            // Create a Dictionary of validation rules for fast lookup.
            // Each property name of a validated property maps to one or more ValidationRule.
            this.ValidationRules.Add(nameof(this.Name), new List<ValidationRule>() { new AccountManagerValidationRule() });
            this.ValidationRules.Add(nameof(this.PhoneNumber), new List<ValidationRule>() { new PhoneNumberValidationRule() });
            InitValidate();

            SaveCmd = new RelayCommand<Object[]>(ActionSave, (Object[] obj) => !HasErrors);
        }

        public void InitValidate()
        {
            ValidateProperty(Name, "Name");
            ValidateProperty(PhoneNumber, "PhoneNumber");
        }

        // Object[name, phoneNumber]
        public void ActionSave(Object[] values)
        {
            using (var db = new MediasManangementEntities())
            {
                //_userStore.CurrentUser.Name = Name;
                //_userStore.CurrentUser.PhoneNumber = PhoneNumber;

                db.SaveChanges();
                MessageBox.Show("Cập nhật thông tin thành công", "Thành công", MessageBoxButton.OK);
            }
        }
    }
}