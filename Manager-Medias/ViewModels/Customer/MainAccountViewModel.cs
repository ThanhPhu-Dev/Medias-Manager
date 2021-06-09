using Manager_Medias.Commands;
using Manager_Medias.Services;
using Manager_Medias.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class MainAccountViewModel : BaseViewModel
    {
        private readonly NavigationStore _acccountNavigateStore;
        public BaseViewModel ContentAccountViewModel => _acccountNavigateStore.ContentViewModel;

        #region Command

        public ICommand NavigateAccountManagerCmd { get; }
        public ICommand NavigateChangePasswordCmd { get; }
        public ICommand NavigateProfileManagerCmd { get; }
        public ICommand NavigateMyListCmd { get; }
        public ICommand NavigateFavoriteManagerCmd { get; }
        public ICommand NavigateHistoryListCmd { get; }
        public ICommand NavigatePaymentHistoryCmd { get; }

        #endregion Command

        private bool _focusFirst = true;

        public bool FocusFirst
        {
            get => _focusFirst;
            set
            {
                _focusFirst = value;
                OnPropertyChanged();
            }
        }

        public MainAccountViewModel()
        {
            FocusFirst = true;
            _acccountNavigateStore = new NavigationStore();
            _acccountNavigateStore.ContentViewModel = new AccountManagerViewModel();

            NavigateAccountManagerCmd = new NavigateCommand<AccountManagerViewModel>(
                new NavigationService<AccountManagerViewModel>(_acccountNavigateStore, () => new AccountManagerViewModel()));

            NavigateChangePasswordCmd = new NavigateCommand<ChangePasswordViewModel>(
                new NavigationService<ChangePasswordViewModel>(_acccountNavigateStore, () => new ChangePasswordViewModel()));

            NavigateProfileManagerCmd = new NavigateCommand<ProfileManagerViewModel>(
                new NavigationService<ProfileManagerViewModel>(_acccountNavigateStore, () => new ProfileManagerViewModel()));

            NavigateMyListCmd = new NavigateCommand<MyPlayListViewModel>(
                new NavigationService<MyPlayListViewModel>(_acccountNavigateStore, () => new MyPlayListViewModel()));

            NavigateFavoriteManagerCmd = new NavigateCommand<FavoriteListViewModel>(
                new NavigationService<FavoriteListViewModel>(_acccountNavigateStore, () => new FavoriteListViewModel()));

            NavigateHistoryListCmd = new NavigateCommand<HistoryListViewModel>(
                new NavigationService<HistoryListViewModel>(_acccountNavigateStore, () => new HistoryListViewModel()));

            NavigatePaymentHistoryCmd = new NavigateCommand<PaymentHistoryViewModel>(
                new NavigationService<PaymentHistoryViewModel>(_acccountNavigateStore, () => new PaymentHistoryViewModel()));

            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
            _acccountNavigateStore.CurrentContentViewModelChanged += _acccountNavigateStore_CurrentContentViewModelChanged;
        }

        private void _acccountNavigateStore_CurrentContentViewModelChanged()
        {
            FocusFirst = false;
            OnPropertyChanged(nameof(ContentAccountViewModel));
        }
    }
}