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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Manager_Medias.Views.Audio
{
    /// <summary>
    /// Interaction logic for DetailAudioView.xaml
    /// </summary>
    public partial class DetailAudioView : UserControl
    {
        private DispatcherTimer seeker;
        private bool isSeekingMedia = false;

        public DetailAudioView()
        {
            InitializeComponent();
            audio.Play();
        }

        private void MediaTimeline_CurrentTimeInvalidated(object sender, EventArgs e)
        {
        }

        private void audio_MediaOpened(object sender, RoutedEventArgs e)
        {
            timelineSlider.Maximum = audio.NaturalDuration.TimeSpan.TotalMilliseconds;
            seeker = new DispatcherTimer();
            seeker.Interval = TimeSpan.FromSeconds(1);
            seeker.Tick += Seeker_Tick;
            seeker.Start();
        }

        private void Seeker_Tick(object sender, EventArgs e)
        {
            if (!isSeekingMedia)
            {
                timelineSlider.Value = audio.Position.TotalMilliseconds;
            }
        }

        private void timelineSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            isSeekingMedia = true;
        }

        private void timelineSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            isSeekingMedia = false;
            int SliderValue = (int)timelineSlider.Value;

            // Overloaded constructor takes the arguments days, hours, minutes, seconds, milliseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            audio.Position = ts;
            isSeekingMedia = false;
        }

        private void btn_playvideo_Checked(object sender, RoutedEventArgs e)
        {
            audio.Pause();
        }

        private void btn_playvideo_Unchecked(object sender, RoutedEventArgs e)
        {
            audio.Play();
        }

        //Message 
        private DispatcherTimer _timer;
        private int count;
        private void btn_likeAudio_Click(object sender, RoutedEventArgs e)
        {
            _timer = null;
            count = 0;
            StartTimer();
        }
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

        void _timer_Tick(object sender, EventArgs e)
        {
            count++;
            if(count == 3)
            {
                _timer.Stop();
                Storyboard sb = this.FindResource("CloseMessage") as Storyboard;
                Storyboard.SetTarget(sb, this.bd_Message);
                sb.Begin();
            }
        }
    }
}