using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.Stores
{
    public class UserStore
    {
        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            set => _currentUser = value;
        }
    }
}