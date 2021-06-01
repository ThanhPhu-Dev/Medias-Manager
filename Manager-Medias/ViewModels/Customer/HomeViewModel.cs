using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Manager_Medias.ViewModels.Customer
{
    public class HomeViewModel : BaseViewModel
    {
        public static readonly DependencyProperty MovieListProperty;

        public ObservableCollection<Movie> Movies
        {
            get => (ObservableCollection<Movie>)GetValue(MovieListProperty);
            set => SetValue(MovieListProperty, value);
        }

        public static ObservableCollection<Audio> Audios { get; set; }

        public static ObservableCollection<Album_Detail> Albums { get; set; }

        static HomeViewModel()
        {
            MovieListProperty = DependencyProperty.Register("MovieList", typeof(ObservableCollection<Movie>), typeof(HomeViewModel));
        }

        public HomeViewModel()
        {
            using (var db = new MediasManangementEntities())
            {
                Albums = new ObservableCollection<Album_Detail>(db.Album_Details.ToList());
                Audios = new ObservableCollection<Audio>(db.Audios.ToList());
                Movies = new ObservableCollection<Movie>(db.Movies.ToList());
            }
        }
    }
}