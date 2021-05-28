using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Manager_Medias.Views.Admin
{
    /// <summary>
    /// Interaction logic for DashboardAdmin.xaml
    /// </summary>
    public partial class DashboardAdmin : Window
    {
        public DashboardAdmin()
        {
            InitializeComponent();
            
        }


        private void ButtonCloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void GridTop_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Add(new MainDashBoardAdminUserControl());
        }

        private void addUserBt_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
                
        }

        private void dashBordBt_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(new MainDashBoardAdminUserControl());
        }

        private void accountManagerBt_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(new AccountManagerUserControl());
        }

        private void statisticalBt_Click(object sender, RoutedEventArgs e)
        {
            mainGrid.Children.Clear();
            mainGrid.Children.Add(new StatisticalUserControl());
        }
    }
    public class User
    {
        public string Email { get; set; }
        public int Level { get; set; }

        public string Password { get; set; }

        public DateTime CreateAt { get; set; }

        public double Balance { get; set; }
        public string cardNum { get; set; }
        public int Accumulated { get; set; }
        public string Code { get; set; }
    }
}