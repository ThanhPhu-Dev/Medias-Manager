using LiveCharts;
using LiveCharts.Wpf;
using Manager_Medias.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Manager_Medias.Views.Admin
{
    /// <summary>
    /// Interaction logic for StatisticalUserControl.xaml
    /// </summary>
    public partial class StatisticalUserControl : UserControl
    {
        private AdminViewStatisticsVM vm;

        public StatisticalUserControl()
        {
            InitializeComponent();

            vm = new AdminViewStatisticsVM();
            DataContext = vm;

            Thread.CurrentThread.CurrentCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
        }

        private void PieChart_DataClick(object sender, LiveCharts.ChartPoint chartPoint)
        {
            var chart = chartPoint.ChartView as PieChart;
            foreach (PieSeries pie in chart.Series)
            {
                pie.PushOut = 0;
            }

            var neo = chartPoint.SeriesView as PieSeries;
            neo.PushOut = 30;
        }
    }
}