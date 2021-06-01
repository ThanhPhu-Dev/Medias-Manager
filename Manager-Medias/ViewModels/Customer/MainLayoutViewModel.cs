using Manager_Medias.Commands;
using Manager_Medias.Models;
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
            get => _userStore.ProfileName;
        }

        #endregion Binding

        public MainLayoutViewModel()
        {
            NavigateAccountCmd = new NavigateCommand<MainAccountViewModel>(
                new NavigationService<MainAccountViewModel>(_navigationStore, () => new MainAccountViewModel()));

            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
            MovieCmd = new RelayCommand<object>(MoviewShow);

            _userStore.ProfileChanged += _userStore_ProfileChanged;
            _userStore.AvatarChanged += _userStore_AvatarChanged;
            _userStore.NameChanged += _userStore_NameChanged;
        }

        private void _userStore_ProfileChanged()
        {
            OnPropertyChanged(nameof(ProfileName));
            OnPropertyChanged(nameof(Avatar));
        }

        private void _userStore_NameChanged()
        {
            OnPropertyChanged(nameof(ProfileName));
        }

        private void _userStore_AvatarChanged()
        {
            OnPropertyChanged(nameof(Avatar));
        }

        private void MoviewShow(object obj)
        {
            _navigationStore.ContentViewModel = new HomeMovieViewModel();
        }
    }
}