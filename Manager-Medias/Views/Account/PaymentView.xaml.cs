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

namespace Manager_Medias.Views.Account
{
    /// <summary>
    /// Interaction logic for PaymentView.xaml
    /// </summary>
    public partial class PaymentView : UserControl
    {
        public PaymentView()
        {
            InitializeComponent();
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

        void _timer_Tick(object sender, EventArgs e)
        {
            count++;
            if (count == 3)
            {
                _timer.Stop();
                Storyboard sb = this.FindResource("CloseMessage") as Storyboard;
                //Storyboard.SetTarget(sb, this.bd_Message);
                sb.Begin();
            }
        }

        private void btn_lvlcheck(object sender, RoutedEventArgs e)
        {
            var bt = sender as Button;
            var lvl = bt.Tag.ToString();
            var lvlcurrent = level.Text;
            if (int.Parse(lvlcurrent) >= int.Parse(lvl))
            {
                Storyboard sb = this.FindResource("OpenMessage") as Storyboard;
                sb.Begin();
                _timer = null;
                count = 0;
                StartTimer();
            }
        }

        private void showmessSuccess_click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("OpenMessage") as Storyboard;
            sb.Begin();
            _timer = null;
            count = 0;
            StartTimer();
        }
    }
}
