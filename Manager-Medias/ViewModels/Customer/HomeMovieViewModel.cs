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
    public class HomeMovieViewModel:BaseViewModel
    {
        public static readonly DependencyProperty MovieListProperty;
        static HomeMovieViewModel()
        {
            MovieListProperty = DependencyProperty.Register("MovieList", typeof(ObservableCollection<Movie>), typeof(HomeMovieViewModel));
        }
        public ObservableCollection<Movie> MovieList
        {
            get => (ObservableCollection<Movie>)GetValue(MovieListProperty);
            set => SetValue(MovieListProperty, value);
        }

        public HomeMovieViewModel()
        {
            LoadMovie();
        }

        private void LoadMovie()
        {
            using (var db = new MediasManangementEntities())
            {
                //cập nhật danh sách bài hát liên quan (chung danh mục) cho UI
                MovieList = new ObservableCollection<Movie>(db.Movies.ToList());
            }
        }
    }
}
