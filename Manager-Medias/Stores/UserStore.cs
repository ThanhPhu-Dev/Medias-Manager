using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public string PathAvatar
        {
            get => _currentProfile.Avatar;
        }

        public string Email => _currentUser.Email;

        public ObservableCollection<Profile> Profiles
        {
            get
            {
                if (_currentUser != null)
                {
                    using (var db = new MediasManangementEntities())
                    {
                        return new ObservableCollection<Profile>(db.Users.Where(u => u.Email == _currentUser.Email).Single().Profiles);
                    }
                }

                return null;
            }
        }

        public UserStore(User user)
        {
            using (var db = new MediasManangementEntities())
            {
                this._currentUser = user;
                this._currentProfile = db.Users.Where(u => u.Email == user.Email).Single()
                    .Profiles.Where(p => p.Status == 1).Single();
            }
        }
    }
}