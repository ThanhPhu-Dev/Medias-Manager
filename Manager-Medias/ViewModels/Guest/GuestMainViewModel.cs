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
        public ICommand NavigateLoginCmd { get; set; }
        public ICommand NavigateRegisterCmd { get; set; }

        public GuestMainViewModel()
        {
            NavigateLoginCmd = new NavigateCommand<GuestLoginViewModel>(
                new NavigationService<GuestLoginViewModel>(_navigationStore, () => new GuestLoginViewModel()));
            NavigateRegisterCmd = new NavigateCommand<GuestInfoRegisterViewModel>(
                new NavigationService<GuestInfoRegisterViewModel>(_navigationStore, () => new GuestInfoRegisterViewModel()));

            _navigationStore.CurrentViewModelChanged += _navigationStore_CurrentViewModelChanged;
            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
        }
    }
}