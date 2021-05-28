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
            using (var db = new MediasManangementEntities())
            {
                var user = db.Users.Where(u => u.Email == "nghiadx2001@gmail.c").FirstOrDefault();

                if (string.IsNullOrEmpty(user.ToString()))
                {
                    MessageBox.Show("Lỗi không tìm thấy user");
                    return;
                }

                UserStore userStore = new UserStore(user);

                // Check valid account
                // Redirect to MainLayout
                _navigationStore.CurrentViewModel = new MainLayoutViewModel(userStore, _navigationStore);
                _navigationStore.ContentViewModel = new HomeViewModel();
            }

            //_navigationStore.ContentViewModel = new DetailAudioViewModel(1, 1);
        }
    }
}