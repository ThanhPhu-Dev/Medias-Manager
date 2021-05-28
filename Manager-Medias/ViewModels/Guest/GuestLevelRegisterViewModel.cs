using Manager_Medias.Commands;
using Manager_Medias.Models;
using Manager_Medias.Stores;
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

        public GuestLevelRegisterViewModel(User u, NavigationStore navigationStore)
        {
            //khởi tạo để chuyển trang 
            _navigationStore = navigationStore;

            //user local
            _userCurrent = u;

            //kiem tra user có level thì gán level ko thì mặc định = 1
            if (_userCurrent.Level != null)
                LevelCurrent = (int)_userCurrent.Level;
            else LevelCurrent = 1;

            //tạo event command
            CmdLvBasic = new RelayCommand<Object>(LvBasic);
            CmdLvMedium = new RelayCommand<Object>(LvMedium);
            CmdLvVip = new RelayCommand<Object>(LvVip);
            CmdContinue = new RelayCommand<Object>(Continue);
        }

        private void Continue(object obj)
        {
            using (var db = new MediasManangementEntities())
            {
                var user = db.Users.Where(u => u.Email == _userCurrent.Email).Single() as User;
                user.Level = LevelCurrent;
                
                if(db.SaveChanges() > 0)
                {
                    _navigationStore.ContentViewModel = new GuestCartRegisterViewModel(user);
                }
            }
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
