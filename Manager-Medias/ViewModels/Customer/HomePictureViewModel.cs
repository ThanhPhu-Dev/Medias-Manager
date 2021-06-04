using Manager_Medias.Commands;
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
    public class HomePictureViewModel : BaseViewModel
    {
        public static readonly DependencyProperty AlbumProperty;
        public ICommand CmdToDetailMovie { get; set; }
        //command like và lưu
        public ICommand CmdLike { get; set; }
        public ICommand CmdSave { get; set; }
        public ICommand CmdSelectionChange { get; set; }
        public int _currentProfileID;
        public int _currentAlbumID;

        static HomePictureViewModel()
        {
            AlbumProperty = DependencyProperty.Register("AlbumList", typeof(ObservableCollection<Album>), typeof(HomePictureViewModel));
        }
        public ObservableCollection<Album> AlbumList
        {
            get => (ObservableCollection<Album>)GetValue(AlbumProperty);
            set => SetValue(AlbumProperty, value);
        }
        public bool CheckLike
        {
            get => _checkLike;
            set
            {
                _checkLike = value;
                OnPropertyChanged();
            }
        }

        public bool CheckSave
        {
            get => _checkSave;
            set
            {
                _checkSave = value;
                OnPropertyChanged();
            }
        }
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        public Album AlbumSelectedItem { get => _AlbumSelectedItem; set => _AlbumSelectedItem = value; }

        private Album _AlbumSelectedItem;
        private string _message;
        private bool _checkLike;
        private bool _checkSave;
        public HomePictureViewModel(int idAlbum)
        {
            _currentAlbumID = 9;
            _currentProfileID = _userStore.CurrentProfile.Id;
            
            //gọi hàm load giao diện
            LoadMovie();
            LoadLikeAndSave();
            //command
            CmdToDetailMovie = new RelayCommand<object>(ToDetailMovie);

            CmdLike = new RelayCommand<object>(Likemt);
            CmdSave = new RelayCommand<object>(Savemt);
            CmdSelectionChange = new RelayCommand<object>(SelectionChangemt);
        }

        private void SelectionChangemt(object obj)
        {
            LoadLikeAndSave();
        }

        private void ToDetailMovie(object obj)
        {

        }

        private void LoadMovie()
        {
            using (var db = new MediasManangementEntities())
            {
                AlbumSelectedItem = db.Albums.Where(a => a.Id == _currentAlbumID).Single() as Album;
                //cập nhật danh sách bài hát liên quan (chung danh mục) cho UI
                AlbumList = new ObservableCollection<Album>(db.Albums.Include("Album_Details").ToList());
            }
        }

        private void Likemt(object obj)
        {
            int mediaId = (int)obj;
            Like li = new Like()
            {
                IdProfile = _currentProfileID,
                IdMedia = mediaId,
                //date
            };
            using (var db = new MediasManangementEntities())
            {
                if (CheckLike)
                {
                    var likeSelect = db.Likes.Where(l => l.IdMedia == mediaId).Single() as Like;
                    db.Likes.Remove(likeSelect);
                    CheckLike = false;
                    if (db.SaveChanges() > 0)
                    {
                        Message = "Đã xóa khỏi danh sách yêu thích";
                    }
                }
                else
                {
                    db.Likes.Add(li);
                    CheckLike = true;
                    if (db.SaveChanges() > 0)
                    {
                        Message = "Đã thêm vào danh sách yêu thích";
                    }
                }
            }
        }

        private void Savemt(object obj)
        {
            int mediaId = (int)obj;
            My_List li = new My_List()
            {
                IdProfile = _currentProfileID,
                IdMedia = mediaId,
                //date
            };
            using (var db = new MediasManangementEntities())
            {
                if (CheckSave)
                {
                    var likeSelect = db.My_Lists.Where(l => l.IdMedia == mediaId).Single() as My_List;
                    db.My_Lists.Remove(likeSelect);
                    CheckSave = false;
                    if (db.SaveChanges() > 0)
                    {
                        Message = "Đã xóa khỏi danh sách cá nhân";
                    }
                }
                else
                {
                    db.My_Lists.Add(li);
                    CheckSave = true;
                    if (db.SaveChanges() > 0)
                    {
                        Message = "Đã thêm vào danh sách cá nhân";
                    }
                }
            }
        }
        private void LoadLikeAndSave()
        {
            using (var db = new MediasManangementEntities())
            {
                //ktr xem đã like và lưu bài nhạc này chưa
                //chưa có user id
                var nLike = db.Likes.Where(l => l.IdMedia == AlbumSelectedItem.Id && l.IdProfile == _currentProfileID).Count();
                var nSave = db.My_Lists.Where(l => l.IdMedia == AlbumSelectedItem.Id && l.IdProfile == _currentProfileID).Count();

                CheckLike = true ? nLike > 0 : false;
                CheckSave = true ? nSave > 0 : false;
            }
        }
    }
}
