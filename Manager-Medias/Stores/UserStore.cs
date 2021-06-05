using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
            set
            {
                using (var db = new MediasManangementEntities())
                {
                    var user = db.Users.Single(u => u.Email == _email);
                    var currentActiveProfile = user.Profiles.Single(p => p.Status == 1);

                    var selectedProfile = user.Profiles.Single(p => p.Id == value.Id);
                    currentActiveProfile.Status = 0;
                    selectedProfile.Status = 1;

                    db.SaveChanges();
                }
                OnProfileChanged();
            }
        }

        public string PathAvatar
        {
            get => this.CurrentProfile.Avatar;
            set
            {
                using (var db = new MediasManangementEntities())
                {
                    var profile = db.Users.Single(u => u.Email == _email).Profiles.Single(p => p.Status == 1);
                    profile.Avatar = value;
                    db.SaveChanges();
                }
                OnAvatarChanged();
            }
        }

        public string ProfileName
        {
            get => this.CurrentProfile.Name;
            set
            {
                using (var db = new MediasManangementEntities())
                {
                    var profile = db.Users.Single(u => u.Email == _email).Profiles.Single(p => p.Status == 1);
                    profile.Name = value;
                    db.SaveChanges();
                }
                OnProfileNameChanged();
            }
        }

        public string LevelName
        {
            get
            {
                using (var db = new MediasManangementEntities())
                {
                    return db.Users.Single(u => u.Email == _email).Lvl.Name;
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

        public event Action AvatarChanged;

        public event Action NameChanged;

        public event Action ProfileChanged;

        public event Action LevelChanged;

        public UserStore(User user)
        {
            Email = user.Email;
        }

        public void OnAvatarChanged()
        {
            AvatarChanged?.Invoke();
        }

        public void OnProfileNameChanged()
        {
            NameChanged?.Invoke();
        }

        public void OnProfileChanged()
        {
            ProfileChanged?.Invoke();
        }

        public void OnLevelChanged()
        {
            LevelChanged?.Invoke();
        }
    }
}