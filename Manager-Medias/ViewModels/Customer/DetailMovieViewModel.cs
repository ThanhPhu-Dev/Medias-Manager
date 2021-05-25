using Manager_Medias.Commands;
using Manager_Medias.CustomeModels;
using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class DetailMovieViewModel : BaseViewModel
    {
        public static readonly DependencyProperty MovieProperty;
        public static bool liked = true;
        public ICommand CmdLikesClick { get; set; }
        public ICommand CmdShareClick { get; set; }
        public ICommand CmdErrorClick { get; set; }
        

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
            CmdLikesClick = new RelayCommand<Object>(Likes);
            CmdShareClick = new RelayCommand<Object>(Share);
            CmdErrorClick = new RelayCommand<Object>(Error);
            loaded();
        }
        public void loaded()
        {
            using (var x = new MediasManangementEntities())
            {
                Movies movies = x.Movies.FirstOrDefault() as Movies;
                DetailMovieCustomeModel dm = new DetailMovieCustomeModel()
                {
                    Id = movies.Id,
                    Name = movies.Name,
                    Description = movies.Description,
                    IMDB = movies.IMDB.Value,
                    Level = "Cấp Độ " + x.Levels.Where(p => p.Id == movies.Media.Lvl).FirstOrDefault().Name,
                    like = movies.Likes.Value,
                    Category = movies.Movie_Categories.Name,
                    Time = movies.Time,
                    view = movies.NumberOfViews.Value,
                    Image = movies.Poster,
                    Video = movies.Video,
                    Directors = movies.Directors,
                    Nation = movies.Nation,
                };
                DetailMovies = dm;
            }
        }

        public void Likes(Object ob)
        {
            var id = int.Parse(ob.ToString());
            using (var db = new MediasManangementEntities())
            {
                Movies mv = db.Movies.Where(p => p.Id == id).Single() as Movies;
                if (liked == true)
                {
                    mv.Likes++;
                    liked = false;
                }
                else
                {
                    mv.Likes--;
                    liked = true;
                };
                db.SaveChanges();
                loaded();
            }
        }

        public void Share(Object ob)
        {
            MessageBox.Show("Chức đang được thực hiện.");
        }

        public void Error(Object ob)
        {
            MessageBox.Show("Cảm ơn đã thông báo, chúng tôi sẽ kiểm tra và khắc phục.");
        }
    }
}
