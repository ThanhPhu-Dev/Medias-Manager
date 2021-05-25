using Manager_Medias.Commands;
using Manager_Medias.Services;
using Manager_Medias.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Guest
{
    public class GuestMainViewModel : BaseViewModel
    {
        private readonly NavigationStore _navigationStore;

        public BaseViewModel ContentViewModel => _navigationStore.ContentViewModel;

        public ICommand NavigateLoginCmd { get; }
        public ICommand NavigateRegisterCmd { get; }

        public GuestMainViewModel(NavigationStore navigationStore)
        {
            //ContentViewModel = new ProfileViewModel();
            _navigationStore = navigationStore;
            NavigateLoginCmd = new NavigateCommand<GuestLoginViewModel>(
                new NavigationService<GuestLoginViewModel>(navigationStore, () => new GuestLoginViewModel()));
            NavigateRegisterCmd = new NavigateCommand<GuestRegisterViewModel>(
                new NavigationService<GuestRegisterViewModel>(navigationStore, () => new GuestRegisterViewModel()));

            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
        }

        private void _navigationStore_CurrentContentViewModelChanged()
        {
            OnPropertyChanged(nameof(ContentViewModel));
        }
    }
}