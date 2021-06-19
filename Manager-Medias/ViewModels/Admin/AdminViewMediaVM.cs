using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Manager_Medias.ViewModels.Admin
{
    class AdminViewMediaVM : DependencyObject
    {
        public static readonly DependencyProperty MovieListProperty;
        public static readonly DependencyProperty MovieProperty;


        static AdminViewMediaVM()
        {

            MovieListProperty = DependencyProperty.Register("MovieList",
                                typeof(ListCollectionView), typeof(AdminViewMediaVM));

            MovieProperty = DependencyProperty.Register("Movie",
                            typeof(Movie), typeof(AdminViewMediaVM));

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

        public AdminViewMediaVM()
        {
            using (var db = new MediasManangementEntities())
            {
                MovieList = new ListCollectionView(db.Movies.ToList());
            }
        }
    }
}
