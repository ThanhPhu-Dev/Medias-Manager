using Manager_Medias.ViewModels.Admin;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Manager_Medias.Views.Admin
{
    /// <summary>
    /// Interaction logic for MediaManagementUserControl.xaml
    /// </summary>
    public partial class MediaManagementUserControl : UserControl
    {
        private bool mediaPlayerIsPlaying = false;
        private bool userIsDraggingSlider = false;
        public MediaManagementUserControl()
        {
            InitializeComponent();

            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Tick += timer_Tick;
            //timer.Start();

            var vm = new AdminViewMediaVM();
            DataContext = vm;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            //if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan) && (!userIsDraggingSlider))
            //{
            //    sliProgress.Minimum = 0;
            //    sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
            //    sliProgress.Value = mePlayer.Position.TotalSeconds;
            //}
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViwer = GetScrollViewer(lvMovies) as ScrollViewer;
            detailPanel.Collapse();
        }

        private void Open_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media files (*.mp3;*.mpg;*.mpeg;*.mp4)|*.mp3;*.mpg;*.mpeg;*mp4|All files (*.*)|*.*";
            //if (openFileDialog.ShowDialog() == true)
            //mePlayer.Source = new Uri(openFileDialog.FileName);
        }

        private void Play_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //e.CanExecute = (mePlayer != null) && (mePlayer.Source != null);
        }

        private void Play_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //mePlayer.Play();
            mediaPlayerIsPlaying = true;
        }

        private void Pause_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Pause_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //mePlayer.Pause();
        }

        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = mediaPlayerIsPlaying;
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            //mePlayer.Stop();
            mediaPlayerIsPlaying = false;
        }

        private void sliProgress_DragStarted(object sender, DragStartedEventArgs e)
        {
            userIsDraggingSlider = true;
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            userIsDraggingSlider = false;
            //mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            //mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void boderTemplate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;
            var media = border.FindName("movieItem") as MediaElement;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Thread.Sleep(200);
                Dispatcher.Invoke(() =>
                {
                    if (media?.Source != null)
                    {
                        media.Visibility = Visibility.Visible;
                        media?.Play();
                        detailPanel.Show();
                    }
                });
            });
        }

        private void boderTemplate_MouseLeave(object sender, MouseEventArgs e)
        {
            var border = sender as Border;
            var media = border.FindName("movieItem") as MediaElement;
            media?.Close();
            if (media == null)
                return;
            media.Visibility = Visibility.Hidden;

        }

        public static DependencyObject GetScrollViewer(DependencyObject o)
        {
            // Return the DependencyObject if it is a ScrollViewer
            if (o is ScrollViewer)
            { return o; }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(o); i++)
            {
                var child = VisualTreeHelper.GetChild(o, i);

                var result = GetScrollViewer(child);
                if (result == null)
                {
                    continue;
                }
                else
                {
                    return result;
                }
            }
            return null;
        }

        ScrollViewer scrollViwer;
        private void btMoveRight_Click(object sender, RoutedEventArgs e)
        {

            double move = scrollViwer.HorizontalOffset + 3;
            scrollViwer.ScrollToHorizontalOffset(move);
            detailPanel.Collapse();
        }

        private void btMoveLeft_Click(object sender, RoutedEventArgs e)
        {

            double move = scrollViwer.HorizontalOffset - 3;
            scrollViwer.ScrollToHorizontalOffset(move);
        }
    }
}
