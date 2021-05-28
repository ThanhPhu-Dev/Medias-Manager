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

        private Profile _currentProfile;

        public Profile CurrentProfile
        {
            get => _currentProfile;
        }

        public string Email => _currentUser.Email;
        //List<Profile> Profiles => _currentUser.Profiles;

        public UserStore(User user)
        {
            using (var db = new MediasManangementEntities())
            {
                this._currentUser = user;
                //this._currentProfile = user.Profiles.Where()
            }
        }
    }
}