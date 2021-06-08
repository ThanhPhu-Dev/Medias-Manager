using Manager_Medias.Commands;
using Manager_Medias.Validates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Guest
{
    public class GuestSetNewPasswordViewModel:BaseViewModel
    {
        ICommand CmdConfirm;
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
            CmdConfirm = new RelayCommand<Object[]>(Confirm, (Object[] obj) => !HasErrors);
            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();

            this.ValidationRules.Add(nameof(this.Password), new List<ValidationRule>() { new ValidatePassword() });
        }

        private void Confirm(object[] obj)
        {
            
        }
    }
}
