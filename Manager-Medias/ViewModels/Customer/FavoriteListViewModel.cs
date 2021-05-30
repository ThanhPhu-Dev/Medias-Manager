using Manager_Medias.Stores;
using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager_Medias.CustomModels;
using System.Windows.Data;
using System.Windows.Input;
using Manager_Medias.Commands;
using Manager_Medias.Services;

namespace Manager_Medias.ViewModels.Customer
{
    public class FavoriteListViewModel : BaseViewModel
    {
        private readonly UserStore _userStore;

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

        public FavoriteListViewModel(UserStore userStore, NavigationStore navigationStore)
        {
            _userStore = userStore;
            _navigationStore = navigationStore;
            ItemClickCmd = new RelayCommand<Object>((Object selectedItem) =>
            {
                if (selectedItem != null)
                {
                    PlayList.MoveCurrentTo(selectedItem as MediaCustomModel);
                }
            });

            NavigateDetailAlbum = new NavigateCommand<DetailPictureViewModel>(
                                   new NavigationService<DetailPictureViewModel>(_navigationStore, () =>
                                   new DetailPictureViewModel(_userStore.CurrentProfile.Id, (PlayList.CurrentItem as MediaCustomModel).MediaID)));

            NavigateDetailAudio = new NavigateCommand<DetailAudioViewModel>(
                                   new NavigationService<DetailAudioViewModel>(_navigationStore, () =>
                                   new DetailAudioViewModel(_userStore.CurrentProfile.Id, (PlayList.CurrentItem as MediaCustomModel).MediaID)));

            NavigateDetailMovie = new NavigateCommand<DetailMovieViewModel>(
                                   new NavigationService<DetailMovieViewModel>(_navigationStore, () =>
                                   new DetailMovieViewModel()));

            RemoveCmd = new RelayCommand<Object>(ActionRemove);

            GetList();
        }

        public void GetList()
        {
            using (var db = new MediasManangementEntities())
            {
                var LikeList = db.Likes.Where(l =>
                                    l.IdProfile == _userStore.CurrentProfile.Id);
                List<MediaCustomModel> MediaList = new List<MediaCustomModel>();
                LikeList.ToList().ForEach(likes =>
                {
                    MediaCustomModel media = new MediaCustomModel();
                    switch (likes.Media.Media_Categories.Name.ToLower())
                    {
                        case "phim":
                            media.ID = likes.Id;
                            media.MediaID = likes.Media.Id;
                            media.Name = likes.Media.Movy.Name;
                            media.Image = likes.Media.Movy.Poster;
                            media.Date = (DateTime)likes.Date;
                            media.Time = likes.Media.Movy.Time;
                            media.MediaType = "Phim";
                            break;

                        case "hình ảnh":
                            string image = string.Empty;
                            if (likes.Media.Album.Album_Details.Any())
                            {
                                image = likes.Media.Album.Album_Details.FirstOrDefault().Image;
                            }

                            media.MediaID = likes.Media.Id;
                            media.Name = likes.Media.Album.Name;
                            media.Image = image;
                            media.Date = (DateTime)likes.Date;
                            media.Time = null;
                            media.MediaType = "Hình ảnh";
                            break;

                        case "âm nhạc":
                            media.ID = likes.Id;
                            media.MediaID = likes.Media.Id;
                            media.Name = likes.Media.Audio.Name;
                            media.Image = likes.Media.Audio.Image;
                            media.Date = (DateTime)likes.Date;
                            media.Time = likes.Media.Audio.Time;
                            media.MediaType = "Âm nhạc";

                            break;

                        default:
                            break;
                    }
                    MediaList.Add(media);
                });

                PlayList = new ListCollectionView(MediaList);
            }
        }

        public void ActionRemove(Object o)
        {
            if (o == null) return;
            var Item = o as MediaCustomModel;

            using (var db = new MediasManangementEntities())
            {
                var likes = db.Likes.Single(l => l.Id == Item.ID);
                db.Likes.Remove(likes);
                db.SaveChanges();
            }

            PlayList.Remove(Item);
        }
    }
}