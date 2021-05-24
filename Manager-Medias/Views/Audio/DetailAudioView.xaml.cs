using Manager_Medias.ViewModels.Customer;
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

namespace Manager_Medias.Views.Audio
{
    /// <summary>
    /// Interaction logic for DetailAudioView.xaml
    /// </summary>
    public partial class DetailAudioView : UserControl
    {
        public DetailAudioView()
        {
            InitializeComponent();

            DataContext = new DetailAudioViewModel();
            //List<test> items = new List<test>();
            //items.Add(new test() { stt = "1", name = "Sắp 30", time = "4:05" });
            //items.Add(new test() { stt = "2", name = "Giờ này mà sao còn", time = "4:05" });
            //items.Add(new test() { stt = "3", name = "Trốn tìm", time = "4:05" });
            //items.Add(new test() { stt = "1", name = "Sắp 30", time = "4:05" });
            //items.Add(new test() { stt = "2", name = "Giờ này mà sao còn", time = "4:05" });
            //items.Add(new test() { stt = "3", name = "Trốn tìm", time = "4:05" });
            //items.Add(new test() { stt = "1", name = "Sắp 30", time = "4:05" });
            //items.Add(new test() { stt = "2", name = "Giờ này mà sao còn", time = "4:05" });
            //items.Add(new test() { stt = "3", name = "Trốn tìm", time = "4:05" });
            //items.Add(new test() { stt = "1", name = "Sắp 30", time = "4:05" });
            //items.Add(new test() { stt = "2", name = "Giờ này mà sao còn", time = "4:05" });
            //items.Add(new test() { stt = "3", name = "Trốn tìm", time = "4:05" });
            //DataContext = items;
        }
    }
    public class test
    {
        public string stt { get; set; }
        public string name { get; set; }
        public string time { get; set; }
    }
}
