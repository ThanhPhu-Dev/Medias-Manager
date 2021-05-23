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
    /// <summary>
    /// Interaction logic for HomeSuggestions.xaml
    /// </summary>
    public partial class HomeSuggestions : UserControl
    {
        MainWindow main;

        class CarouselModel
        {
            public string Image { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public double View { get; set; }
            public int Rate { get; set; }
        }

        public HomeSuggestions(MainWindow main)
        {
            InitializeComponent();
            this.main = main;

            ObservableCollection<CarouselModel> models = new ObservableCollection<CarouselModel>()
            {
                  new CarouselModel(){Image = "/Images/phim-hanh-dong-1.jpg", Title = "Không hối tiếc", Description="Without Remorse(2021)", View =46.5, Rate= 4},
                  new CarouselModel(){Image = "/Images/phim-hanh-dong-2.jpg", Title = "Cuộc giải cứu sinh tử",Description = "Redemption Day (2021)", View =88.8, Rate= 4},
                  new CarouselModel(){Image = "/Images/phim-hanh-dong-3.jpg", Title = "Anh hùng trở về",Description="Heroes Return", View =46.5, Rate= 4},
                  new CarouselModel(){Image = "/Images/phim-hanh-dong-4.jpg", Title = "Nhân tố tiết lộ",Description="Agent Revelation", View =46.5, Rate= 4},
                  new CarouselModel(){Image = "/Images/phim-hanh-dong-5.jpg", Title = "Siêu trộm Lupin",Description="Lupin(2021)", View =46.5, Rate= 4},
                  new CarouselModel(){Image = "/Images/phim-hanh-dong-6.jpg", Title = "Thợ săn quái vật",Description="Monster Hunter(2020)", View =46.5, Rate= 4},
                  new CarouselModel(){Image = "/Images/phim-hanh-dong-7.jpg", Title = "Lưỡng bất nghi",Description="Liuang Bu Yi", View =46.5, Rate= 4},
                  new CarouselModel(){Image = "/Images/phim-hanh-dong-8.jpg", Title = "Doraemon", Description="Đôi bạn thân", View =46.5, Rate= 4},
                  new CarouselModel(){Image = "/Images/phim-hanh-dong-9.jpg", Title = "Tân phong thần",Description="New Gods", View =46.5, Rate= 4},
                  new CarouselModel(){Image = "/Images/phim-hanh-dong-10.jpg", Title = "Tay đám tối thượng",Description="Normal man", View =46.5, Rate= 4},
            };
            crsActionMovie.ItemsSource = models;
            crsLoveMovie.ItemsSource = models;
            crs18Movie.ItemsSource = models;
            lvHotMovie.ItemsSource = models;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("Detail movie");
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

        private void btnSwitchHome_Click(object sender, RoutedEventArgs e)
        {
            main.SwitchScreen(new Home(main));
        }

        private void btnShowMore_Click(object sender, RoutedEventArgs e)
        {
            main.SwitchScreen(new HomeFilters(main));
        }
    }
}
