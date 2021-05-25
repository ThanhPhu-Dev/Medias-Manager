using Manager_Medias.CustomeModels;
using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Manager_Medias.ViewModels.Customer
{
    public class DetailMovieViewModel: BaseViewModel
    {
        public static readonly DependencyProperty MovieProperty;

        static DetailMovieViewModel()
        {
            MovieProperty = DependencyProperty.Register("DetailMovies", typeof(DetailMovieCustomeModel), typeof(DetailMovieViewModel));
        }

        public DetailMovieCustomeModel DetailMovies
        {
            get => (DetailMovieCustomeModel)GetValue(MovieProperty);
            set => SetValue(MovieProperty, value);
        }

        public DetailMovieViewModel()
        {
            loaded();
        }
        public void loaded()
        {
            using (var x = new MediasManangementEntities())
            {
                Movies movies = x.Movies.FirstOrDefault() as Movies;
                DetailMovieCustomeModel dm = new DetailMovieCustomeModel()
                {
                    Name = movies.Name,
                    Description = movies.Description,
                    IMDB = movies.IMDB.Value,
                    Level = "Cấp Độ "+x.Levels.Where(p => p.Id == movies.Media.Lvl).FirstOrDefault().Name,
                    like = movies.Likes.Value,
                    Category = movies.Movie_Categories.Name,
                    Time= movies.Time,
                    view = movies.NumberOfViews.Value,
                    image = movies.Poster,
                };
                DetailMovies = dm;
            }
        }
    }
}
