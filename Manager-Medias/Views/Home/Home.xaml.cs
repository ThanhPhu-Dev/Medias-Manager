using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Manager_Medias.Views.Home
{
    public partial class Home : UserControl
    {
        private MainWindow main;

        private class CarouselModel
        {
            public string Image { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }

        public Home()
        {
            InitializeComponent();
        }

        public Home(MainWindow main)
        {
            InitializeComponent();

            this.main = main;

            cbbPhimLe.ItemsSource = new List<string>() { "Phim lẻ 2021", "Phim lẻ 2020", "Phim lẻ 2018" };
            cbbQuocGia.ItemsSource = new List<string>() { "Mỹ", "Hàn Quốc", "Nhật Bản" };
            cbbTheLoai.ItemsSource = new List<string>() { "Hành động", "Viễn tưởng", "18+" };

            ObservableCollection<CarouselModel> carouselModels = new ObservableCollection<CarouselModel>()
            {
                  new CarouselModel(){Image = "/Images/transitton-4.jpg", Title = "MIME", Description =" Sở hữu"},
                  new CarouselModel(){Image = "/Images/transitton-1.jpg", Title = "DOOM AT YOUR SERVICE", Description ="Một ngày nọ kẻ hủy diệt gõ cửa nhà tôi"},
                  new CarouselModel(){Image = "/Images/transitton-3.jpg", Title = "VIVY: FLUORITE EYE'S SONG", Description =" Câu chuyện VyVy"},
            };
            carousel.ItemsSource = carouselModels;
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.Cursor != Cursors.Wait)
                Mouse.OverrideCursor = Cursors.Hand;
        }

        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.Cursor != Cursors.Wait)
                Mouse.OverrideCursor = Cursors.Arrow;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Detail movie");
        }

        private void btnSwitchHomeSuggestions_Click(object sender, RoutedEventArgs e)
        {
            //main.SwitchScreen(new HomeSuggestions(main)); //KHOA: tiếp tục truyền biến main để các user control khác sử dụng
        }

        private void cbbPhimLe_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //main.SwitchScreen(new HomeFilters(main));
        }
    }
}