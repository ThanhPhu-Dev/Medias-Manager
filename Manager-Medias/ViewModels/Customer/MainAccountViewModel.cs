using Manager_Medias.Commands;
using Manager_Medias.Services;
using Manager_Medias.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class MainAccountViewModel : BaseViewModel
    {
        private readonly NavigationStore _acccountNavigateStore;
        private readonly UserStore _userStore;
        public BaseViewModel ContentAccountViewModel => _acccountNavigateStore.ContentViewModel;

        #region Command

        public ICommand NavigateAccountManagerCmd { get; }
        public ICommand NavigateChangePasswordCmd { get; }
        public ICommand NavigateProfileManagerCmd { get; }
        public ICommand NavigateFavoriteManagerCmd { get; }
        public ICommand NavigateHistoryListCmd { get; }
        public ICommand NavigatePaymentHistoryCmd { get; }

        #endregion Command

        public MainAccountViewModel(UserStore userStore, NavigationStore navigationStore)
        {
            _acccountNavigateStore = new NavigationStore();
            _navigationStore = navigationStore;
            _userStore = userStore;

            // ViewModel(Account, ParentNavigation use to Cancel command)
            _acccountNavigateStore.ContentViewModel = new AccountManagerViewModel(_userStore, _navigationStore);

            //_navigationStore.ContentViewModel = new AccountManagerViewModel(userStore, _navigationStore);

            NavigateAccountManagerCmd = new NavigateCommand<AccountManagerViewModel>(
                new NavigationService<AccountManagerViewModel>(_acccountNavigateStore, () => new AccountManagerViewModel(userStore, _navigationStore)));

            NavigateChangePasswordCmd = new NavigateCommand<ChangePasswordViewModel>(
                new NavigationService<ChangePasswordViewModel>(_acccountNavigateStore, () => new ChangePasswordViewModel(userStore, _navigationStore)));

            NavigateProfileManagerCmd = new NavigateCommand<ProfileManagerViewModel>(
                new NavigationService<ProfileManagerViewModel>(_acccountNavigateStore, () => new ProfileManagerViewModel(userStore)));

            NavigateFavoriteManagerCmd = new NavigateCommand<FavoriteListViewModel>(
                new NavigationService<FavoriteListViewModel>(_acccountNavigateStore, () => new FavoriteListViewModel(userStore, _navigationStore)));

            NavigateHistoryListCmd = new NavigateCommand<HistoryViewModel>(
                new NavigationService<HistoryViewModel>(_acccountNavigateStore, () => new HistoryViewModel(userStore)));

            NavigatePaymentHistoryCmd = new NavigateCommand<PaymentHistoryViewModel>(
                new NavigationService<PaymentHistoryViewModel>(_acccountNavigateStore, () => new PaymentHistoryViewModel(userStore)));

            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
            _acccountNavigateStore.CurrentContentViewModelChanged += _acccountNavigateStore_CurrentContentViewModelChanged;
        }

        private void _acccountNavigateStore_CurrentContentViewModelChanged()
        {
            OnPropertyChanged(nameof(ContentAccountViewModel));
        }
    }
}