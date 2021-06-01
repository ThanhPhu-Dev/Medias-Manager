using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for HomeAudioView.xaml
    /// </summary>
    public partial class HomeAudioView : UserControl
    {
        public HomeAudioView()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            //var template = itc_movie1.Template;
            //var sv = (ScrollViewer)template.FindName("sv_itc", itc_movie1);
            //var curPos = sv.HorizontalOffset;
            //var btn = sender as RepeatButton;
            //var contentBtn = (MaterialDesignThemes.Wpf.PackIcon)btn.Content;
            //string turn = contentBtn.Kind.ToString();
            //switch (turn)
            //{
            //    case "KeyboardArrowRight":
            //        sv.ScrollToHorizontalOffset(curPos + 80);
            //        break;
            //    case "ChevronLeft":
            //        sv.ScrollToHorizontalOffset(curPos - 80);
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
