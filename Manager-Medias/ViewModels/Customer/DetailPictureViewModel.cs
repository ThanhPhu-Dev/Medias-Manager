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
    public class DetailPictureViewModel:BaseViewModel
    {
        public static readonly DependencyProperty PictureListProperty;
        public int _currentProfileID;
        public int _currentAlbumID;
        //command like và lưu
        public ICommand CmdLike { get; set; }
        public ICommand CmdSave { get; set; }

        static DetailPictureViewModel()
        {
            PictureListProperty = DependencyProperty.Register("PictureList", typeof(ObservableCollection<Album_Detail>), typeof(DetailPictureViewModel));
        }
        public ObservableCollection<Album_Detail> PictureList
        {
            get => (ObservableCollection<Album_Detail>)GetValue(PictureListProperty);
            set => SetValue(PictureListProperty, value);
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
        private bool _checkLike;
        private bool _checkSave;

        public DetailPictureViewModel(int idProfile, int idAlbum)
        {
            _currentAlbumID = idAlbum;
            _currentProfileID = idProfile;
            using (var db = new MediasManangementEntities())
            {
                PictureList = new ObservableCollection<Album_Detail>(db.Album_Details.Where(al => al.IdAlbum == _currentAlbumID).ToList());

                //kiem tra album đã dc like và save chưa
                var nLike = db.Likes.Where(l => l.IdMedia == _currentAlbumID && l.IdProfile == _currentProfileID).Count();
                var nSave = db.My_Lists.Where(l => l.IdMedia == _currentAlbumID && l.IdProfile == _currentProfileID).Count();

                CheckLike = true ? nLike > 0 : false;
                CheckSave = true ? nSave > 0 : false;
            }

            CmdLike = new RelayCommand<object>(Likemt);
            CmdSave = new RelayCommand<object>(Savemt);
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
                        MessageBox.Show("Đã xóa khỏi ds yêu thích");
                    }
                    else
                    {
                        MessageBox.Show("Đã thêm vào ds yêu thích");
                    }
                }
                else
                {
                    db.Likes.Add(li);
                    CheckLike = true;
                    if (db.SaveChanges() > 0)
                    {
                        MessageBox.Show("Đã thêm vào ds yêu thích");
                    }
                    else
                    {
                        MessageBox.Show("Đã xóa khỏi ds yêu thích");
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
                        MessageBox.Show("Đã bỏ lưu");
                    }
                    else
                    {
                        MessageBox.Show("Đã lưu bài hát");
                    }
                }
                else
                {
                    db.My_Lists.Add(li);
                    CheckSave = true;
                    if (db.SaveChanges() > 0)
                    {
                        MessageBox.Show("Đã lưu bài hát");
                    }
                    else
                    {
                        MessageBox.Show("Đã bỏ lưu");
                    }
                }

            }
        }
    }
}
