using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.ViewModels.Customer
{
    public class LayoutViewModel : BaseViewModel
    {
        public BaseViewModel ContentViewModel { get; }

        public LayoutViewModel()
        {
            ContentViewModel = new ProfileViewModel();
        }
    }
}