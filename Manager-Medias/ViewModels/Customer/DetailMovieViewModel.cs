using Manager_Medias.Commands;
using Manager_Medias.CustomeModels;
using Manager_Medias.Models;
using Manager_Medias.Stores;
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
        private readonly UserStore _userStore;
        public static readonly DependencyProperty MovieProperty;
        public static bool liked = true;
        public ICommand CmdLikesClick { get; set; }
        public ICommand CmdShareClick { get; set; }
        public ICommand CmdErrorClick { get; set; }
        public ICommand CmdLike { get; set; }

        public ICommand CmdAddMyListClick { get; set; }

        public User currentUser => _userStore.CurrentUser;        

        static DetailMovieViewModel()
        {
            MovieProperty = DependencyProperty.Register("DetailMovies", typeof(DetailMovieCustomModel), typeof(DetailMovieViewModel));
        }

        public DetailMovieCustomModel DetailMovies
        {
            get => (DetailMovieCustomModel)GetValue(MovieProperty);
            set => SetValue(MovieProperty, value);
        }

        public DetailMovieViewModel()
        {
            CmdLikesClick = new RelayCommand<Object>(Likes);
            CmdShareClick = new RelayCommand<Object>(Share);
            CmdErrorClick = new RelayCommand<Object>(Error);
            CmdAddMyListClick = new RelayCommand<Object>(AddMyList);
            loaded();
        }

        public DetailMovieViewModel(UserStore userStore)
        {
            _userStore = userStore;
        }
        public void loaded()
        {
            using (var x = new MediasManangementEntities())
            {
                Movie Movie = x.Movies.FirstOrDefault() as Movie;
                DetailMovies = new DetailMovieCustomModel()
                {
                    Id = Movie.Id,
                    Name = Movie.Name,
                    Description = Movie.Description,
                    IMDB = Movie.IMDB.Value,
                    Level = "Cấp Độ " + x.Levels.Where(p => p.Id == Movie.Media.Lvl).FirstOrDefault().Name,
                    like = Movie.Likes.Value,
                    Category = Movie.Movie_Categories.Name,
                    Time = Movie.Time,
                    view = Movie.NumberOfViews.Value,
                    Image = Movie.Poster,
                    Video = Movie.Video,
                    Directors = Movie.Directors,
                    Nation = Movie.Nation,
                };
            }
        }

        public void Likes(Object ob)
        {
            var id = int.Parse(ob.ToString());
            using (var db = new MediasManangementEntities())
            {
                Movie mv = db.Movies.Where(p => p.Id == id).Single() as Movie;
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

        public void AddMyList(Object ob)
        {
            var id = ob.ToString();
            My_List my_List = new My_List()
            {
                IdMedia = int.Parse(id),
                Date = DateTime.Now.Date,
                IdProfile = 1,
            };
            using (var db = new MediasManangementEntities())
            {
                db.My_Lists.Add(my_List);
                db.SaveChanges();
            }
        }
    }
}
