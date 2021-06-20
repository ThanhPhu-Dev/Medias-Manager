﻿using Manager_Medias.Commands;
using Manager_Medias.Models;
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
    public class HomeViewModel : BaseViewModel
    {
        public static readonly DependencyProperty CatMovieListProperty = 
            DependencyProperty.Register("CatMovieList",
                typeof(ObservableCollection<Movie_Category>), typeof(HomeViewModel));

        public ICommand CmdToDetailMovie { get; set; }

        public int Level => (int)_userStore.CurrentUser.Level;

        public ObservableCollection<Movie_Category> CatMovieList
        {
            get => (ObservableCollection<Movie_Category>)GetValue(CatMovieListProperty);
            set => SetValue(CatMovieListProperty, value);
        }

        public HomeViewModel()
        {
            LoadMovie();

            CmdToDetailMovie = new RelayCommand<object>(ToDetailMovie, (object o) =>
            {
                Movie movie = o as Movie;
                if (movie != null && Level >= movie.Media.Lvl)
                {
                    return true;
                }
                return false;
            });
        }

        private void ToDetailMovie(object obj)
        {
            Movie movie = obj as Movie;
            if (movie != null)
            {
                _navigationStore.ContentViewModel = new DetailMovieViewModel(movie.Id);
            }
        }

        private async void LoadMovie()
        {
            IsLoading = true;

            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    using (var db = new MediasManangementEntities())
                    {
                        CatMovieList = new ObservableCollection<Movie_Category>(
                           db.Movie_Categories.Include("Movies")
                                               .Include("Movies.Media")
                                               .Include("Movies.Media.Level")
                                               .ToList());
                    }
                });
            }).ContinueWith((task) =>
            {
                IsLoading = false;
            }).ConfigureAwait(false);
        }
    }
}