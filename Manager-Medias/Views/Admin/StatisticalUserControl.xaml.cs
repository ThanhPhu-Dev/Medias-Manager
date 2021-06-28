using LiveCharts;
using LiveCharts.Wpf;
using Manager_Medias.ViewModels.Admin;
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

namespace Manager_Medias.Views.Admin
{
    /// <summary>
    /// Interaction logic for StatisticalUserControl.xaml
    /// </summary>
    public partial class StatisticalUserControl : UserControl
    {
        AdminViewStatisticsVM vm;
        public StatisticalUserControl()
        {
            InitializeComponent();

            vm = new AdminViewStatisticsVM();
            DataContext = vm;
            mostRateChartPanel.Visibility = Visibility.Hidden;
        }

        private void Button_Tab_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            GridCursor.Margin = new Thickness((250 * index + 30), 0, 0, 0);

            if(index == 0)
            {
                mostViewChartPanel.Visibility = Visibility.Visible;
                mostRateChartPanel.Visibility = Visibility.Hidden;
            }
            if (index == 1)
            {
                mostViewChartPanel.Visibility = Visibility.Hidden;
                mostRateChartPanel.Visibility = Visibility.Visible;
            }
            
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

      

        public SeriesCollection Data2 => new SeriesCollection() // Biến chứa dữ liệu biểu đồ
        {
             new ColumnSeries()
            {
                Values = new ChartValues<float> { 124124} , Title = "Spider man: Far from home"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 57342} , Title = "Bố già lắm chiêu"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 56233 } , Title ="Gái già"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 235235 }, Title = "Còn cái nịt"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 54745} , Title = "Còn đúng cái nịt thôi"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 235} , Title = "One piece Movie 3"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 23425} , Title = "Trò chơi vương quyền 7"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 1212} , Title = "Captain Mavel"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 9865} , Title = "Trò chơi vương quyền 1"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 12314} , Title = "Còn đúng cái nịt thôi"
            },
        };
    }
}
