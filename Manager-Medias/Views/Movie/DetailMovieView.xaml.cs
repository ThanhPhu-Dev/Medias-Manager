using Manager_Medias.ViewModels;
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
            isSeekingMedia = false;
            int SliderValue = (int)timelineSlider.Value;

            // Overloaded constructor takes the arguments days, hours, minutes, seconds, milliseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            mea_video.Position = ts;
            isSeekingMedia = false;
        }

        private void audio_MediaOpened(object sender, RoutedEventArgs e)
        {
            InitPosition();
            timelineSlider.Maximum = mea_video.NaturalDuration.TimeSpan.TotalMilliseconds;
            seeker = new DispatcherTimer();
            seeker.Interval = TimeSpan.FromSeconds(1);
            seeker.Tick += Seeker_Tick;
            seeker.Start();
        }

        private void InitPosition()
        {
            int SliderValue = (int)timelineSlider.Value;

            // Overloaded constructor takes the arguments days, hours, minutes, seconds, milliseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            mea_video.Position = ts;
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
        }

        private void btn_playvideo_Unchecked(object sender, RoutedEventArgs e)
        {
            mea_video.Play();
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            Container.ScrollToBottom();
            mea_video.Play();
        }
    }
}