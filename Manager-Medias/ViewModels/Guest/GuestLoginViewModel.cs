using Manager_Medias.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Guest
{
    public class GuestLoginViewModel : BaseViewModel
    {
        public ICommand LoginCmd { get; set; }

        public GuestLoginViewModel()
        {
            LoginCmd = new RelayCommand<object[]>(ActionLogin);
        }

        public void ActionLogin(object[] values)
        {
        }
    }
}