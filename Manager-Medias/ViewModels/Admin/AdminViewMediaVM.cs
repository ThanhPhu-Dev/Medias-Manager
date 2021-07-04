using HtmlAgilityPack;
using Manager_Medias.Commands;
using Manager_Medias.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.IO;
using Manager_Medias.CustomModels;
using System.ComponentModel;

namespace Manager_Medias.ViewModels.Admin
{
    class AdminViewMediaVM : DependencyObject
    {
        public static readonly DependencyProperty MovieListProperty;
        public static readonly DependencyProperty MostViewMovieListProperty;
        public static readonly DependencyProperty MostLikeMovieListProperty;
        public static readonly DependencyProperty NewMovieListProperty;

        public static readonly DependencyProperty MovieProperty;

        public static readonly DependencyProperty CategoryListProperty;

        public static readonly DependencyProperty UserLevelListProperty;
        public static readonly DependencyProperty UserLevelProperty;


        public ICommand CmdAddMovie { get; }
        public ICommand CmdUpdateIMDBrating { get;  }

        public ICommand CmdUpdateMovie { get; }

        public ICommand CmdDeleteMovie { get; }

        static AdminViewMediaVM()
        {

            MovieListProperty = DependencyProperty.Register("MovieList",
                                typeof(ListCollectionView), typeof(AdminViewMediaVM));
            MostViewMovieListProperty = DependencyProperty.Register("ViewMovieList",
                                typeof(ListCollectionView), typeof(AdminViewMediaVM));
            MostLikeMovieListProperty = DependencyProperty.Register("LikeMovieList",
                                typeof(ListCollectionView), typeof(AdminViewMediaVM));
            NewMovieListProperty = DependencyProperty.Register("NewMovieList",
                                typeof(ListCollectionView), typeof(AdminViewMediaVM));

            MovieProperty = DependencyProperty.Register("Movie",
                            typeof(MovieCustomModel), typeof(AdminViewMediaVM));


            CategoryListProperty = DependencyProperty.Register("CategoryList",
                               typeof(ListCollectionView), typeof(AdminViewMediaVM));

            UserLevelListProperty = DependencyProperty.Register("UserLevelList",
                               typeof(ListCollectionView), typeof(AdminViewMediaVM));

            UserLevelProperty = DependencyProperty.Register("Level",
                typeof(Media), typeof(AdminViewMediaVM));


        }

        public ListCollectionView MovieList
        {

            get => (ListCollectionView)GetValue(MovieListProperty);
            set => SetValue(MovieListProperty, value);
        }

        public ListCollectionView NewMovieList
        {

            get => (ListCollectionView)GetValue(NewMovieListProperty);
            set => SetValue(NewMovieListProperty, value);
        }

        public ListCollectionView MostViewMovieList
        {

            get => (ListCollectionView)GetValue(MostViewMovieListProperty);
            set => SetValue(MostViewMovieListProperty, value);
        }

        public int MovieCount { get; set; }
        public int CategoryCount { get; set; }
        public int totalView { get; set; }
        public int totalLike { get; set; }

        public ListCollectionView MostLikeMovieList
        {

            get => (ListCollectionView)GetValue(MostLikeMovieListProperty);
            set => SetValue(MostLikeMovieListProperty, value);
        }

        public MovieCustomModel Movie
        {
            get => (MovieCustomModel)GetValue(MovieProperty);
            set => SetValue(MovieProperty, value);
        }

        public ListCollectionView CategoryList
        {
            get => (ListCollectionView)GetValue(CategoryListProperty);
            set => SetValue(CategoryListProperty, value);
        }

        public ListCollectionView UserLevelList
        {
            get => (ListCollectionView)GetValue(UserLevelListProperty);
            set => SetValue(UserLevelListProperty, value);
        }

        public Media Level
        {
            get => (Media)GetValue(UserLevelProperty);
            set => SetValue(UserLevelProperty, value);
        }

        int check = 0;
        public AdminViewMediaVM()
        {
           
            CmdAddMovie = new RelayCommand<object>(AddMovie);
            CmdUpdateIMDBrating = new RelayCommand<object>(UpdateIMDBrating);
            CmdUpdateMovie = new RelayCommand<object>(UpdateMovie);
            CmdDeleteMovie = new RelayCommand<object>(DeleteMovie);

            
            using (var db = new MediasManangementEntities())
            {
                var getMovie = new ListCollectionView(db.Movies.ToList());
                BindingList<MovieCustomModel> MovieCustomList = new BindingList<MovieCustomModel>();
                foreach (Movie m in getMovie)
                {
                    var movie = new MovieCustomModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Poster = m.Poster,
                        IdCategory = (int)m.IdCategory,
                        Nation = m.Nation,
                        Age = (int)m.Age,
                        Season = m.Season,
                        Directors = m.Directors,
                        Description = m.Description,
                        Video = m.Video,
                        Likes = (int)m.Likes,
                        IMDB = (double)m.IMDB,
                        NumberOfViews = (int)m.NumberOfViews,
                        Time = m.Time,
                    };
                    MovieCustomList.Add(movie);
                }
                MovieList = new ListCollectionView(MovieCustomList);

                CategoryList = new ListCollectionView(db.Movie_Categories.ToList());

                UserLevelList = new ListCollectionView(db.Levels.ToList());

                var getMovieNew = new ListCollectionView(db.Movies.OrderByDescending(x => x.CreateAt).Take(10).ToList());
                BindingList<MovieCustomModel> MovieCustomListNew = new BindingList<MovieCustomModel>();
                foreach (Movie m in getMovieNew)
                {
                    var movie = new MovieCustomModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Poster = m.Poster,
                        IdCategory = (int)m.IdCategory,
                        Nation = m.Nation,
                        Age = (int)m.Age,
                        Season = m.Season,
                        Directors = m.Directors,
                        Description = m.Description,
                        Video = m.Video,
                        Likes = (int)m.Likes,
                        IMDB = (double)m.IMDB,
                        NumberOfViews = (int)m.NumberOfViews,
                        Time = m.Time,
                    };
                    MovieCustomListNew.Add(movie);
                }
                NewMovieList = new ListCollectionView(MovieCustomListNew);


                var getMovieLikes = new ListCollectionView(db.Movies.OrderByDescending(x => x.Likes).Take(10).ToList());
                BindingList<MovieCustomModel> MovieCustomListLike = new BindingList<MovieCustomModel>();
                foreach (Movie m in getMovieLikes)
                {
                    var movie = new MovieCustomModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Poster = m.Poster,
                        IdCategory = (int)m.IdCategory,
                        Nation = m.Nation,
                        Age = (int)m.Age,
                        Season = m.Season,
                        Directors = m.Directors,
                        Description = m.Description,
                        Video = m.Video,
                        Likes = (int)m.Likes,
                        IMDB = (double)m.IMDB,
                        NumberOfViews = (int)m.NumberOfViews,
                        Time = m.Time,
                    };
                    MovieCustomListLike.Add(movie);
                }
                MostLikeMovieList = new ListCollectionView(MovieCustomListLike);


                var getMovieView = new ListCollectionView(db.Movies.OrderByDescending(x => x.NumberOfViews).Take(10).ToList());
                BindingList<MovieCustomModel> MovieCustomListView = new BindingList<MovieCustomModel>();
                foreach (Movie m in getMovieView)
                {
                    var movie = new MovieCustomModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Poster = m.Poster,
                        IdCategory = (int)m.IdCategory,
                        Nation = m.Nation,
                        Age = (int)m.Age,
                        Season = m.Season,
                        Directors = m.Directors,
                        Description = m.Description,
                        Video = m.Video,
                        Likes = (int)m.Likes,
                        IMDB = (double)m.IMDB,
                        NumberOfViews = (int)m.NumberOfViews,
                        Time = m.Time,
                    };
                    MovieCustomListView.Add(movie);
                }
                MostViewMovieList = new ListCollectionView(MovieCustomListView);

                MovieCount = db.Movies.Count();
                CategoryCount = db.Movie_Categories.Count();

                totalLike = (int)db.Movies.Sum(l => l.Likes);
                totalView = (int)db.Movies.Sum(l => l.NumberOfViews);
            }

            MovieList.CurrentChanged += (_, e) =>
            {
                var MovieCurrent = MovieList.CurrentItem as MovieCustomModel;
                if (MovieCurrent == null)
                    return;

                Movie = new MovieCustomModel
                {
                    Id = MovieCurrent.Id,
                    Name = MovieCurrent.Name,
                    Poster = MovieCurrent.Poster,
                    IdCategory = (int)MovieCurrent.IdCategory,
                    Nation = MovieCurrent.Nation,
                    Age = (int)MovieCurrent.Age,
                    Season = MovieCurrent.Season,
                    Directors = MovieCurrent.Directors,
                    Description = MovieCurrent.Description,
                    Video = MovieCurrent.Video,
                    Likes = (int)MovieCurrent.Likes,
                    IMDB = (double)MovieCurrent.IMDB,
                    NumberOfViews = (int)MovieCurrent.NumberOfViews,
                    Time = MovieCurrent.Time,
                };
               

                using (var db = new MediasManangementEntities())
                {
                    var level = db.Medias.Find(Movie.Id);
                    Level = level;
                }

                check = 1;
            };


            NewMovieList.CurrentChanged += (_, e) =>
            {
                var MovieCurrent = NewMovieList.CurrentItem as MovieCustomModel;
                if (MovieCurrent == null)
                    return;

                Movie = new MovieCustomModel
                {
                    Id = MovieCurrent.Id,
                    Name = MovieCurrent.Name,
                    Poster = MovieCurrent.Poster,
                    IdCategory = (int)MovieCurrent.IdCategory,
                    Nation = MovieCurrent.Nation,
                    Age = (int)MovieCurrent.Age,
                    Season = MovieCurrent.Season,
                    Directors = MovieCurrent.Directors,
                    Description = MovieCurrent.Description,
                    Video = MovieCurrent.Video,
                    Likes = (int)MovieCurrent.Likes,
                    IMDB = (double)MovieCurrent.IMDB,
                    NumberOfViews = (int)MovieCurrent.NumberOfViews,
                    Time = MovieCurrent.Time,
                };


                using (var db = new MediasManangementEntities())
                {
                    var level = db.Medias.Find(Movie.Id);
                    Level = level;
                }

                check = 2;
            };

            MostLikeMovieList.CurrentChanged += (_, e) =>
            {
                var MovieCurrent = MostLikeMovieList.CurrentItem as MovieCustomModel;
                if (MovieCurrent == null)
                    return;

                Movie = new MovieCustomModel
                {
                    Id = MovieCurrent.Id,
                    Name = MovieCurrent.Name,
                    Poster = MovieCurrent.Poster,
                    IdCategory = (int)MovieCurrent.IdCategory,
                    Nation = MovieCurrent.Nation,
                    Age = (int)MovieCurrent.Age,
                    Season = MovieCurrent.Season,
                    Directors = MovieCurrent.Directors,
                    Description = MovieCurrent.Description,
                    Video = MovieCurrent.Video,
                    Likes = (int)MovieCurrent.Likes,
                    IMDB = (double)MovieCurrent.IMDB,
                    NumberOfViews = (int)MovieCurrent.NumberOfViews,
                    Time = MovieCurrent.Time,
                };


                using (var db = new MediasManangementEntities())
                {
                    var level = db.Medias.Find(Movie.Id);
                    Level = level;
                }

                check = 3;
            };

            MostViewMovieList.CurrentChanged += (_, e) =>
            {
                var MovieCurrent = MostViewMovieList.CurrentItem as MovieCustomModel;
                if (MovieCurrent == null)
                    return;

                Movie = new MovieCustomModel
                {
                    Id = MovieCurrent.Id,
                    Name = MovieCurrent.Name,
                    Poster = MovieCurrent.Poster,
                    IdCategory = (int)MovieCurrent.IdCategory,
                    Nation = MovieCurrent.Nation,
                    Age = (int)MovieCurrent.Age,
                    Season = MovieCurrent.Season,
                    Directors = MovieCurrent.Directors,
                    Description = MovieCurrent.Description,
                    Video = MovieCurrent.Video,
                    Likes = (int)MovieCurrent.Likes,
                    IMDB = (double)MovieCurrent.IMDB,
                    NumberOfViews = (int)MovieCurrent.NumberOfViews,
                    Time = MovieCurrent.Time,
                };


                using (var db = new MediasManangementEntities())
                {
                    var level = db.Medias.Find(Movie.Id);
                    Level = level;
                }

                check = 4;
            };



            UserLevelList.CurrentChanged += (_, e) =>
            {
                var LevelCurrent = UserLevelList.CurrentItem as Level;
                if (LevelCurrent == null)
                    return;

                Level = new Media
                {
                    Lvl = LevelCurrent.Id,
                };
            };
        }

        private void DeleteMovie(object obj)
        {
            using (var db = new MediasManangementEntities())
            {

                db.Likes.RemoveRange(db.Likes.Where(l => l.IdMedia == Movie.Id));
                db.View_History.RemoveRange(db.View_History.Where(v => v.IdMedia == Movie.Id));
                db.Medias.Remove(db.Medias.FirstOrDefault(m => m.Id == Movie.Id));
                db.My_Lists.RemoveRange(db.My_Lists.Where(m => m.IdMedia == Movie.Id));

                //db.SaveChanges();

                var MovieDel = db.Movies.FirstOrDefault(u => u.Id == Movie.Id);
                if (MovieDel == null)
                    return;
                db.Movies.Remove(MovieDel);

                if (db.SaveChanges() > 1)
                {
                    MessageBox.Show("Đã xóa phim!");
                    if (MovieList.IsAddingNew)
                    {
                        MovieList.CancelNew();
                        NewMovieList.CancelNew();
                    }

                    MovieCustomModel del = MovieList.CurrentItem as MovieCustomModel;
                    if (check == 2)
                    {
                        del = NewMovieList.CurrentItem as MovieCustomModel;
                    }
                    else if (check == 3)
                    {
                        del = MostLikeMovieList.CurrentItem as MovieCustomModel;
                    }
                    else if (check == 4)
                    {
                        del = MostViewMovieList.CurrentItem as MovieCustomModel;
                    }
                    deteleALL(del.Id);
                }
                else
                {
                    MessageBox.Show("Xóa phim không thành công!");
                }
            }
        }
        private void deteleALL(int delId)
        {
            int index = IndexOfItem(MovieList, delId);
            if (index > -1)
            {
                MovieList.RemoveAt(index);
            }
            index = IndexOfItem(NewMovieList, delId);
            if (index > -1)
            {
                NewMovieList.RemoveAt(index);
            }
            index = IndexOfItem(MostLikeMovieList, delId);
            if (index > -1)
            {
                MostLikeMovieList.RemoveAt(index);
            }
            index = IndexOfItem(MostViewMovieList, delId);
            if (index > -1)
            {
                MostViewMovieList.RemoveAt(index);
            }
        }
        private void AddMovie(object obj)
        {
            if (Movie.Video == "" || Movie.Poster == "")
            {
                MessageBox.Show("Hãy thêm Video và Poster cho phim!");
                return;
            }

            if (!File.Exists(Movie.Poster) || !File.Exists(Movie.Video))
            {
                MessageBox.Show("Không tìm thấy Poster hoặc Video trong thư mục của bạn!");
                return;
            }

            StringBuilder url = new StringBuilder(AppDomain.CurrentDomain.BaseDirectory + "Images" + Path.DirectorySeparatorChar);
            url.Append("Movie" + Path.DirectorySeparatorChar + "Poster" + Path.DirectorySeparatorChar);

            var tailFile = Movie.Poster.Substring(Movie.Poster.LastIndexOf("."));
            var newFileNamePoster = string.Format(@"{0}{1}", Guid.NewGuid(), tailFile);

            var newPathPoster = url.Append(newFileNamePoster);
            File.Copy(Movie.Poster, newPathPoster.ToString());


            url = new StringBuilder(AppDomain.CurrentDomain.BaseDirectory + "Images" + Path.DirectorySeparatorChar);
            url.Append("Movie" + Path.DirectorySeparatorChar + "Video" + Path.DirectorySeparatorChar);
            var newFileNameVideo = string.Format(@"{0}.mp4", Guid.NewGuid());
            var newPathVideo = url.Append(newFileNameVideo);
            File.Copy(Movie.Video, newPathVideo.ToString());

            Random rd = new Random();
            using (var db = new MediasManangementEntities())
            {
                int newID;
                do
                {
                    newID = rd.Next(1, 1000);
                } while (db.Movies.SingleOrDefault(p => p.Id == newID) != null);

                var newMedia = new Media
                {
                    Id = newID,
                    IdCategory = 2,
                    Lvl = Level.Lvl,
                };
                db.Medias.Add(newMedia);

                var Cat = CategoryList.CurrentItem as Movie_Category;
                var NewMovie = new Movie
                {
                    Id = newID,
                    Name = Movie.Name,
                    Poster = newFileNamePoster,
                    IdCategory = Cat.Id,
                    Nation = Movie.Nation,
                    Age = Movie.Age,
                    Season = Movie.Season,
                    Directors = Movie.Directors,
                    Description = Movie.Description,
                    Video = newFileNameVideo,
                    Likes = 0,
                    IMDB = Movie.IMDB,
                    NumberOfViews = 0,
                    Time = Movie.Time,
                };

                db.Movies.Add(NewMovie);
                if (db.SaveChanges() > 1)
                {
                    MessageBox.Show("Thêm phim thành công!");

                    MovieList.AddNewItem(NewMovie);

                    NewMovieList.AddNewItem(NewMovie);
                }
                else
                {
                    MessageBox.Show("Thêm phim không thành công!");
                }
            }
        }

        public int IndexOfItem( ListCollectionView collectionView, int Id)
        {
            int result = 0;

            foreach (MovieCustomModel i in collectionView)
            {
                if (i.Id == Id)
                    return result;
                result++;
            }

            return -1;
        }
        private void UpdateMovie(object obj)
        {
            var cat = CategoryList.CurrentItem as Movie_Category;
            string name;
            int Idphim = Movie.Id;
            using (var db = new MediasManangementEntities())
            {

                var movieUpdate = db.Movies.FirstOrDefault(u => u.Id == Movie.Id);
                name = movieUpdate.Name;

                movieUpdate.Name = Movie.Name;
                movieUpdate.Nation = Movie.Nation;
                movieUpdate.Age = Movie.Age;
                movieUpdate.Season = Movie.Season;
                movieUpdate.IdCategory = cat.Id;
                movieUpdate.IMDB = Movie.IMDB;
                movieUpdate.Directors = Movie.Directors;
                movieUpdate.Description = Movie.Description;
                if(movieUpdate.Video != Movie.Video)
                {
                    if (!File.Exists(Movie.Video))
                    {
                        MessageBox.Show("Không tìm thấy Video trong thư mục!");
                        return;
                    }

                    StringBuilder url = new StringBuilder(AppDomain.CurrentDomain.BaseDirectory + "Images" + Path.DirectorySeparatorChar);
                    url.Append("Movie" + Path.DirectorySeparatorChar + "Video" + Path.DirectorySeparatorChar);
                    var newFileNameVideo = string.Format(@"{0}.mp4", Guid.NewGuid());
                    var newPathVideo = url.Append(newFileNameVideo);
                    File.Copy(Movie.Video, newPathVideo.ToString());

                    movieUpdate.Video = newFileNameVideo;
                }
                if(movieUpdate.Poster != Movie.Poster)
                {
                    if (!File.Exists(Movie.Poster))
                    {
                        MessageBox.Show("Không tìm thấy Poster trong thư mục!");
                        return;
                    }
                    StringBuilder url = new StringBuilder(AppDomain.CurrentDomain.BaseDirectory + "Images" + Path.DirectorySeparatorChar);
                    url.Append("Movie" + Path.DirectorySeparatorChar + "Poster" + Path.DirectorySeparatorChar);

                    var tailFile = Movie.Poster.Substring(Movie.Poster.LastIndexOf("."));
                    var newFileNamePoster = string.Format(@"{0}{1}", Guid.NewGuid(), tailFile);

                    var newPathPoster = url.Append(newFileNamePoster);
                    File.Copy(Movie.Poster, newPathPoster.ToString());

                    movieUpdate.Poster = newFileNamePoster;
                }
                
                

                var LevelUpdate = db.Medias.FirstOrDefault(u => u.Id == Movie.Id);
                LevelUpdate.Lvl = Level.Lvl;


                if (db.SaveChanges() > 0)
                {
                    MessageBox.Show("Cập nhật thành công");

                    var MovieCur = MovieList.CurrentItem as MovieCustomModel;
                    if(MovieCur.Name != name)
                    {
                        MovieCur = NewMovieList.CurrentItem as MovieCustomModel;
                    }

                    if (MovieCur.Name != name)
                    {
                        MovieCur = MostLikeMovieList.CurrentItem as MovieCustomModel;
                    }

                    if (MovieCur.Name != name)
                    {
                        MovieCur = MostViewMovieList.CurrentItem as MovieCustomModel;
                    }

                    MovieCur.Name = Movie.Name;
                    MovieCur.Nation = Movie.Nation;
                    MovieCur.Age = Movie.Age;
                    MovieCur.Season = Movie.Season;
                    MovieCur.IdCategory = cat.Id;
                    MovieCur.IMDB = Movie.IMDB;
                    MovieCur.Directors = Movie.Directors;
                    MovieCur.Description = Movie.Description;
                    MovieCur.Video = Movie.Video;
                    MovieCur.Poster = Movie.Poster;

                    UpdateALL(Movie, cat.Id);


                    var LevelCur = UserLevelList.CurrentItem as Level;
                    LevelCur.Id = (int)Level.Lvl;

                }
                else
                {
                    MessageBox.Show("Không có thay đổi hoặc cập nhật thất bại");
                }

                

            }
        }


        private void UpdateALL(MovieCustomModel Movie, int catid)
        {
            int index = IndexOfItem(MovieList, Movie.Id);
            if (index > -1)
            {
                var updateItem = MovieList.GetItemAt(index) as MovieCustomModel;
                updateItem.Name = Movie.Name;
                updateItem.Nation = Movie.Nation;
                updateItem.Age = Movie.Age;
                updateItem.Season = Movie.Season;
                updateItem.IdCategory = catid;
                updateItem.IMDB = Movie.IMDB;
                updateItem.Directors = Movie.Directors;
                updateItem.Description = Movie.Description;
                updateItem.Video = Movie.Video;
                updateItem.Poster = Movie.Poster;
            }
            index = IndexOfItem(NewMovieList, Movie.Id);
            if (index > -1)
            {
                var updateItem = NewMovieList.GetItemAt(index) as MovieCustomModel;
                updateItem.Name = Movie.Name;
                updateItem.Nation = Movie.Nation;
                updateItem.Age = Movie.Age;
                updateItem.Season = Movie.Season;
                updateItem.IdCategory = catid;
                updateItem.IMDB = Movie.IMDB;
                updateItem.Directors = Movie.Directors;
                updateItem.Description = Movie.Description;
                updateItem.Video = Movie.Video;
                updateItem.Poster = Movie.Poster;
            }
            index = IndexOfItem(MostLikeMovieList, Movie.Id);
            if (index > -1)
            {
                var updateItem = MostLikeMovieList.GetItemAt(index) as MovieCustomModel;
                updateItem.Name = Movie.Name;
                updateItem.Nation = Movie.Nation;
                updateItem.Age = Movie.Age;
                updateItem.Season = Movie.Season;
                updateItem.IdCategory = catid;
                updateItem.IMDB = Movie.IMDB;
                updateItem.Directors = Movie.Directors;
                updateItem.Description = Movie.Description;
                updateItem.Video = Movie.Video;
                updateItem.Poster = Movie.Poster;
            }
            index = IndexOfItem(MostViewMovieList, Movie.Id);
            if (index > -1)
            {
                var updateItem = MostViewMovieList.GetItemAt(index) as MovieCustomModel;
                updateItem.Name = Movie.Name;
                updateItem.Nation = Movie.Nation;
                updateItem.Age = Movie.Age;
                updateItem.Season = Movie.Season;
                updateItem.IdCategory = catid;
                updateItem.IMDB = Movie.IMDB;
                updateItem.Directors = Movie.Directors;
                updateItem.Description = Movie.Description;
                updateItem.Video = Movie.Video;
                updateItem.Poster = Movie.Poster;
            }
        }



        const string GOOGLE_CUSTOM_SEARCH_KEY = "AIzaSyB5GeTPTkFtCgNGX7EXY1CnQc6R9D7yjYU";
        const string GOOGLE_CUSTOM_CX_IMDB = "b1968641cfadcf950";
        private void UpdateIMDBrating(object obj)
        {
            string movies = Movie.Name;
            double imdbRating = 0.0;
            string imdbRatingCount = "";
            string query = String.Format("https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}", GOOGLE_CUSTOM_SEARCH_KEY, GOOGLE_CUSTOM_CX_IMDB, movies);
            JObject response = JObject.Parse(new System.Net.WebClient().DownloadString(query));

            HtmlNodeCollection node = null;
            HtmlDocument document = null;
            int dem = 0;
            do
            {
                var link = response.SelectToken("items[0].link");
                if (link == null)
                {
                    MessageBox.Show("Không tìm thấy phim trên IMDB. Hãy thử lại lần sau");
                    return;
                }

                string imdbLink = link.ToString();
                imdbLink = imdbLink.Substring(0, imdbLink.LastIndexOf("/"));
                HtmlWeb web = new HtmlWeb();
                document = web.Load(imdbLink);
                node = document.DocumentNode.SelectNodes("//span[contains(@class, 'AggregateRatingButton__RatingScore-sc-1ll29m0-1 iTLWoV')]");
                dem++;

                if(dem == 5)
                {
                    MessageBox.Show("Hãy thử đổi tên phim (tên tiếng anh) và thử lại!");
                    return;
                }
            } while (node == null);

            HtmlNode[] nodes = node.ToArray();
            imdbRating = double.Parse(nodes[0].InnerHtml.ToString());
            nodes = document.DocumentNode.SelectNodes("//div[contains(@class, 'AggregateRatingButton__TotalRatingAmount-sc-1ll29m0-3 jkCVKJ')]").ToArray();
            imdbRatingCount = nodes[0].InnerHtml.ToString();

            nodes = document.DocumentNode.SelectNodes("//h1[contains(@data-testid, 'hero-title-block__title')]").ToArray();
            string nameMovie = nodes[0].InnerText.ToString();

            nodes = document.DocumentNode.SelectNodes("//span[contains(@class, 'TitleBlockMetaData__ListItemText-sc-12ein40-2 jedhex')]").ToArray();
            string namPhatHanh = nodes[0].InnerText.ToString();

            nodes = document.DocumentNode.SelectNodes("//a[contains(@class, 'ipc-metadata-list-item__list-content-item ipc-metadata-list-item__list-content-item--link')]").ToArray();
            string directorName = nodes[0].InnerText.ToString();


            nodes = document.DocumentNode.SelectNodes("//a[contains(@class, 'ipc-link ipc-link--baseAlt ipc-link--inherit-color TitleBlockMetaData__StyledTextLink-sc-12ein40-1 rgaOW')]").ToArray();
            string nhan = nodes[1].InnerText.ToString();

            nodes = document.DocumentNode.SelectNodes("//li[contains(@class, 'ipc-inline-list__item')]").ToArray();
            string duration = nodes[2].InnerText.ToString();

            nodes = document.DocumentNode.SelectNodes("//li[contains(@data-testid, 'title-details-origin')]").ToArray();
            var len = nodes.Length;
            string quocgia = nodes[0].InnerText.ToString().Replace("Countries of origin", "");
            quocgia = nodes[0].InnerText.ToString().Replace("Country of origin", "");

            int age = 0;
            if (nhan == "R")
            {
                nhan += " (trên 17 tuổi)";
                age = 18;
            }
            if(nhan == "PG-13")
            {
                nhan += " (trên 12 tuổi)";
                age = 13;
            }
            if (nhan == "PG")
            {
                nhan += " (trên 9 tuổi)";
                age = 10;
            }
            if (nhan == "C13")
            {
                nhan += " (trên 18 tuổi)";
                age = 14;
            }
            if (nhan == "C18")
            {
                nhan += " (trên 18 tuổi)";
                age = 19;
            }
            //Placing the result in the rating text box
            var result = MessageBox.Show("Phim: " + nameMovie + "\nNăm phát hành: " + namPhatHanh + "\nĐạo diễn: "+ directorName + "\nQuốc gia: " + quocgia + "\nĐiểm IMDB: " + imdbRating + "\nTổng số đánh giá: " + imdbRatingCount + "\nNhãn: " + nhan + "\nThời lượng: "+ duration + "\nXác nhận cập nhật các thông tin trên ?", "Xác nhận phim", 
                                        MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                Movie.Age = age;
                Movie.Directors = directorName;
                Movie.IMDB = imdbRating;
                Movie.Time = duration;
                Movie.Nation = quocgia;
            }
        }

    }
}
