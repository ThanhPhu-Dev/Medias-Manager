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
        private int historyID { get; set; }

        public static readonly DependencyProperty MovieProperty =
            DependencyProperty.Register("DetailMovies", typeof(DetailMovieCustomModel), typeof(DetailMovieViewModel));

        public ICommand CmdLikesClick { get; set; }
        public ICommand CmdShareClick { get; set; }
        public ICommand CmdErrorClick { get; set; }
        public ICommand CmdLike { get; set; }
        public ICommand CmdAddMyListClick { get; set; }
        public ICommand CmdPlay { get; set; }

        private bool _checkSave;
        private double _sliderValue = 0;

        public double SliderValue
        {
            get => _sliderValue;
            set
            {
                _sliderValue = value;
                OnPropertyChanged();
            }
        }

        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
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
            CmdPlay = new RelayCommand<Object>(AddEventOnClosingViewModel);
            this.id = idMovie;
            loaded();
            CreateHistory();
        }

        public DetailMovieViewModel(int idMovie, double time)
        {
            CmdLikesClick = new RelayCommand<Object>(Likes);
            CmdShareClick = new RelayCommand<Object>(Share);
            CmdErrorClick = new RelayCommand<Object>(Error);
            CmdAddMyListClick = new RelayCommand<Object>(AddMyList);
            CmdPlay = new RelayCommand<Object>(AddEventOnClosingViewModel);

            this.id = idMovie;

            loaded();
            CreateHistory();

            GetTimeStartMedia(time);
        }

        public void AddEventOnClosingViewModel(Object o)
        {
            _navigationStore.CurrentContentViewModelChanged += OnClosingViewModel;
            Application.Current.MainWindow.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            using (var db = new MediasManangementEntities())
            {
                int milisecond = (int)SliderValue;

                var ht = db.View_History.Single(h => h.Id == this.historyID);
                ht.time = milisecond.ToString();

                db.SaveChanges();
            }
        }

        private void OnClosingViewModel()
        {
            using (var db = new MediasManangementEntities())
            {
                int milisecond = (int)SliderValue;

                var ht = db.View_History.Single(h => h.Id == this.historyID);
                ht.time = milisecond.ToString();

                db.SaveChanges();
            }
            // Remove event
            _navigationStore.CurrentContentViewModelChanged -= OnClosingViewModel;
            Application.Current.MainWindow.Closed -= MainWindow_Closed;
        }

        public void loaded()
        {
            using (var db = new MediasManangementEntities())
            {
                Movie Movie = db.Movies.Where(p => p.Id == id).FirstOrDefault() as Movie;
                DetailMovies = new DetailMovieCustomModel()
                {
                    Id = Movie.Id,
                    Name = Movie.Name,
                    Description = Movie.Description,
                    IMDB = Movie.IMDB.Value,
                    Level = "Cấp Độ " + db.Levels.Where(p => p.Id == Movie.Media.Lvl).FirstOrDefault().Name,
                    like = Movie.Likes.Value,
                    Category = Movie.Movie_Categories.Name,
                    Time = Movie.Time,
                    view = Movie.NumberOfViews.Value,
                    Image = Movie.Poster,
                    Video = Movie.Video,
                    Directors = Movie.Directors,
                    Nation = Movie.Nation,
                };
                CheckSave = db.My_Lists.Where(p => p.IdProfile == _userStore.CurrentProfile.Id && p.IdMedia == id).Any();
            }
        }

        public void Likes(Object ob)
        {
            var id = int.Parse(ob.ToString());
            Like lkenew = new Like()
            {
                Date = DateTime.Now.Date,
                IdMedia = id,
                IdProfile = _userStore.CurrentProfile.Id,
            };
            using (var db = new MediasManangementEntities())
            {
                Movie mv = db.Movies.Where(p => p.Id == id).Single() as Movie;
                Like like = db.Likes.Where(p => p.IdProfile == lkenew.IdProfile && p.IdMedia == lkenew.IdMedia).FirstOrDefault() as Like;
                if (like == null)
                {
                    mv.Likes++;
                    Message = "Đã thêm danh sách yêu thích";
                    db.Likes.Add(lkenew);
                }
                else
                {
                    mv.Likes--;
                    Message = "Đã xóa khỏi danh sách yêu thích";
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
                IdProfile = _userStore.CurrentProfile.Id,
            };
            using (var db = new MediasManangementEntities())
            {
                My_List mydelete = db.My_Lists.Where(p => p.IdMedia == my_List.IdMedia && p.IdProfile == my_List.IdProfile).FirstOrDefault() as My_List;
                if (mydelete != null)
                {
                    CheckSave = false;
                    Message = "Đã xóa khỏi danh sách myList";
                    db.My_Lists.Remove(mydelete);
                }
                else
                {
                    CheckSave = true;
                    Message = "Đã thêm danh sách myList";
                    db.My_Lists.Add(my_List);
                }
                db.SaveChanges();
            }
        }

        public void CreateHistory()
        {
            using (var db = new MediasManangementEntities())
            {
                View_History ht = new View_History
                {
                    Date = DateTime.Now,
                    IdMedia = this.id,
                    IdProfile = _userStore.CurrentProfile.Id,
                    time = "0"
                };

                db.View_History.Add(ht);
                db.SaveChanges();

                this.historyID = ht.Id;
            }
        }

        public void GetTimeStartMedia(double time)
        {
            SliderValue = time;
        }
    }
}