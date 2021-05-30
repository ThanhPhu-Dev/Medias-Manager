using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Manager_Medias.Views.Home
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void moviesButton_Click(object sender, RoutedEventArgs e)
        {
            var template = itc_movie_action.Template;
            var sv = (ScrollViewer)template.FindName("sv_itc_movie", itc_movie_action);
            var curPos = sv.HorizontalOffset;
            var btn = sender as RepeatButton;
            var contentBtn = (MaterialDesignThemes.Wpf.PackIcon)btn.Content;
            string turn = contentBtn.Kind.ToString();
            switch (turn)
            {
                case "KeyboardArrowRight":
                    sv.ScrollToHorizontalOffset(curPos + 40);
                    break;
                case "ChevronLeft":
                    sv.ScrollToHorizontalOffset(curPos - 40);
                    break;
                default:
                    break;
            }
        }

        private void albumsButton_Click(object sender, RoutedEventArgs e)
        {
            var template = itc_albums.Template;
            var sv = (ScrollViewer)template.FindName("sv_itc_albums", itc_albums);
            var curPos = sv.HorizontalOffset;
            var btn = sender as RepeatButton;
            var contentBtn = (MaterialDesignThemes.Wpf.PackIcon)btn.Content;
            string turn = contentBtn.Kind.ToString();
            switch (turn)
            {
                case "KeyboardArrowRight":
                    sv.ScrollToHorizontalOffset(curPos + 40);
                    break;
                case "ChevronLeft":
                    sv.ScrollToHorizontalOffset(curPos - 40);
                    break;
                default:
                    break;
            }
        }
    }
}
