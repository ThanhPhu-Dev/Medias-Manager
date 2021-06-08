using Manager_Medias.Commands;
using Manager_Medias.Models;
using Manager_Medias.Services;
using Manager_Medias.Stores;
using Manager_Medias.ViewModels.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class MainLayoutViewModel : BaseViewModel
    {
        #region Command

        public ICommand NavigateMovieCmd { get; set; }
        public ICommand NavigateAlbumCmd { get; set; }
        public ICommand NavigateAudioCmd { get; set; }
        public ICommand NavigateAccountCmd { get; set; }
        public ICommand NavigateHomeCmd { get; set; }
        public ICommand LogoutCmd { get; set; }
        public ICommand MovieCmd { get; set; }
        public ICommand AudioCmd { get; set; }
        public ICommand PictureCmd { get; set; }
        public ICommand PaymentCmd { get; set; }
        public ICommand MovieByCatCmd { get; set; }

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

        public string LevelName
        {
            get => _userStore.LevelName;
        }

        private ListCollectionView _movieCategories;

        public ListCollectionView MovieCategories
        {
            get => _movieCategories;
            set
            {
                _movieCategories = value;
                OnPropertyChanged();
            }
        }

        #endregion Binding

        public MainLayoutViewModel()
        {
            LoadMovieCategories();
            NavigateAccountCmd = new NavigateCommand<MainAccountViewModel>(
                new NavigationService<MainAccountViewModel>(_navigationStore, () => new MainAccountViewModel()));

            NavigateHomeCmd = new NavigateCommand<HomeViewModel>(
                new NavigationService<HomeViewModel>(_navigationStore, () => new HomeViewModel()));

            PaymentCmd = new NavigateCommand<PaymentViewModels>(
                new NavigationService<PaymentViewModels>(_navigationStore, () => new PaymentViewModels()));

            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;

            LogoutCmd = new RelayCommand<object>((object o) =>
            {
                _userStore = null;
                _navigationStore.CurrentViewModel = new GuestMainViewModel();
                _navigationStore.ContentViewModel = new GuestHomeViewModel();
            });
            MovieCmd = new RelayCommand<object>(MoviewShow);
            AudioCmd = new RelayCommand<object>(AudioShow);
            PictureCmd = new RelayCommand<object>(PictureShow);
            MovieByCatCmd = new RelayCommand<object>(NavigateMovieWithCat);

            _userStore.ProfileChanged += _userStore_ProfileChanged;
            _userStore.AvatarChanged += _userStore_AvatarChanged;
            _userStore.NameChanged += _userStore_NameChanged;
            _userStore.LevelChanged += _userStore_LevelChanged;
        }

        private void NavigateMovieWithCat(object obj)
        {
            if (obj != null)
            {
                int CatId = (int)obj;

                _navigationStore.ContentViewModel = new HomeMovieViewModel(CatId);
            }
        }

        private void LoadMovieCategories()
        {
            using (var db = new MediasManangementEntities())
            {
                var cats = db.Movie_Categories.ToList();
                MovieCategories = new ListCollectionView(cats);
            }
        }

        private void _userStore_LevelChanged()
        {
            OnPropertyChanged(nameof(LevelName));
        }

        private void PictureShow(object obj)
        {
            _navigationStore.ContentViewModel = new HomePictureViewModel();
        }

        private void AudioShow(object obj)
        {
            _navigationStore.ContentViewModel = new HomeAudioViewModel();
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