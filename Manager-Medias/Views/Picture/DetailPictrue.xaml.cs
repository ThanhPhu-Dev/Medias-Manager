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

namespace Manager_Medias.Views.Picture
{
    /// <summary>
    /// Interaction logic for DetailPictrue.xaml
    /// </summary>
    public partial class DetailPictrue : UserControl
    {
        public DetailPictrue()
        {
            InitializeComponent();
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
            if (count == 3)
            {
                _timer.Stop();
                Storyboard sb = this.FindResource("CloseMessage") as Storyboard;
                Storyboard.SetTarget(sb, this.bd_Message);
                sb.Begin();
            }
        }
    }
}
