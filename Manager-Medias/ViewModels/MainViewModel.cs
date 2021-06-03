using Manager_Medias.Services;
using Manager_Medias.Stores;
using Manager_Medias.ViewModels.Customer;
using Manager_Medias.ViewModels.Guest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(NavigationStore navigationStore, LoadingServices ls)
        {
            _loadingServices = ls;
            _navigationStore = navigationStore;

            _navigationStore.ContentViewModel = new GuestHomeViewModel();
            _navigationStore.CurrentViewModel = new GuestMainViewModel();

            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentViewModelChanged;
            _navigationStore.CurrentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
            ls.LoadingChanged += MainViewModel_LoadingChanged;
        }
    }
}