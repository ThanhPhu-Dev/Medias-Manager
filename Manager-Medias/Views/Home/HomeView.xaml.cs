using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

        public static childItem FindVisualChild<childItem>(DependencyObject obj)
                where childItem : DependencyObject
        {
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

        private void gDatatemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;
            var media = grid.FindName("VideoPreview") as MediaElement;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Thread.Sleep(500);
                Dispatcher.Invoke(() =>
                {
                    if (media?.Source != null)
                    {
                        media?.Play();
                    }
                });
            });
        }

        private void gDatatemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;
            var media = grid.FindName("VideoPreview") as MediaElement;
            media?.Close();
        }

        private void albumsButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}