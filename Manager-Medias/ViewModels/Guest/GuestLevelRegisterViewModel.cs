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
        private User _userCurrent;
        private int _levelCurrent;

        public ICommand CmdLvBasic { get; }
        public ICommand CmdLvMedium { get; }
        public ICommand CmdLvVip { get; }
        public ICommand CmdContinue { get; }

        public int LevelCurrent
        { 
            get => _levelCurrent; 
            set
            {
                _levelCurrent = value;
                OnPropertyChanged();
            }
        }

        public GuestLevelRegisterViewModel(User u)
        {
            _userCurrent = u;
            //kiem tra user có level thì gán level ko thì mặc định = 1
            if (_userCurrent.Level != null)
                LevelCurrent = (int)_userCurrent.Level;
            else LevelCurrent = 2;

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
            LevelCurrent = 3;
        }

        private void LvMedium(object obj)
        {
            LevelCurrent = 2;
        }

        private void LvBasic(object obj)
        {
            LevelCurrent = 1;
        }
    }
}
