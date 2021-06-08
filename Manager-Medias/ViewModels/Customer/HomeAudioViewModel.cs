using Manager_Medias.Commands;
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
        public int Level => (int)_userStore.CurrentUser.Level;

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
            CmdToDetailAudio = new RelayCommand<object>(ToDetailAudio, (object o) =>
            {
                Audio audio = o as Audio;
                if (audio != null && Level >= audio.Media.Lvl)
                {
                    return true;
                }
                return false;
            });
        }

        private void ToDetailAudio(object obj)
        {
            Audio audio = obj as Audio;
            //chuyển trang
            _navigationStore.ContentViewModel = new DetailAudioViewModel(audio.Id);
        }

        private void LoadMovie()
        {
            using (var db = new MediasManangementEntities())
            {
                CatAudioList = new ObservableCollection<Audio_Category>(
                    db.Audio_Categories.Include("Audios")
                                    .Include("Audios.Media")
                                    .Include("Audios.Media.Level")
                                    .ToList());
            }

            var a = CatAudioList[0].Audios.ElementAt(0).Media.Id;
        }
    }
}