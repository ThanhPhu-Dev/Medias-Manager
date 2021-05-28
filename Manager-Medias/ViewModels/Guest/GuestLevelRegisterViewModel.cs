using Manager_Medias.Commands;
using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Guest
{
    public class GuestLevelRegisterViewModel : BaseViewModel
    {
        private User userCurrent;
        private int Level;

        public ICommand CmdLvBasic { get; }
        public ICommand CmdLvMedium { get; }
        public ICommand CmdLvVip { get; }
        public ICommand CmdContinue { get; }

        public GuestLevelRegisterViewModel(User u)
        {
            this.userCurrent = u;

            CmdLvBasic = new RelayCommand<Object>(LvBasic);
            CmdLvMedium = new RelayCommand<Object>(LvMedium);
            CmdLvVip = new RelayCommand<Object>(LvVip);
            CmdContinue = new RelayCommand<Object>(Continue);
        }

        private void Continue(object obj)
        {
            
        }

        private void LvVip(object obj)
        {
            Level = 3;
        }

        private void LvMedium(object obj)
        {
            Level = 2;
        }

        private void LvBasic(object obj)
        {
            Level = 1;
        }
    }
}
