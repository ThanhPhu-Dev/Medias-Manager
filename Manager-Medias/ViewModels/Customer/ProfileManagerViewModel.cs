using Manager_Medias.Commands;
using Manager_Medias.Models;
using Manager_Medias.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class ProfileManagerViewModel : BaseViewModel
    {
        private readonly UserStore _userStore;

        #region Command

        public ICommand SwitchProfileCmd { get; set; }

        #endregion Command

        #region Binding

        private ObservableCollection<Profile> _profiles;

        public ObservableCollection<Profile> Profiles
        {
            get => _profiles;
            set
            {
                _profiles = value;
                OnPropertyChanged();
            }
        }

        #endregion Binding

        public ProfileManagerViewModel(UserStore userStore)
        {
            _userStore = userStore;
            LoadCommand();
            LoadProfile();
        }

        public void LoadCommand()
        {
            SwitchProfileCmd = new RelayCommand<Object>(ActionSwitchProfile);
        }

        public void LoadProfile()
        {
            Profiles = _userStore.Profiles;
        }

        public void ActionSwitchProfile(Object profileId)
        {
            using (var db = new MediasManangementEntities())
            {
                var user = db.Users.Where(u => u.Email == _userStore.Email).Single();
                var currentActiveProfile = user.Profiles.Where(p => p.Status == 1).Single();

                var selectedProfile = user.Profiles.Where(p => p.Id == int.Parse(profileId.ToString())).Single();
                currentActiveProfile.Status = 0;
                selectedProfile.Status = 1;

                db.SaveChanges();
            }

            Profiles = _userStore.Profiles;
        }
    }
}