using LiveCharts;
using LiveCharts.Wpf;
using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Manager_Medias.ViewModels.Admin
{
    public class AdminViewStatisticsVM : DependencyObject
    {
        public static readonly DependencyProperty statisticsProperties;
        


        static AdminViewStatisticsVM()
        {
            statisticsProperties = DependencyProperty.Register("statistics", typeof(ObservableCollection<Movie>), typeof(AdminViewStatisticsVM));
        }

        public ObservableCollection<Movie> statistics
        {
            get => (ObservableCollection<Movie>)GetValue(statisticsProperties);
            set => SetValue(statisticsProperties, value);
        }

        public SeriesCollection chartData
        {
            get => Data;
        }

        public SeriesCollection chartData2
        {
            get => Data2;
        }

        public SeriesCollection chartData3
        {
            get => Data3;
        }


        public AdminViewStatisticsVM()
        {
            using (var db = new MediasManangementEntities())
            {
                statistics = new ObservableCollection<Movie>(db.Movies.OrderByDescending(x => x.NumberOfViews).Take(10).ToArray());
            }
            
           

        }
        public SeriesCollection Data => new SeriesCollection()
        {
            new PieSeries()
            {
                Values = new ChartValues<int> { (int)statistics[0].NumberOfViews} , Title = statistics[0].Name
            },
            new PieSeries()
            {
                Values = new ChartValues<int> { (int)statistics[1].NumberOfViews} , Title = statistics[1].Name
            },
            new PieSeries()
            {
                Values = new ChartValues<int> { (int)statistics[2].NumberOfViews} , Title = statistics[2].Name
            },
            new PieSeries()
            {
                Values = new ChartValues<int> { (int)statistics[3].NumberOfViews} , Title = statistics[3].Name
            },
            new PieSeries()
            {
                Values = new ChartValues<int> { (int)statistics[4].NumberOfViews} , Title = statistics[4].Name
            },
            new PieSeries()
            {
                Values = new ChartValues<int> { (int)statistics[5].NumberOfViews} , Title = statistics[5].Name
            },
            new PieSeries()
            {
                Values = new ChartValues<int> { (int)statistics[6].NumberOfViews} , Title = statistics[6].Name
            },
            new PieSeries()
            {
                Values = new ChartValues<int> { (int)statistics[7].NumberOfViews} , Title = statistics[7].Name
            },
            new PieSeries()
            {
                Values = new ChartValues<int> { (int)statistics[8].NumberOfViews} , Title = statistics[8].Name
            },
            new PieSeries()
            {
                Values = new ChartValues<int> { (int)statistics[9].NumberOfViews} , Title = statistics[9].Name
            }
        };

        public SeriesCollection Data2 => new SeriesCollection() // Biến chứa dữ liệu biểu đồ
        {
             new ColumnSeries()
            {
                Values = new ChartValues<float> { 124124} , Title = "Tháng 1"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 57342} , Title = "Tháng 2"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 56233 } , Title ="Tháng 3"
            },
            new ColumnSeries()
            {
                Values = new ChartValues<float> { 235235 }, Title = "Tháng 4"
            },
            
        };

        public SeriesCollection Data3 => new SeriesCollection() // Biến chứa dữ liệu biểu đồ
        {
             new PieSeries()
            {
                Values = new ChartValues<float> { 124124} , Title = "Tháng 11"
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { 57342} , Title = "Tháng 12"
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { 56233 } , Title ="Tháng 3"
            },
            new PieSeries()
            {
                Values = new ChartValues<float> { 235235 }, Title = "Tháng 4"
            },
        };

    }
}
