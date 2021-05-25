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
        public ICommand NavigateLoginCmd { get; }
        public ICommand NavigateRegisterCmd { get; }

        public GuestMainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            NavigateLoginCmd = new NavigateCommand<GuestLoginViewModel>(
                new NavigationService<GuestLoginViewModel>(navigationStore, () => new GuestLoginViewModel(navigationStore)));
            NavigateRegisterCmd = new NavigateCommand<GuestRegisterViewModel>(
                new NavigationService<GuestRegisterViewModel>(navigationStore, () => new GuestRegisterViewModel()));

            _navigationStore.CurrentViewModelChanged += _navigationStore_CurrentViewModelChanged;
            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
        }
    }
}