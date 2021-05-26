using Manager_Medias.Commands;
using Manager_Medias.Models;
using Manager_Medias.Services;
using Manager_Medias.Stores;
using Manager_Medias.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Guest
{
    public class GuestLoginViewModel : BaseViewModel
    {
        public ICommand LoginCmd { get; set; }

        public GuestLoginViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            //LoginCmd = new NavigateCommand<ProfileViewModel>(
            //    new NavigationService<ProfileViewModel>(navigationStore, () => new ProfileViewModel()));

            LoginCmd = new RelayCommand<object[]>(ActionLogin);
        }

        public void ActionLogin(object[] values)
        {
            // Check null data
            if (string.IsNullOrEmpty(values.ToString()) || string.IsNullOrEmpty(values[0].ToString()) ||
                string.IsNullOrEmpty(values[1].ToString()))
            {
                MessageBox.Show("Vui lòng điền đầy đủ dữ liệu!");
                return;
            }

            var email = values[0].ToString();
            var password = values[1].ToString();
            // User has found
            User currentUser = new User
            {
                Code = "usercode",
                Email = "demo@gmail.com",
                Level = 1,
                NumberCard = "demo_card_number",
                Password = "123",
            };

            UserStore userStore = new UserStore(currentUser);

            // Check valid account
            // Redirect to MainLayout
            _navigationStore.CurrentViewModel = new MainLayoutViewModel(userStore, _navigationStore);
            //navigationStore.ContentViewModel = new DetailAudioViewModel();
            _navigationStore.ContentViewModel = new DetailAudioViewModel();
        }
    }
}