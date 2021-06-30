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
        public static readonly DependencyProperty catMoviesProperties =
            DependencyProperty.Register("catMovies", typeof(ObservableCollection<Movie_Category>), typeof(AdminViewStatisticsVM));
        public static readonly DependencyProperty classifyMoviesProperties =
            DependencyProperty.Register("classifyMovies", typeof(ObservableCollection<Movie_classify>), typeof(AdminViewStatisticsVM));

        #region Command

        public ICommand cbDoanhThuCmd { get; set; }
        public ICommand SelectedDateChangedCmd { get; set; }

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

        private string _currentFilter = FILTER_DAY;

        public List<Payment_History> Revenue_ListPayment { get; set; }

        public ObservableCollection<Movie> statistics
        {
            get => (ObservableCollection<Movie>)GetValue(statisticsProperties);
            set => SetValue(statisticsProperties, value);
        }
        public ObservableCollection<Movie_Category> catMovies
        {
            get => (ObservableCollection<Movie_Category>)GetValue(catMoviesProperties);
            set => SetValue(catMoviesProperties, value);
        }
        public ObservableCollection<Movie_classify> classifyMovies
        {
            get => (ObservableCollection<Movie_classify>)GetValue(classifyMoviesProperties);
            set => SetValue(classifyMoviesProperties, value);
        }

        public SeriesCollection chartData
        {
            get => Data;
        }

        private SeriesCollection _revenueData;

        public SeriesCollection RevenueData
        {
            get => _revenueData;
            set
            {
                _revenueData = value;
                OnPropertyChanged();
            }
        }

        public SeriesCollection chartData3
        {
            get => Data3;
        }

        public AdminViewStatisticsVM()
        {
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
            RevenueData = new SeriesCollection();
            using (var db = new MediasManangementEntities())
            {
                statistics = new ObservableCollection<Movie>(db.Movies.OrderByDescending(x => x.NumberOfViews).Take(10).ToArray());

                //ds danh mục - phân loại phim
                catMovies = new ObservableCollection<Movie_Category>(db.Movie_Categories.Include("Movies").ToList());
                classifyMovies = new ObservableCollection<Movie_classify>(db.Movie_classify.Include("Movies").ToList());
            }

            // Command selection changed
            cbDoanhThuCmd = new RelayCommand<object>(cbDoanhThuChanged);
            SelectedDateChangedCmd = new RelayCommand<object>(SelectedDateChanged);
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
            RevenueSeriesCollection();
        }

        private void SelectedDateChanged(object o)
        {
            PeriodPayments();
            RevenueSeriesCollection();
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

        private void RevenueSeriesCollection()
        {
            RevenueData.Clear();
            foreach (Payment_History history in Revenue_ListPayment)
            {
                DateTime date = (DateTime)history.DateOfPayment;
                string title = _currentFilter == FILTER_DAY ? date.ToString("dd/MM/yyyy") :
                                _currentFilter == FILTER_MONTH ? date.ToString("MM/yyyy") :
                                date.ToString("yyyy");
                var found = RevenueData.Where(r => r.Title == title);
                if (found.Any())
                {
                    var column = found.First();
                    int newValue = (int)column.Values[0] + (int)history.Price;
                    column.Values = new ChartValues<int> { newValue };
                }
                else
                {
                    ColumnSeries newColumn = new ColumnSeries
                    {
                        Values = new ChartValues<int> { (int)history.Price.Value },
                        Title = title,
                        Margin = new Thickness(20, 10, 20, 10),
                    };

                    RevenueData.Add(newColumn);
                }
            }
        }
    }
}