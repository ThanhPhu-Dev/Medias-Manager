using Manager_Medias.ViewModels;
using Manager_Medias.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Manager_Medias.Views.Movie
{
    /// <summary>
    /// Interaction logic for DetailMovie.xaml
    /// </summary>
    public partial class DetailMovie : UserControl
    {
        private DispatcherTimer seeker;
        private bool isSeekingMedia = false;

        public DetailMovie()
        {
            InitializeComponent();
        }

        private void timelineSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            isSeekingMedia = true;
        }

        private void timelineSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            int SliderValue = (int)timelineSlider.Value;

            // Overloaded constructor takes the arguments days, hours, minutes, seconds, milliseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            mea_video.Position = ts;

            isSeekingMedia = false;
        }

        private void audio_MediaOpened(object sender, RoutedEventArgs e)
        {
            timelineSlider.Maximum = mea_video.NaturalDuration.TimeSpan.TotalMilliseconds;

            int SliderValue = (int)timelineSlider.Value;

            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            mea_video.Position = ts;

            seeker = new DispatcherTimer();
            seeker.Interval = TimeSpan.FromSeconds(1);
            seeker.Tick += Seeker_Tick;
            seeker.Start();

            mea_video.MediaEnded += Mea_video_MediaEnded;
            mea_video.MediaFailed += Mea_video_MediaFailed;
        }

        private void Mea_video_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            Trace.WriteLine("seek failed");
        }

        private void Mea_video_MediaEnded(object sender, RoutedEventArgs e)
        {
            seeker.Stop();
        }

        private void Seeker_Tick(object sender, EventArgs e)
        {
            if (!isSeekingMedia)
            {
                timelineSlider.Value = mea_video.Position.TotalMilliseconds;
            }
        }

        private void btn_playvideo_Checked(object sender, RoutedEventArgs e)
        {
            mea_video.Pause();
            seeker.Stop();
        }

        private void btn_playvideo_Unchecked(object sender, RoutedEventArgs e)
        {
            mea_video.Play();
            seeker.Start();
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            Container.ScrollToBottom();
            mea_video.Play();
        }

        //Message
        private DispatcherTimer _timer;

        private int count;

        public void StartTimer()
        {
            if (_timer == null)
            {
                _timer = new DispatcherTimer();
                _timer.Tick += _timer_Tick;
            }

            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Start();
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            count++;
            if (count == 4)
            {
                _timer.Stop();
                Storyboard sb = this.FindResource("CloseMessage") as Storyboard;
                Storyboard.SetTarget(sb, this.bd_Message);
                sb.Begin();
            }
        }

        private void likec_click(object sender, RoutedEventArgs e)
        {
            _timer = null;
            count = 0;
            StartTimer();
        }

        private void mea_video_Unloaded(object sender, RoutedEventArgs e)
        {
            if (seeker != null && seeker.IsEnabled)
            {
                seeker.Stop();
            }
        }
    }
}