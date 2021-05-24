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
        public BaseViewModel CurrentViewModel { get; }
        private NavigationStore navigationStore = new NavigationStore();

        public MainViewModel()
        {
            navigationStore.ContentViewModel = new GuestHomeViewModel();
            CurrentViewModel = new GuestMainViewModel(navigationStore);

            // Init content after login
            //navigationStore.ContentViewModel = new HomeViewModel();
            //CurrentViewModel = new HomeViewModel();
        }
    }
}