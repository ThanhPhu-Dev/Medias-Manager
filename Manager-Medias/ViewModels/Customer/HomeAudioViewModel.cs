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
    public class HomeAudioViewModel : BaseViewModel
    {
        public static readonly DependencyProperty CatAudioListProperty;
        public ICommand CmdToDetailAudio { get; set; }

        static HomeAudioViewModel()
        {
            CatAudioListProperty = DependencyProperty.Register("CatAudioList", typeof(ObservableCollection<Audio_Category>), typeof(HomeAudioViewModel));
        }

        public ObservableCollection<Audio_Category> CatAudioList
        {
            get => (ObservableCollection<Audio_Category>)GetValue(CatAudioListProperty);
            set => SetValue(CatAudioListProperty, value);
        }

        public HomeAudioViewModel()
        {
            LoadMovie();
            CmdToDetailAudio = new RelayCommand<object>(ToDetailAudio);
        }

        private void ToDetailAudio(object obj)
        {
            var id = (int)obj;
            //chuyển trang
            _navigationStore.ContentViewModel = new DetailAudioViewModel(id);
        }

        private void LoadMovie()
        {
            using (var db = new MediasManangementEntities())
            {
                CatAudioList = new ObservableCollection<Audio_Category>(db.Audio_Categories.Include("Audios").ToList());
            }
        }
    }
}