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
        private int id;
        public static readonly DependencyProperty MovieProperty;
        public ICommand CmdLikesClick { get; set; }
        public ICommand CmdShareClick { get; set; }
        public ICommand CmdErrorClick { get; set; }
        public ICommand CmdLike { get; set; }
        public ICommand CmdAddMyListClick { get; set; }
        private bool _checkSave;

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

        public bool CheckSave
        {
            get => _checkSave;
            set
            {
                _checkSave = value;
                OnPropertyChanged();
            }
        }

        public DetailMovieViewModel(int idMovie)
        {
            CmdLikesClick = new RelayCommand<Object>(Likes);
            CmdShareClick = new RelayCommand<Object>(Share);
            CmdErrorClick = new RelayCommand<Object>(Error);
            CmdAddMyListClick = new RelayCommand<Object>(AddMyList);
            this.id = idMovie;
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
                Movie Movie = x.Movies.Where(p => p.Id == id).FirstOrDefault() as Movie;
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
            Like lkenew = new Like()
            {
                Date = DateTime.Now.Date,
                IdMedia = id,
                IdProfile = 1,
            };
            using (var db = new MediasManangementEntities())
            {
                Movie mv = db.Movies.Where(p => p.Id == id).Single() as Movie;
                Like like = db.Likes.Where(p => p.IdProfile == lkenew.IdProfile && p.IdMedia == lkenew.IdMedia).FirstOrDefault() as Like;
                if (like == null)
                {
                    mv.Likes++;
                    db.Likes.Add(lkenew);
                }
                else
                {
                    mv.Likes--;
                    db.Likes.Remove(like);
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
            var id = int.Parse(ob.ToString());
            My_List my_List = new My_List()
            {
                IdMedia = id,
                Date = DateTime.Now.Date,
                IdProfile = 1,
            };
            using (var db = new MediasManangementEntities())
            {
                My_List mydelete = db.My_Lists.Where(p => p.IdMedia == my_List.IdMedia && p.IdProfile == my_List.IdProfile).FirstOrDefault() as My_List;
                if (mydelete != null)
                {
                    db.My_Lists.Remove(mydelete);
                }
                else
                {
                    db.My_Lists.Add(my_List);
                }
                db.SaveChanges();
            }
        }
    }
}