using Manager_Medias.Commands;
using Manager_Medias.Services;
using Manager_Medias.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Manager_Medias.ViewModels.Customer
{
    public class MainAccountView : BaseViewModel
    {
        private readonly UserStore _userStore;

        #region Command

        public ICommand NavigateAccountManagerCmd { get; }
        public ICommand NavigateProfileManagerCmd { get; }
        public ICommand NavigateFavoriteManagerCmd { get; }
        public ICommand NavigateHistoryListCmd { get; }

        #endregion Command

        public MainAccountView(UserStore userStore)
        {
            _navigationStore = new NavigationStore();
            _userStore = userStore;
            _navigationStore.ContentViewModel = new AccountManagerViewModel();

            NavigateAccountManagerCmd = new NavigateCommand<AccountManagerViewModel>(
                new NavigationService<AccountManagerViewModel>(_navigationStore, () => new AccountManagerViewModel(userStore)));

            NavigateProfileManagerCmd = new NavigateCommand<ProfileManagerViewModel>(
                new NavigationService<ProfileManagerViewModel>(_navigationStore, () => new ProfileManagerViewModel(userStore)));

            NavigateFavoriteManagerCmd = new NavigateCommand<FavoriteViewModel>(
                new NavigationService<FavoriteViewModel>(_navigationStore, () => new FavoriteViewModel(userStore)));

            NavigateHistoryListCmd = new NavigateCommand<HistoryViewModel>(
                new NavigationService<HistoryViewModel>(_navigationStore, () => new HistoryViewModel(userStore)));

            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
        }
    }
}