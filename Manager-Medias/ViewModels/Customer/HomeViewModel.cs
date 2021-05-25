using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.ViewModels.Customer
{
    public class HomeViewModel : BaseViewModel
    {
        public ObservableCollection<string> MovieGenre { get; set; }
        public ObservableCollection<MovieCarouselModel> CarouselCollection { get; set; }

        public HomeViewModel()
        {
            MovieGenre = new ObservableCollection<string>
            {
                "Hành động", "Viễn tưởng", "18+"
            };

            CarouselCollection = new ObservableCollection<MovieCarouselModel>()
            {
                  new MovieCarouselModel(){Image = "/Images/transitton-4.jpg", Title = "MIME", Description =" Sở hữu"},
                  new MovieCarouselModel(){Image = "/Images/transitton-1.jpg", Title = "DOOM AT YOUR SERVICE", Description ="Một ngày nọ kẻ hủy diệt gõ cửa nhà tôi"},
                  new MovieCarouselModel(){Image = "/Images/transitton-3.jpg", Title = "VIVY: FLUORITE EYE'S SONG", Description =" Câu chuyện VyVy"},
            };
        }
    }
}