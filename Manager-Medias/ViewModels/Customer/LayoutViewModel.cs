using Manager_Medias.Commands;
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
        private readonly NavigationStore _navigationStore;

        public BaseViewModel ContentViewModel => _navigationStore.ContentViewModel;

        public ICommand NavigateProfileCmd { get; }

        public LayoutViewModel(NavigationStore navigationStore)
        {
            //ContentViewModel = new ProfileViewModel();
            _navigationStore = navigationStore;
            NavigateProfileCmd = new NavigateCommand<ProfileViewModel>(navigationStore, () => new ProfileViewModel());
            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
        }

        private void _navigationStore_CurrentContentViewModelChanged()
        {
            OnPropertyChanged(nameof(ContentViewModel));
        }
    }
}