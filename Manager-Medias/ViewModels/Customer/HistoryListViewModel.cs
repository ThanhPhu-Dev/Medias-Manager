using Manager_Medias.Commands;
using Manager_Medias.CustomModels;
using Manager_Medias.Models;
using Manager_Medias.Services;
using Manager_Medias.Stores;
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
    public class HistoryListViewModel : BaseViewModel
    {
        private readonly UserStore _userStore;

        #region Command

        public ICommand ItemClickCmd { get; }
        public ICommand NavigateDetailAlbum { get; }
        public ICommand NavigateDetailAudio { get; }
        public ICommand NavigateDetailMovie { get; }

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

        public HistoryListViewModel(UserStore userStore, NavigationStore navigationStore)
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
                                   new DetailMovieViewModel((PlayList.CurrentItem as MediaCustomModel).MediaID)));

            GetList();
        }

        public void GetList()
        {
            using (var db = new MediasManangementEntities())
            {
                var LikeList = db.View_History.Where(l =>
                                    l.IdProfile == _userStore.CurrentProfile.Id);
                List<MediaCustomModel> MediaList = new List<MediaCustomModel>();
                LikeList.ToList().ForEach(history =>
                {
                    MediaCustomModel media = new MediaCustomModel();
                    switch (history.Media.Media_Categories.Name.ToLower())
                    {
                        case "phim":
                            media.ID = history.Id;
                            media.MediaID = history.Media.Id;
                            media.Name = history.Media.Movy.Name;
                            media.Image = history.Media.Movy.Poster;
                            media.Date = (DateTime)history.Date;
                            media.Time = history.Media.Movy.Time;
                            media.MediaType = "Phim";
                            break;

                        case "hình ảnh":
                            string image = string.Empty;
                            if (history.Media.Album.Album_Details.Any())
                            {
                                image = history.Media.Album.Album_Details.FirstOrDefault().Image;
                            }

                            media.MediaID = history.Media.Id;
                            media.Name = history.Media.Album.Name;
                            media.Image = image;
                            media.Date = (DateTime)history.Date;
                            media.Time = null;
                            media.MediaType = "Hình ảnh";
                            break;

                        case "âm nhạc":
                            media.ID = history.Id;
                            media.MediaID = history.Media.Id;
                            media.Name = history.Media.Audio.Name;
                            media.Image = history.Media.Audio.Image;
                            media.Date = (DateTime)history.Date;
                            media.Time = history.Media.Audio.Time;
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
    }
}