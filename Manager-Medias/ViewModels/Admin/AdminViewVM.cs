using Manager_Medias.Commands;
using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Admin
{
    class AdminViewVM : DependencyObject
    {
        public static readonly DependencyProperty UserListProperty;
        public static readonly DependencyProperty UserProperty;

        public static readonly DependencyProperty ProfileListProperty;
        public static readonly DependencyProperty ProfileProperty;


        public ICommand CmdAddUser { get; }
        static AdminViewVM()
        {
            UserListProperty = DependencyProperty.Register("UserList",
                                typeof(ListCollectionView), typeof(AdminViewVM));

            ProfileListProperty = DependencyProperty.Register("ProfileList",
                                typeof(ListCollectionView), typeof(AdminViewVM));

            UserProperty = DependencyProperty.Register("User",
                            typeof(User), typeof(AdminViewVM));

            ProfileProperty = DependencyProperty.Register("Profile",
                            typeof(Profile), typeof(AdminViewVM));
        }

        public ListCollectionView UserList
        {
            get => (ListCollectionView)GetValue(UserListProperty);
            set => SetValue(UserListProperty, value);
        }

        public ListCollectionView ProfileList
        {
            get => (ListCollectionView)GetValue(ProfileListProperty);
            set => SetValue(ProfileListProperty, value);
        }

        public User User
        {
            get => (User)GetValue(UserProperty);
            set => SetValue(UserProperty, value);
        }

        public Profile Profile
        {
            get => (Profile)GetValue(ProfileProperty);
            set => SetValue(ProfileProperty, value);
        }

        public AdminViewVM()
        {
            CmdAddUser = new RelayCommand<object>(AddUser);

            using (var db = new MediasManangementEntities())
            {
                UserList = new ListCollectionView(db.Users.ToList());
            }


            UserList.CurrentChanged += (_, e) =>
            {
                var UserCurrent = UserList.CurrentItem as User;
                if (UserCurrent == null)
                    return;
                User = new User
                {
                    Email = UserCurrent.Email,
                    Password = "",
                    Level = UserCurrent.Level,
                    Code = UserCurrent.Code,
                    NumberCard = UserCurrent.NumberCard,
                };

                using (var db = new MediasManangementEntities())
                {
                    ProfileList = new ListCollectionView(db.Profiles.Where(p => p.Email == UserCurrent.Email).ToList());
                }

                ProfileList.CurrentChanged += (obj, e2) =>
                {
                    var currentProfile = ProfileList.CurrentItem as Profile;
                    if (currentProfile == null)
                        return;

                    Profile = new Profile
                    {
                        Name = currentProfile.Name,
                        Status = currentProfile.Status,
                        Avatar = currentProfile.Avatar
                    };
                };

            };
        }


        private void AddUser(object obj)
        {
            using (var db = new MediasManangementEntities())
            {
                var NewUser = new User
                {
                    Email = User.Email,
                    Code = User.Code,
                    NumberCard = User.NumberCard,
                    Level = User.Level,
                    Exp = "0/20",
                    Password = User.Password,
                };

                db.Users.Add(NewUser);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }

                UserList.AddNewItem(NewUser);

                var rd = new Random();
                int newID;
                do
                {
                    newID = rd.Next(1, 1000);
                } while (db.Profiles.SingleOrDefault(p => p.Id == newID) != null);

                var NewProfile = new Profile
                {
                    Name = Profile.Name,
                    Email = User.Email,
                    Id = newID,
                    Avatar = Profile.Avatar,
                    Status = 1,
                };

                db.Profiles.Add(NewProfile);
                
                db.SaveChanges();

                ProfileList.AddNewItem(NewProfile);


            }
        }
    } 
}
