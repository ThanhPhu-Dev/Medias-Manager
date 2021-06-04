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
        private string _message;
        private bool _checkLike;
        private bool _checkSave;
        public HomePictureViewModel(int idAlbum)
        {
            _currentAlbumID = 1;
            _currentProfileID = _userStore.CurrentProfile.Id;
            //gọi hàm load giao diện
            LoadMovie();

            //command
            CmdToDetailMovie = new RelayCommand<object>(ToDetailMovie);

            CmdLike = new RelayCommand<object>(Likemt);
            CmdSave = new RelayCommand<object>(Savemt);
        }

        private void ToDetailMovie(object obj)
        {

        }

        private void LoadMovie()
        {
            using (var db = new MediasManangementEntities())
            {
                //cập nhật danh sách bài hát liên quan (chung danh mục) cho UI
                AlbumList = new ObservableCollection<Album>(db.Albums.Include("Album_Details").ToList());
            }
        }

        private void Likemt(object obj)
        {
            Like li = new Like()
            {
                IdProfile = _currentProfileID,
                IdMedia = _currentAlbumID,
                //date
            };
            using (var db = new MediasManangementEntities())
            {
                if (CheckLike)
                {
                    var likeSelect = db.Likes.Where(l => l.IdMedia == _currentAlbumID).Single() as Like;
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
            My_List li = new My_List()
            {
                IdProfile = _currentProfileID,
                IdMedia = _currentAlbumID,
                //date
            };
            using (var db = new MediasManangementEntities())
            {
                if (CheckSave)
                {
                    var likeSelect = db.My_Lists.Where(l => l.IdMedia == _currentAlbumID).Single() as My_List;
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
    }
}
