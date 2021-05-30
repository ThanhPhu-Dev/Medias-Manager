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

namespace Manager_Medias.ViewModels.Customer
{
    public class FavoriteListViewModel : BaseViewModel
    {
        private readonly UserStore _userStore;

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

        public FavoriteListViewModel(UserStore userStore)
        {
            _userStore = userStore;
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
                            media.MediaType = "Movie";
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
                            media.MediaType = "Album";
                            break;

                        case "âm thanh":
                            media.ID = likes.Id;
                            media.MediaID = likes.Media.Id;
                            media.Name = likes.Media.Audio.Name;
                            media.Image = likes.Media.Audio.Image;
                            media.Date = (DateTime)likes.Date;
                            media.Time = likes.Media.Audio.Time;
                            media.MediaType = "Audio";

                            break;

                        default:
                            break;
                    }
                    MediaList.Add(media);
                });

                PlayList = new ListCollectionView(MediaList);
            }
        }
    }
}