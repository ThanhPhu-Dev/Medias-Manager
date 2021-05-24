using Manager_Medias.Stores;
using Manager_Medias.ViewModels.Customer;
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

        //public MainViewModel()
        //{
        //    CurrentViewModel = new LayoutViewModel();
        //}

        public MainViewModel(NavigationStore navigationStore)
        {
            CurrentViewModel = new LayoutViewModel(navigationStore);
        }
    }
}