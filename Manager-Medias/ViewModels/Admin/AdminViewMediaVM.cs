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

namespace Manager_Medias.ViewModels.Admin
{
    class AdminViewMediaVM : DependencyObject
    {
        public static readonly DependencyProperty MovieListProperty;
        public static readonly DependencyProperty MovieProperty;

        public static readonly DependencyProperty CategoryListProperty;


        public ICommand CmdAddMovie { get; }
        public ICommand CmdUpdateIMDBrating { get;  }
        static AdminViewMediaVM()
        {

            MovieListProperty = DependencyProperty.Register("MovieList",
                                typeof(ListCollectionView), typeof(AdminViewMediaVM));

            MovieProperty = DependencyProperty.Register("Movie",
                            typeof(Movie), typeof(AdminViewMediaVM));


            CategoryListProperty = DependencyProperty.Register("CategoryList",
                               typeof(ListCollectionView), typeof(AdminViewMediaVM));
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

        public AdminViewMediaVM()
        {
            CmdAddMovie = new RelayCommand<object>(AddMovie);
            CmdUpdateIMDBrating = new RelayCommand<object>(UpdateIMDBrating);

            using (var db = new MediasManangementEntities())
            {
                MovieList = new ListCollectionView(db.Movies.ToList());

                CategoryList = new ListCollectionView(db.Movie_Categories.ToList());
            }

            MovieList.CurrentChanged += (_, e) =>
            {
                var MovieCurrent = MovieList.CurrentItem as Movie;
                if (MovieCurrent == null)
                    return;

                Movie = MovieCurrent;
            };
        }

        private void AddMovie(object obj)
        {
            using (var db = new MediasManangementEntities())
            {

                var Cat = CategoryList.CurrentItem as Movie_Category;

                Random rd = new Random();
                int newID;
                do
                {
                    newID = rd.Next(1, 1000);
                } while (db.Movies.SingleOrDefault(p => p.Id == newID) != null);

                var NewMovie = new Movie
                {
                    Id = newID,
                    Name = Movie.Name,
                    Poster = Movie.Poster,
                    IdCategory = Cat.Id,
                    Nation = Movie.Nation,
                    Age = Movie.Age,
                    Season = Movie.Season,
                    Directors = Movie.Directors,
                    Description = Movie.Description,
                    Video = Movie.Video,
                    Likes = 0,
                    NumberOfViews = 0,
                    Time = Movie.Time,
                };

                db.Movies.Add(NewMovie);
                db.SaveChanges();
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
            HtmlNodeCollection name = null;
            HtmlDocument document = null;
            do
            {
                var link = response.SelectToken("items[0].link");
                if (link == null)
                {
                    MessageBox.Show("Không tìm thấy phim hoặc phim không có điểm IMDB. Hãy thử lại lần sau");
                    return;
                }

                string imdbLink = link.ToString();
                imdbLink = imdbLink.Substring(0, imdbLink.LastIndexOf("/"));
                HtmlWeb web = new HtmlWeb();
                document = web.Load(imdbLink);
                name = document.DocumentNode.SelectNodes("//h1[@class]");
                node = document.DocumentNode.SelectNodes("//span[contains(@itemprop, 'ratingValue')]");
            } while (node == null);


            HtmlNode[] nodes = node.ToArray();
            imdbRating = double.Parse(nodes[0].InnerHtml.ToString());
            nodes = document.DocumentNode.SelectNodes("//span[contains(@itemprop, 'ratingCount')]").ToArray();
            imdbRatingCount = int.Parse(nodes[0].InnerHtml.ToString().Replace(",", string.Empty));
            nodes = name.ToArray();
            string nameMovie = nodes[0].InnerText.ToString().Replace("&nbsp;", "\nNăm phát hành: ");

            //Placing the result in the rating text box
            MessageBox.Show("Phim: " + nameMovie + "\nĐiểm IMDB: " + imdbRating + "\nTổng số " + imdbRatingCount + " đánh giá.", "Xác nhận phim", 
                MessageBoxButton.YesNo, MessageBoxImage.Question);

        }
    }
}
