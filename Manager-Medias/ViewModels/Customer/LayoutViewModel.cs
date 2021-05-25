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
    public class LayoutViewModel : BaseViewModel
    {
        public ICommand NavigateProfileCmd { get; }

        public LayoutViewModel(NavigationStore navigationStore)
        {
            //ContentViewModel = new ProfileViewModel();
            _navigationStore = navigationStore;
            //NavigateProfileCmd = new NavigateCommand<ProfileViewModel>(
            //    new NavigationService<ProfileViewModel>(navigationStore, () => new ProfileViewModel()));
            //_navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
            NavigateProfileCmd = new NavigateCommand<DetailAudioViewModel>(
                new NavigationService<DetailAudioViewModel>(navigationStore, () => new DetailAudioViewModel()));
            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
        }
    }
}