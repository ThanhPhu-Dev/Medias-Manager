﻿using Manager_Medias.Commands;
using Manager_Medias.Models;
using Manager_Medias.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class HomeMovieViewModel:BaseViewModel
    {
        public static readonly DependencyProperty MovieListProperty;
        public ICommand CmdToDetailMovie { get; set; }
        private UserStore _user;

        static HomeMovieViewModel()
        {
            MovieListProperty = DependencyProperty.Register("MovieList", typeof(ObservableCollection<Movie>), typeof(HomeMovieViewModel));
        }
        public ObservableCollection<Movie> MovieList
        {
            get => (ObservableCollection<Movie>)GetValue(MovieListProperty);
            set => SetValue(MovieListProperty, value);
        }

        public HomeMovieViewModel(NavigationStore navigationStore, UserStore userStore)
        {
            //user hiện tại
            _user = userStore;

            //gán biến chuyển trang 
            _navigationStore = navigationStore;

            //gọi hàm load giao diện
            LoadMovie();

            //command
            CmdToDetailMovie = new RelayCommand<object>(ToDetailMovie);
        }

        private void ToDetailMovie(object obj)
        {
            

            var id = (int)obj;
            //chuyển trang
            _navigationStore.ContentViewModel = new DetailMovieViewModel(id, _user);
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