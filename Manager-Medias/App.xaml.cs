using Manager_Medias.Stores;
using Manager_Medias.ViewModels;
using Manager_Medias.ViewModels.Customer;
using Manager_Medias.ViewModels.Guest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Manager_Medias
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDUwNjgwQDMxMzkyZTMxMmUzMEFCRUZSUzU4RGNhTGo0eHkvMW9YYVJkMUd5UXhxbTdaNTdtSFFJSS84dlU9");
            // Init location
            NavigationStore navigationStore = new NavigationStore();
            navigationStore.ContentViewModel = new GuestHomeViewModel();
            navigationStore.CurrentViewModel = new GuestMainViewModel(navigationStore);

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}