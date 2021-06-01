using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Manager_Medias.ViewModels.Admin
{
    class AdminViewVM : DependencyObject
    {
        public static readonly DependencyProperty UserListProperty;
        public static readonly DependencyProperty UserProperty;

        public static readonly DependencyProperty ProfileListProperty;
        public static readonly DependencyProperty ProfileProperty;

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
                    Password = UserCurrent.Password,
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
    } 
}
