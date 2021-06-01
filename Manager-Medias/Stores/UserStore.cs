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
        #region Property

        private string _email;

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnCurrentUserChanged();
            }
        }

        public User CurrentUser
        {
            get
            {
                using (var db = new MediasManangementEntities())
                {
                    return db.Users.Single(u => u.Email == _email);
                }
            }
        }

        public Profile CurrentProfile
        {
            get
            {
                using (var db = new MediasManangementEntities())
                {
                    return db.Users.Single(u => u.Email == _email)
                    .Profiles.Single(p => p.Status == 1);
                }
            }
        }

        public string PathAvatar
        {
            get
            {
                using (var db = new MediasManangementEntities())
                {
                    return db.Users.Single(u => u.Email == _email)
                    .Profiles.Single(p => p.Status == 1).Avatar;
                }
            }
        }

        public ObservableCollection<Profile> Profiles
        {
            get
            {
                if (_email != null)
                {
                    using (var db = new MediasManangementEntities())
                    {
                        return new ObservableCollection<Profile>(db.Users.Single(u => u.Email == _email).Profiles);
                    }
                }

                return null;
            }
        }

        #endregion Property

        public event Action CurrentUserChanged;

        public UserStore(User user)
        {
            Email = user.Email;
        }

        public void OnCurrentUserChanged()
        {
            CurrentUserChanged?.Invoke();
        }
    }
}