using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

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
            ScrollViewer sv = FindVisualChild<ScrollViewer>(sv_itc_movie);

            //var template = itc_movie_action.Template;
            //var sv = (ScrollViewer)template.FindName("sv_itc_movie", itc_movie_action);
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
            ScrollViewer sv = FindVisualChild<ScrollViewer>(sv_itc_albums);
            //var template = itc_albums.Template;
            //var sv = (ScrollViewer)template.FindName("sv_itc_albums", itc_albums);
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

        private new void PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (sender is ListBox && !e.Handled)
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