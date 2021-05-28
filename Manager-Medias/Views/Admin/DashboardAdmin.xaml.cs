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


            List<User> users = new List<User>();
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com",Level =3, Password = "John Doe", CreateAt = new DateTime(2020, 7, 23), Balance = 120000, cardNum = "691100003613474", Accumulated = 1247, Code = "FHTY7V" });
            users.Add(new User() { Email = "thanhphuLe@1245yahoo.com", Level = 2, Password = "5363425235", CreateAt = new DateTime(2020, 3, 3), Balance = 130000, cardNum = "691145615274", Accumulated = 1247, Code = "DFG464" });
            users.Add(new User() { Email = "dk66nguyen@1245yahoo.com", Level = 3, Password = "ghg245235235", CreateAt = new DateTime(2020, 2, 27), Balance = 20000, cardNum = "64560003615274", Accumulated = 1, Code = "FGHF34" });
            users.Add(new User() { Email = "duongtrongFuh@1245yahoo.com", Level = 1, Password = "2352668hjnf", CreateAt = new DateTime(2020, 11, 13), Balance = 120000, cardNum = "645600003615274", Accumulated = 144, Code = "FHTY7V" });
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com", Level = 1, Password = "zxczxc697445", CreateAt = new DateTime(2020, 7, 3), Balance = 20000, cardNum = "4600003615274", Accumulated = 52, Code = "34534GHFH" });
            users.Add(new User() { Email = "nghiaDxDz@1245yahoo.com", Level = 1, Password = "45kli1024", CreateAt = new DateTime(2020, 11, 23), Balance = 190000, cardNum = "69456003615274", Accumulated = 13, Code = "FHT5Y7V" });
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com", Level = 2, Password = "xvcv3463hj,", CreateAt = new DateTime(2020, 9, 11), Balance = 180000, cardNum = "69235003615274", Accumulated = 46, Code = "GFG3536" });
            users.Add(new User() { Email = "minhthong_ars@1245yahoo.com", Level = 3, Password = "sdfs34fg3", CreateAt = new DateTime(2020, 2, 22), Balance = 20000, cardNum = "6917503615274", Accumulated = 135, Code = "F353HHTY7V" });
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com", Level = 2, Password = "fdgfdfg", CreateAt = new DateTime(2020, 7, 23), Balance = 129000, cardNum = "6934563245274", Accumulated = 1247, Code = "343HGH" });
            users.Add(new User() { Email = "lethiC@1245yahoo.com", Level = 3, Password = "JohnDoe", CreateAt = new DateTime(2020, 1, 16), Balance = 80000, cardNum = "69113463615274", Accumulated = 345, Code = "46HJMH3" });
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com", Level = 2 ,Password = "JohnDoe", CreateAt = new DateTime(2020, 7, 6), Balance = 90000, cardNum = "691346274", Accumulated = 23, Code = "DFDFEH" });
            users.Add(new User() { Email = "vovanminhra@1245yahoo.com", Level = 1, Password = "45kli1024", CreateAt = new DateTime(2020, 7, 23), Balance = 990000, cardNum = "6911003455274", Accumulated = 1247, Code = "FHTY7V" });
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com", Level = 2, Password = "John Doe", CreateAt = new DateTime(2020, 6, 6), Balance = 120000, cardNum = "6234100003615274", Accumulated = 467, Code = "DGĐFDF" });
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com", Level = 3, Password = "45ksdwtli1024", CreateAt = new DateTime(2020, 7, 23), Balance = 120000, cardNum = "69234003615274", Accumulated = 1247, Code = "BFGE47657" });

            DataContext = this;

            
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
            //mainGrid.Children.Add(new MainDashBoardAdminUserControl());
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

        private void Button_Tab_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            GridCursor.Margin = new Thickness((250 * index + 30), 0, 0, 0);
        }
        public SeriesCollection Data => new SeriesCollection() // Biến chứa dữ liệu biểu đồ
        {
            new PieSeries()
            {
                Values = new ChartValues<float> { 124124} , Title = "Spider man: Far from home"
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { 57342} , Title = "Bố già lắm chiêu"
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { 56233 } , Title ="Gái già"
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { 235235 }, Title = "Còn cái nịt"
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { 54745} , Title = "Còn đúng cái nịt thôi"
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { 235} , Title = "One piece Movie 3"
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { 23425} , Title = "Trò chơi vương quyền 7"
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { 1212} , Title = "Captain Mavel"
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { 9865} , Title = "Trò chơi vương quyền 1"
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { 12314} , Title = "Còn đúng cái nịt thôi"
            },

        };

        public SeriesCollection Data2 => new SeriesCollection() // Biến chứa dữ liệu biểu đồ
        {
             new ColumnSeries()
            {
                Values = new ChartValues<float> { 124124} , Title = "Spider man: Far from home"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 57342} , Title = "Bố già lắm chiêu"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 56233 } , Title ="Gái già"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 235235 }, Title = "Còn cái nịt"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 54745} , Title = "Còn đúng cái nịt thôi"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 235} , Title = "One piece Movie 3"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 23425} , Title = "Trò chơi vương quyền 7"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 1212} , Title = "Captain Mavel"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 9865} , Title = "Trò chơi vương quyền 1"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 12314} , Title = "Còn đúng cái nịt thôi"
            },
        };

        private void PieChart_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            var chart = chartPoint.ChartView as PieChart;
            foreach (PieSeries pie in chart.Series)
            {
                pie.PushOut = 0;
            }

            var neo = chartPoint.SeriesView as PieSeries;
            neo.PushOut = 30;
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