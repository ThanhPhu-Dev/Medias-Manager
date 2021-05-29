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

namespace Manager_Medias.Views.Movie
{
    /// <summary>
    /// Interaction logic for HomeMovieView.xaml
    /// </summary>
    public partial class HomeMovieView : UserControl
    {
        public HomeMovieView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var template = itc_movie.Template;
            var sv = (ScrollViewer)template.FindName("sv_itc", itc_movie);
            var curPos = sv.HorizontalOffset;
            var btn = sender as RepeatButton;
            switch((string)btn.Content)
            {
                case "Right":
                    sv.ScrollToHorizontalOffset(curPos + 40);
                    break;
                case "Left":
                    sv.ScrollToHorizontalOffset(curPos - 40);
                    break;
                default:
                    break;
            }

        }
    }
}
