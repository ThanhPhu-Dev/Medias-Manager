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

        public AdminViewStatisticsVM()
        {
            using (var db = new MediasManangementEntities())
            {
                statistics = new ObservableCollection<Movie>(db.Movies.OrderByDescending(x => x.NumberOfViews).Take(10).ToList());
            }
        }
    }
}
