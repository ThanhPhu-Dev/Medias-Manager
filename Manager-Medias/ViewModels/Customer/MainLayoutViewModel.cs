using Manager_Medias.Commands;
using Manager_Medias.Services;
using Manager_Medias.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class MainLayoutViewModel : BaseViewModel
    {
        private readonly UserStore _userStore;

        #region Command

        public ICommand NavigateMovieCmd { get; }
        public ICommand NavigateAlbumCmd { get; }
        public ICommand NavigateAudioCmd { get; }
        public ICommand NavigateAccountCmd { get; }
        public ICommand MovieCmd { get; set; }

        #endregion Command

        #region Binding

        public string Avatar
        {
            get => _userStore.PathAvatar;
        }

        public string ProfileName
        {
            get => _userStore.CurrentProfile.Name;
        }

        #endregion Binding

        public MainLayoutViewModel(UserStore userStore, NavigationStore navigationStore)
        {
            _userStore = userStore;
            _navigationStore = navigationStore;

            NavigateAccountCmd = new NavigateCommand<MainAccountViewModel>(
                new NavigationService<MainAccountViewModel>(_navigationStore, () => new MainAccountViewModel(userStore, _navigationStore)));

            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
            MovieCmd = new RelayCommand<object>(MoviewShow);
        }

        private void MoviewShow(object obj)
        {
            _navigationStore.ContentViewModel = new HomeMovieViewModel(_navigationStore, _userStore);
        }
    }
}