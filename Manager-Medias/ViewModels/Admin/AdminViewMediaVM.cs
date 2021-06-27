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

namespace Manager_Medias.ViewModels.Admin
{
    class AdminViewMediaVM : DependencyObject
    {
        public static readonly DependencyProperty MovieListProperty;
        public static readonly DependencyProperty MovieProperty;

        public static readonly DependencyProperty CategoryListProperty;

        public static readonly DependencyProperty UserLevelListProperty;
        public static readonly DependencyProperty UserLevelProperty;


        public ICommand CmdAddMovie { get; }
        public ICommand CmdUpdateIMDBrating { get;  }

        public ICommand CmdUpdateMovie { get; }
        static AdminViewMediaVM()
        {

            MovieListProperty = DependencyProperty.Register("MovieList",
                                typeof(ListCollectionView), typeof(AdminViewMediaVM));

            MovieProperty = DependencyProperty.Register("Movie",
                            typeof(Movie), typeof(AdminViewMediaVM));


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

        public Movie Movie
        {
            get => (Movie)GetValue(MovieProperty);
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

        public AdminViewMediaVM()
        {
            CmdAddMovie = new RelayCommand<object>(AddMovie);
            CmdUpdateIMDBrating = new RelayCommand<object>(UpdateIMDBrating);
            CmdUpdateMovie = new RelayCommand<object>(UpdateMovie);

            using (var db = new MediasManangementEntities())
            {
                MovieList = new ListCollectionView(db.Movies.ToList());

                CategoryList = new ListCollectionView(db.Movie_Categories.ToList());

                UserLevelList = new ListCollectionView(db.Levels.ToList());
            }

            MovieList.CurrentChanged += (_, e) =>
            {
                var MovieCurrent = MovieList.CurrentItem as Movie;
                if (MovieCurrent == null)
                    return;

                Movie = new Movie
                {
                    Id = MovieCurrent.Id,
                    Name = MovieCurrent.Name,
                    Poster = MovieCurrent.Poster,
                    IdCategory = MovieCurrent.IdCategory,
                    Nation = MovieCurrent.Nation,
                    Age = MovieCurrent.Age,
                    Season = MovieCurrent.Season,
                    Directors = MovieCurrent.Directors,
                    Description = MovieCurrent.Description,
                    Video = MovieCurrent.Video,
                    Likes = MovieCurrent.Likes,
                    IMDB = MovieCurrent.IMDB,
                    NumberOfViews = MovieCurrent.NumberOfViews,
                    Time = MovieCurrent.Time,
                };
               

                using (var db = new MediasManangementEntities())
                {
                    var level = db.Medias.Find(Movie.Id);
                    Level = level;
                }
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

        private void AddMovie(object obj)
        {
            if (Movie.Video == "" || Movie.Poster == "")
            {
                MessageBox.Show("Hãy thêm Video và Poster cho phim!");
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
                }
                else
                {
                    MessageBox.Show("Thêm phim không thành công!");
                }
            }
        }


        private void UpdateMovie(object obj)
        {
            var cat = CategoryList.CurrentItem as Movie_Category;
            
            using (var db = new MediasManangementEntities())
            {
                var movieUpdate = db.Movies.FirstOrDefault(u => u.Id == Movie.Id);
                movieUpdate.Name = Movie.Name;
                movieUpdate.Nation = Movie.Nation;
                movieUpdate.Age = Movie.Age;
                movieUpdate.Season = Movie.Season;
                movieUpdate.IdCategory = cat.Id;
                movieUpdate.IMDB = Movie.IMDB;
                movieUpdate.Directors = Movie.Directors;
                movieUpdate.Description = Movie.Description;

                var LevelUpdate = db.Medias.FirstOrDefault(u => u.Id == Movie.Id);
                LevelUpdate.Lvl = Level.Lvl;


                if (db.SaveChanges() > 0)
                {
                    MessageBox.Show("Cập nhật thành công");

                    var MovieCur = MovieList.CurrentItem as Movie;
                    MovieCur.Name = Movie.Name;
                    MovieCur.Nation = Movie.Name;
                    MovieCur.Age = Movie.Age;
                    MovieCur.Season = Movie.Season;
                    MovieCur.IdCategory = cat.Id;
                    MovieCur.IMDB = Movie.IMDB;
                    MovieCur.Directors = Movie.Directors;
                    MovieCur.Description = Movie.Description;

                    var LevelCur = UserLevelList.CurrentItem as Level;
                    LevelCur.Id = (int)Level.Lvl;

                }
                else
                {
                    MessageBox.Show("Không có thay đổi hoặc cập nhật thất bại");
                }

                

            }
        }


        const string GOOGLE_CUSTOM_SEARCH_KEY = "AIzaSyB5GeTPTkFtCgNGX7EXY1CnQc6R9D7yjYU";
        const string GOOGLE_CUSTOM_CX_IMDB = "b1968641cfadcf950";
        private void UpdateIMDBrating(object obj)
        {
            string movies = Movie.Name;
            double imdbRating = 0.0;
            int imdbRatingCount = 0;
            string query = String.Format("https://www.googleapis.com/customsearch/v1?key={0}&cx={1}&q={2}", GOOGLE_CUSTOM_SEARCH_KEY, GOOGLE_CUSTOM_CX_IMDB, movies);
            JObject response = JObject.Parse(new System.Net.WebClient().DownloadString(query));

            HtmlNodeCollection node = null;
            HtmlDocument document = null;
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
                node = document.DocumentNode.SelectNodes("//span[contains(@itemprop, 'ratingValue')]");
            } while (node == null);


            HtmlNode[] nodes = node.ToArray();
            imdbRating = double.Parse(nodes[0].InnerHtml.ToString());
            nodes = document.DocumentNode.SelectNodes("//span[contains(@itemprop, 'ratingCount')]").ToArray();
            imdbRatingCount = int.Parse(nodes[0].InnerHtml.ToString().Replace(",", string.Empty));
            nodes = document.DocumentNode.SelectNodes("//h1[@class]").ToArray();
            string nameMovie = nodes[0].InnerText.ToString().Replace("&nbsp;", "\nNăm phát hành: ");
            nodes = document.DocumentNode.SelectNodes("//div[contains(@class, 'credit_summary_item')]").ToArray();
            string directorName = nodes[0].InnerText.ToString();
            directorName = directorName.Substring(directorName.LastIndexOf("\n") + 1).Replace("    ", "");
            nodes = document.DocumentNode.SelectNodes("//div[contains(@class, 'subtext')]").ToArray();
            string info = nodes[0].InnerText.ToString().Replace(" ", "").Replace("\n", "");
            string nhan = info.Substring(0, info.IndexOf("|"));
            string duration = "";
            if (info.Split('|').Length - 1 == 3)
            {
                duration = info.Substring(info.IndexOf("|") + 1, 7);
            }
            else
            {
                duration = nhan;
                nhan = "Không có";
            }
            if(nhan == "R")
            {
                nhan += " (trên 17 tuổi)";
            }
            if(nhan == "PG-13")
            {
                nhan += " (trên 12 tuổi)";
            }
            if (nhan == "PG")
            {
                nhan += " (trên 9 tuổi)";
            }
            //Placing the result in the rating text box
            var result = MessageBox.Show("Phim: " + nameMovie + "\nĐạo diễn: "+ directorName + "\nĐiểm IMDB: " + imdbRating + "\nTổng số đánh giá: " + imdbRatingCount + "\nNhãn: " + nhan + "\nThời lượng: "+ duration + "\nXác nhận cập nhật các thông tin trên ?", "Xác nhận phim", 
                                        MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Movie.Directors = directorName;
                Movie.IMDB = imdbRating;
                Movie.Time = duration;

                if (nhan == "R")
                {
                    Movie.Age = 18;
                }
                if (nhan == "PG-13")
                {
                    Movie.Age = 13;
                }
                if (nhan == "PG")
                {
                    Movie.Age = 10;
                }
                else
                {
                    Movie.Age = 0;
                }

            }


        }
    }
}
