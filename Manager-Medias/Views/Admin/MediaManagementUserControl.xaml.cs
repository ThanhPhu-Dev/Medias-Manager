using Manager_Medias.ViewModels.Admin;
using Microsoft.Win32;
using System;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;

namespace Manager_Medias.Views.Admin
{
    /// <summary>
    /// Interaction logic for MediaManagementUserControl.xaml
    /// </summary>
    public partial class MediaManagementUserControl : UserControl
    {

        public MediaManagementUserControl()
        {
            InitializeComponent();
            var vm = new AdminViewMediaVM();
            DataContext = vm;


            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }


        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mePlayer.Source != null) && (mePlayer.NaturalDuration.HasTimeSpan))
            {
                sliProgress.Minimum = 0;
                sliProgress.Maximum = mePlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliProgress.Value = mePlayer.Position.TotalSeconds;
            }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            scrollViwer = GetScrollViewer(lvMovies) as ScrollViewer;
            scrollMain = GetScrollViewer(mainScrollViewer) as ScrollViewer;          

            detailPanel.Collapse();
        }

      
        private void btOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Video files (*.mp4)|*mp4";
            if (openFileDialog.ShowDialog() == true)
            {
                txtbVideoAdd.Text = openFileDialog.FileName.ToString();
            }    
                
        }

        private void btPlayMovie_Click(object sender, RoutedEventArgs e)
        {
            mePlayer.Play();

        }
     
        private void btPauseMovie_Click(object sender, RoutedEventArgs e)
        {
            mePlayer.Pause();
        }

        private void sliProgress_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            mePlayer.Position = TimeSpan.FromSeconds(sliProgress.Value);
        }

        private void sliProgress_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliProgress.Value).ToString(@"hh\:mm\:ss");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mePlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }

        private void boderTemplate_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //var border = sender as Border;
            //var media = border.FindName("movieItem") as MediaElement;
            //ThreadPool.QueueUserWorkItem(_ =>
            //{
            //    Thread.Sleep(200);
            //    Dispatcher.Invoke(() =>
            //    {
            //        if (media?.Source != null)
            //        {
            //            media.Visibility = Visibility.Visible;
            //            media?.Play();
                        
            //        }
            //    });
            //});
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
            scrollViwer = GetScrollViewer(lvMovies) as ScrollViewer;
            double move = scrollViwer.HorizontalOffset + 3;
            scrollViwer.ScrollToHorizontalOffset(move);
            detailPanel.Collapse();
        }

        private void btMoveLeft_Click(object sender, RoutedEventArgs e)
        {

            double move = scrollViwer.HorizontalOffset - 3;
            scrollViwer.ScrollToHorizontalOffset(move);
        }

        ScrollViewer scrollMain;
        private void btOpenMovieDetail_Click(object sender, RoutedEventArgs e)
        {
            detailPanel.Show();
            double move = 1408.0;
            mainScrollViewer.ScrollToVerticalOffset(move);
            detailPanel.Focus();
            PCheckBox.IsEnabled = true;
        }
        

        bool _FullScreen = false;
        private void btFullScreen_Click(object sender, RoutedEventArgs e)
        {
            if(!_FullScreen)
            {
                mePlayer.Height = detailPanel.Height - 100;
                mePlayer.Width = 860;
            }
            else
            {
                mePlayer.Height = 220;
                mePlayer.Width = 380;
            }

            _FullScreen = !_FullScreen;
        }

        private void btChooseImgAdd_Click(object sender, RoutedEventArgs e)
        {
            //var a = imgPoster.Source.ToString();

            var screen = new OpenFileDialog();
            screen.Filter = "Image files (*.png,*jpg)|*png;*jpg";
            if (screen.ShowDialog() == true)
            {
                var filename = screen.FileName;
                txtbPosterAdd.Text = filename.ToString();
            }
        }

        private void boderTemplate_MouseEnter(object sender, MouseEventArgs e)
        {
            var border = sender as Border;
            var media = border.FindName("movieItem") as MediaElement;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                //Thread.Sleep(200);
                Dispatcher.Invoke(() =>
                {
                    if (media?.Source != null)
                    {
                        media.Visibility = Visibility.Visible;
                        media?.Play();
                    }
                });
            });
        }

        private void btExitPopup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PCheckBox.IsChecked = false;
        }

        private void btAddMovie_Click(object sender, RoutedEventArgs e)
        {
            detailPanel.Show();
            double move = 1408.0;
            mainScrollViewer.ScrollToVerticalOffset(move);
            detailPanel.Focus();

            txtbAgeAdd.Text = "";
            txtbDesAdd.Text = "";
            txtbNameAdd.Text = "";
            txtbNationAdd.Text = "";
            //txtavatar.Text = "";
            txtbDirectorsAdd.Text = "";
            txtbIMDBAdd.Text = "";
            txtbSeasonAdd.Text = "";
            txtbVideoAdd.Text = ".mp4";
            txtbPosterAdd.Text = ".jpg";
            PCheckBox.IsEnabled = false;

        }

        private void btAdvancedSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
