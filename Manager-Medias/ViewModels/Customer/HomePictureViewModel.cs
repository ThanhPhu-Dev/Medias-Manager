using Manager_Medias.Commands;
using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class HomePictureViewModel : BaseViewModel
    {
        public static readonly DependencyProperty AlbumProperty =
            DependencyProperty.Register("AlbumList", typeof(ObservableCollection<Album>), typeof(HomePictureViewModel));
        public ICommand CmdToDetailMovie { get; set; }

        //command like và lưu
        public ICommand CmdLike { get; set; }
        public ICommand CmdSave { get; set; }
        public ICommand CmdSelectionChange { get; set; }
        public int _currentProfileID;
        public int _currentAlbumID;

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
        public bool ToggleButton
        {
            get => _toggleButton;
            set
            {
                _toggleButton = value;
                OnPropertyChanged();
            }
        }

        public int Level => (int)_userStore.CurrentUser.Level;

        public Album AlbumSelectedItem { get => _AlbumSelectedItem; set => _AlbumSelectedItem = value; }

        private Album _AlbumSelectedItem;
        private string _message;
        private bool _checkLike;
        private bool _checkSave;
        private bool _toggleButton;

        public HomePictureViewModel()
        {
            _currentProfileID = _userStore.CurrentProfile.Id;

            //gọi hàm load giao diện
            using (var db = new MediasManangementEntities())
            {
                AlbumList = new ObservableCollection<Album>(db.Albums.Include("Album_Details")
                                                                     .Include("Media").ToList());
            }

            //gán 2 nút like save Isnable
            ToggleButton = false;

            //command
            CmdLike = new RelayCommand<object>(Likemt);
            CmdSave = new RelayCommand<object>(Savemt);
            CmdSelectionChange = new RelayCommand<object>(SelectionChangemt);
        }

        public HomePictureViewModel(int idAlbum)
        {
            _currentAlbumID = idAlbum;
            _currentProfileID = _userStore.CurrentProfile.Id;

            //gọi hàm load giao diện
            LoadSelectedAlbum();
            LoadAlbum();
            LoadLikeAndSave(idAlbum);

            //command
            CmdLike = new RelayCommand<object>(Likemt);
            CmdSave = new RelayCommand<object>(Savemt);
            CmdSelectionChange = new RelayCommand<object>(SelectionChangemt);
        }

        private void SelectionChangemt(object obj)
        {
            var lb = (ListBox)obj;
            if (lb.SelectedItem != null)
            {
                var selected = (Album)lb.SelectedItem;

                if (Level < selected.Media.Lvl)
                {
                    lb.SelectedIndex = -1;

                    //gán 2 nút like save Isnable
                    ToggleButton = false;
                    //cho 2 nút trở lại như chưa check
                    CheckLike = false;
                    CheckSave = false;
                }
                else
                {
                    LoadLikeAndSave(selected.Id);
                    ToggleButton = true;
                    
                }
            }
        }

        private void LoadSelectedAlbum()
        {
            using (var db = new MediasManangementEntities())
            {
                AlbumSelectedItem = db.Albums.Where(a => a.Id == _currentAlbumID).Single() as Album;
            }
        }

        private void LoadAlbum()
        {
            using (var db = new MediasManangementEntities())
            {
                AlbumList = new ObservableCollection<Album>(db.Albums.Include("Album_Details")
                                                                     .Include("Media").ToList());
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
                    var likeSelect = db.Likes.Where(l => l.IdMedia == mediaId && l.IdProfile == li.IdProfile).Single() as Like;
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
            var lb = (ListBox)obj;

            var album = (Album)lb.SelectedItem;
            if (lb.SelectedIndex < 0)
            {
                return;
            }

            My_List li = new My_List()
            {
                IdProfile = _currentProfileID,
                IdMedia = album.Id,
                //date
            };
            using (var db = new MediasManangementEntities())
            {
                if (CheckSave)
                {
                    var likeSelect = db.My_Lists.Where(l => l.IdMedia == album.Id && l.IdProfile == li.IdProfile).Single() as My_List;
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

        private void LoadLikeAndSave(int albumId)
        {
            using (var db = new MediasManangementEntities())
            {
                var nLike = db.Likes.Where(l => l.IdMedia == albumId && l.IdProfile == _currentProfileID).Count();
                var nSave = db.My_Lists.Where(l => l.IdMedia == albumId && l.IdProfile == _currentProfileID).Count();

                CheckLike = true ? nLike > 0 : false;
                CheckSave = true ? nSave > 0 : false;
            }
        }
    }
}