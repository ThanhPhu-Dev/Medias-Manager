﻿using Manager_Medias.Models;
using Manager_Medias.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Manager_Medias.ViewModels.Customer
{
    public class HomeAudioViewModel : BaseViewModel
    {
        public static readonly DependencyProperty AudioListProperty;

        private UserStore _user;
        static HomeAudioViewModel()
        {
            AudioListProperty = DependencyProperty.Register("AudioList", typeof(ObservableCollection<List<Audio>>), typeof(HomeAudioViewModel));
        }
        public ObservableCollection<List<Audio>> AudioList
        {
            get => (ObservableCollection<List<Audio>>)GetValue(AudioListProperty);
            set => SetValue(AudioListProperty, value);
        }

        public HomeAudioViewModel()
        {
        }

        public HomeAudioViewModel(UserStore us)
        {
            LoadMovie();
            _user = us;
        }

        private void LoadMovie()
        {
            using (var db = new MediasManangementEntities())
            {
                AudioList = new ObservableCollection<List<Audio>>();
                foreach(var id in db.Audio_Categories.ToList())
                {
                    AudioList.Add(new List<Audio>(db.Audios.Where(p => p.IdCategory == id.Id).ToList()));
                }
            }
        }
    }
}
