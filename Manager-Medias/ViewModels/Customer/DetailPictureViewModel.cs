using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Manager_Medias.ViewModels.Customer
{
    public class DetailPictureViewModel:BaseViewModel
    {
        public static readonly DependencyProperty PictureListProperty;
        static DetailPictureViewModel()
        {
            PictureListProperty = DependencyProperty.Register("PictureList", typeof(ObservableCollection<Album>), typeof(DetailPictureViewModel));
        }
        public ObservableCollection<Album> PictureList
        {
            get => (ObservableCollection<Album>)GetValue(PictureListProperty);
            set => SetValue(PictureListProperty, value);
        }

        public DetailPictureViewModel()
        {
            loaded();
        }

        private void loaded()
        {
            using (var db = new MediasManangementEntities())
            {

                PictureList = new ObservableCollection<Album>(db.Albums.ToList());
            }
        }
    }
}
