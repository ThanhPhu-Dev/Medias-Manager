using Manager_Medias.Commands;
using Manager_Medias.CustomModels;
using Manager_Medias.Models;
using Manager_Medias.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class MyPlayListViewModel : BaseViewModel
    {
        #region Command

        public ICommand ItemClickCmd { get; }
        public ICommand NavigateDetailAlbum { get; }
        public ICommand NavigateDetailAudio { get; }
        public ICommand NavigateDetailMovie { get; }
        public ICommand RemoveCmd { get; }

        #endregion Command

        #region Binding

        private ListCollectionView _playList;

        public ListCollectionView PlayList
        {
            get => _playList;
            set
            {
                _playList = value;
                OnPropertyChanged();
            }
        }

        #endregion Binding

        public MyPlayListViewModel()
        {
            ItemClickCmd = new RelayCommand<Object>((Object selectedItem) =>
            {
                if (selectedItem != null)
                {
                    PlayList.MoveCurrentTo(selectedItem as MediaCustomModel);
                }
            });

            NavigateDetailAlbum = new NavigateCommand<DetailPictureViewModel>(
                                   new NavigationService<DetailPictureViewModel>(_navigationStore, () =>
                                   new DetailPictureViewModel((PlayList.CurrentItem as MediaCustomModel).MediaID)));

            NavigateDetailAudio = new NavigateCommand<DetailAudioViewModel>(
                                   new NavigationService<DetailAudioViewModel>(_navigationStore, () =>
                                   new DetailAudioViewModel((PlayList.CurrentItem as MediaCustomModel).MediaID)));

            NavigateDetailMovie = new NavigateCommand<DetailMovieViewModel>(
                                   new NavigationService<DetailMovieViewModel>(_navigationStore, () =>
                                   new DetailMovieViewModel((PlayList.CurrentItem as MediaCustomModel).MediaID)));

            RemoveCmd = new RelayCommand<Object>(ActionRemove);

            GetList();
        }

        public void GetList()
        {
            using (var db = new MediasManangementEntities())
            {
                var MyList = db.My_Lists.Where(l =>
                                    l.IdProfile == _userStore.CurrentProfile.Id);
                List<MediaCustomModel> MediaList = new List<MediaCustomModel>();
                MyList.ToList().ForEach(item =>
                {
                    MediaCustomModel media = new MediaCustomModel();
                    switch (item.Media.Media_Categories.Name.ToLower())
                    {
                        case "phim":
                            media.ID = item.Id;
                            media.MediaID = item.Media.Id;
                            media.Name = item.Media.Movy.Name;
                            media.Image = item.Media.Movy.Poster;
                            media.Date = (DateTime)item.Date;
                            media.Time = item.Media.Movy.Time;
                            media.MediaType = "Phim";
                            //media.TimeWatched = item
                            break;

                        case "hình ảnh":
                            string image = string.Empty;
                            if (item.Media.Album.Album_Details.Any())
                            {
                                image = item.Media.Album.Album_Details.FirstOrDefault().Image;
                            }

                            media.MediaID = item.Media.Id;
                            media.Name = item.Media.Album.Name;
                            media.Image = image;
                            media.Date = (DateTime)item.Date;
                            media.Time = null;
                            media.MediaType = "Hình ảnh";
                            break;

                        case "âm nhạc":
                            media.ID = item.Id;
                            media.MediaID = item.Media.Id;
                            media.Name = item.Media.Audio.Name;
                            media.Image = item.Media.Audio.Image;
                            media.Date = (DateTime)item.Date;
                            media.Time = item.Media.Audio.Time;
                            media.MediaType = "Âm nhạc";

                            break;

                        default:
                            break;
                    }
                    MediaList.Add(media);
                });

                PlayList = new ListCollectionView(MediaList);
                PlayList.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
            }
        }

        public void ActionRemove(Object o)
        {
            if (o == null) return;
            var Item = o as MediaCustomModel;

            using (var db = new MediasManangementEntities())
            {
                var item = db.My_Lists.Single(l => l.Id == Item.ID);
                db.My_Lists.Remove(item);
                db.SaveChanges();
            }

            PlayList.Remove(Item);
        }
    }
}