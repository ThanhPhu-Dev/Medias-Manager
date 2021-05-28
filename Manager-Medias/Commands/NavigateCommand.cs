using Manager_Medias.Services;
using Manager_Medias.Stores;
using Manager_Medias.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : BaseViewModel
    {
        private readonly NavigationService<TViewModel> _navigationService;

        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}