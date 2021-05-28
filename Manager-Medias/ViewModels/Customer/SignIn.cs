using Manager_Medias.Commands;
using Manager_Medias.Models;
using Manager_Medias.Stores;
using Manager_Medias.Validates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class SignIn : BaseViewModel
    {

        public ICommand SaveCmd { get; }

        private string _name;
        private string _password;
        #region BindingProperty
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
        public SignIn(NavigationStore navigationStore)
        {
            SaveCmd = new RelayCommand<Object[]>(ActionSave, (Object[] obj) => !HasErrors);
            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();
            
            this.ValidationRules.Add(nameof(this.Name), new List<ValidationRule>() { new ValidateEmail() });
            this.ValidationRules.Add(nameof(this.Password), new List<ValidationRule>() { new ValidatePassword() });
        }

        private void ActionSave(object[] obj)
        {
           
        }
    }
}
