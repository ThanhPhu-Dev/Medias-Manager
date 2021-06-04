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
    public class HomePictureViewModel:BaseViewModel
    {
        public static readonly DependencyProperty AlbumProperty;
        public ICommand CmdToDetailMovie { get; set; }

        static HomePictureViewModel()
        {
            AlbumProperty = DependencyProperty.Register("AlbumList", typeof(ObservableCollection<Album>), typeof(HomePictureViewModel));
        }
        public ObservableCollection<Album> AlbumList
        {
            get => (ObservableCollection<Album>)GetValue(AlbumProperty);
            set => SetValue(AlbumProperty, value);
        }

        public HomePictureViewModel()
        {
            //gọi hàm load giao diện
            LoadMovie();

            //command
            CmdToDetailMovie = new RelayCommand<object>(ToDetailMovie);
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
    }
}
