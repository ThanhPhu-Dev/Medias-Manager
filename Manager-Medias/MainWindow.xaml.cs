using Manager_Medias.Views.Home;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Manager_Medias
{
    public partial class MainWindow : Window
    {      
        //KHOA: hiển thị user control tương ứng từ control grid 
        internal void SwitchScreen(object sender)
        {
            var screen = ((UserControl)sender);
            if (screen != null)
            {
                grid.Children.Clear();
                grid.Children.Add(screen);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            grid.Children.Add(new Home(this)); //KHOA: truyền this để gọi hàm SwitchScreen từ các user control
        }
    }
}
