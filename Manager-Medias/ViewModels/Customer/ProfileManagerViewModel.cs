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
        private MediasManangementEntities _context = new MediasManangementEntities();

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
            SwitchProfileCmd = new RelayCommand<RoutedEventArgs>(ActionSwitchProfile);
        }

        public void LoadProfile()
        {
            Profiles = _userStore.Profiles;
        }

        public void ActionSwitchProfile(RoutedEventArgs e)
        {
        }
    }
}