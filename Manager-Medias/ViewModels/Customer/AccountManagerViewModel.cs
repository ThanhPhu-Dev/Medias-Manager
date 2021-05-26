using Manager_Medias.Commands;
using Manager_Medias.Stores;
using Manager_Medias.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class AccountManagerViewModel : BaseViewModel
    {
        public ICommand SaveCmd { get; }
        public ICommand NavigateHomeCmd { get; }

        public AccountManagerViewModel(UserStore userStore, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            SaveCmd = new RelayCommand<Object[]>(ActionSave);

            NavigateHomeCmd = new NavigateCommand<HomeViewModel>(new NavigationService<HomeViewModel>(_navigationStore, () => new HomeViewModel()));
        }

        public void ActionSave(Object[] values)
        {
        }
    }
}