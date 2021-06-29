using LiveCharts;
using LiveCharts.Wpf;
using Manager_Medias.Commands;
using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Admin
{
    public class AdminViewStatisticsVM : BaseViewModel
    {
        private const string FILTER_DAY = "Days";
        private const string FILTER_MONTH = "Month";
        private const string FILTER_YEAR = "Year";

        public static readonly DependencyProperty statisticsProperties =
            DependencyProperty.Register("statistics", typeof(ObservableCollection<Movie>), typeof(AdminViewStatisticsVM));

        #region Command

        public ICommand cbDoanhThuCmd { get; set; }

        #endregion Command

        #region Binding

        private DateTime _formDate;

        public DateTime FromDate
        {
            get => _formDate;
            set
            {
                _formDate = value;
                OnPropertyChanged();
            }
        }

        private DateTime _toDate;

        public DateTime ToDate
        {
            get => _toDate;
            set
            {
                _toDate = value;
                OnPropertyChanged();
            }
        }

        private string _selectedDateFormat = "MM/dd/yyyy";

        public string SelectedDateFormat
        {
            get => _selectedDateFormat;
            set
            {
                _selectedDateFormat = value;
                OnPropertyChanged();
            }
        }

        #endregion Binding

        private string _currentFilter = string.Empty;

        public List<Payment_History> Revenue_ListPayment { get; set; }

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
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;

            using (var db = new MediasManangementEntities())
            {
                statistics = new ObservableCollection<Movie>(db.Movies.OrderByDescending(x => x.NumberOfViews).Take(10).ToArray());
            }

            // Command selection changed
            cbDoanhThuCmd = new RelayCommand<object>(cbDoanhThuChanged);
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

        private void cbDoanhThuChanged(object selected)
        {
            string selectedName = (string)selected;
            switch (selectedName)
            {
                case FILTER_DAY:
                    _currentFilter = FILTER_DAY;
                    SelectedDateFormat = "MM/dd/yyyy";
                    break;

                case FILTER_MONTH:
                    _currentFilter = FILTER_MONTH;
                    SelectedDateFormat = "MM/yyyy";
                    break;

                case FILTER_YEAR:
                    _currentFilter = FILTER_YEAR;
                    SelectedDateFormat = "yyyy";
                    break;

                default:
                    _currentFilter = string.Empty;
                    break;
            }
            PeriodPayments();
        }

        private void PeriodPayments()
        {
            DateTime from = DateToFilter(FromDate);

            DateTime to = DateToFilter(ToDate);

            using (var db = new MediasManangementEntities())
            {
                Revenue_ListPayment = db.Payment_History.ToList();
            }

            Revenue_ListPayment = Revenue_ListPayment.Where(h => from <= DateToFilter((DateTime)h.DateOfPayment) &&
                                                                to >= DateToFilter((DateTime)h.DateOfPayment)).ToList();
        }

        private DateTime DateToFilter(DateTime dt)
        {
            DateTime dtFilter = _currentFilter == FILTER_DAY ? new DateTime(dt.Year, dt.Month, dt.Day) :
                            _currentFilter == FILTER_MONTH ? new DateTime(dt.Year, dt.Month, 1) :
                            new DateTime(dt.Year, 1, 1);

            return dtFilter;
        }
    }
}