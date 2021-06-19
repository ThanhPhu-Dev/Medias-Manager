using Manager_Medias.Models;
using Manager_Medias.Services;
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
            // Init location
            NavigationStore navigationStore = new NavigationStore();
            LoadingServices ls = new LoadingServices();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore, ls)
            };

            MainWindow.Show();

            base.OnStartup(e);
        }
    }
}