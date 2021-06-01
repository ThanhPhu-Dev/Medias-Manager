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

        public ICommand ClickAudio { get; set; }



        //public Profile currentProfile => _userStore.Profiles

        #region Command

        public ICommand ComboboxAccountCmd { get; set; }

        public ICommand NavigateMovieCmd { get; }
        public ICommand NavigateAlbumCmd { get; }
        public ICommand NavigateAudioCmd { get; }
        public ICommand NavigateAccountCmd { get; }

        #endregion Command

        public MainLayoutViewModel(UserStore userStore, NavigationStore navigationStore)
        {
            _userStore = userStore;
            _navigationStore = navigationStore;
            ClickAudio = new RelayCommand<object>(Clickaudio);
            ComboboxAccountCmd = new RelayCommand<SelectionChangedEventArgs>(ComboboxAccountChanged);

            NavigateAccountCmd = new NavigateCommand<MainAccountViewModel>(
                new NavigationService<MainAccountViewModel>(_navigationStore, () => new MainAccountViewModel(userStore, _navigationStore)));

            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
        }

        private void Clickaudio(object obj)
        {
            _navigationStore.ContentViewModel = new HomeAudioViewModel(_userStore);
        }

        private void ComboboxAccountChanged(SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }
    }
}