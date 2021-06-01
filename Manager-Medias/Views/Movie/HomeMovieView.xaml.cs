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
            //ScrollViewer sv = FindVisualChild<ScrollViewer>(itc_movie);
            //var template = itc_movie.Template;
            //var sv = (ScrollViewer)template.FindName("sv_itc", itc_movie);
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
        public static childItem FindVisualChild<childItem>(DependencyObject obj)
                where childItem : DependencyObject
        {
            // Iterate through all immediate children
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);

                    if (childOfChild != null)
                        return childOfChild;
                }
            }

            return null;
        }
        private void ItemsControl_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is ItemsControl && !e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
    }
}
