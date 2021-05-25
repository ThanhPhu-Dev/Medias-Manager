using Manager_Medias.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.ViewModels.Customer
{
    public class MainAccountView : BaseViewModel
    {
        private readonly UserStore _userStore;

        public MainAccountView(UserStore userStore)
        {
            _userStore = userStore;
        }
    }
}