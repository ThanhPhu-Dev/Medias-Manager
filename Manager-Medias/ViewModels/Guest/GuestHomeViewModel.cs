using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.ViewModels.Guest
{
    public class GuestHomeViewModel : BaseViewModel
    {
        public GuestHomeViewModel()
        {
            using (var db = new MediasManangementEntities())
            {
                
            }
        }
    }
}