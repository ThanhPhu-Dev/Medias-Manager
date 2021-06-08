using Manager_Medias.Commands;
using Manager_Medias.Functions;
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

        //public static readonly DependencyProperty RolesListProperty;
        //public static readonly DependencyProperty RolesProperty;


        public ICommand CmdAddUser { get; }
        static AdminViewVM()
        {
            UserListProperty = DependencyProperty.Register("UserList",
                                typeof(ListCollectionView), typeof(AdminViewVM));

            ProfileListProperty = DependencyProperty.Register("ProfileList",
                                typeof(ListCollectionView), typeof(AdminViewVM));

            //RolesListProperty = DependencyProperty.Register("RolesList",
            //                   typeof(ListCollectionView), typeof(AdminViewVM));

            UserProperty = DependencyProperty.Register("User",
                            typeof(User), typeof(AdminViewVM));

            ProfileProperty = DependencyProperty.Register("Profile",
                            typeof(Profile), typeof(AdminViewVM));

            //RolesProperty = DependencyProperty.Register("Role",
            //                typeof(Role), typeof(AdminViewVM));
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
        //public ListCollectionView RolesList
        //{
        //    get => (ListCollectionView)GetValue(RolesListProperty);
        //    set => SetValue(RolesListProperty, value);
        //}
        //public Role Role
        //{
        //    get => (Role)GetValue(RolesProperty);
        //    set => SetValue(RolesProperty, value);
        //}
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
                UserList = new ListCollectionView(db.Users.Where(u => u.roleId == 1).ToList());
                //RolesList = new ListCollectionView(db.Roles.ToList());
            }

            //RolesList.CurrentChanged += (_obj2, e3) =>
            //{
            //    var RoleCurrent = RolesList.CurrentItem as Role;
            //    if (RoleCurrent == null)
            //        return;
            //    Role = new Role
            //    {
            //        Id = RoleCurrent.Id,
            //        Name = RoleCurrent.Name,
            //    };
            //};
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
                    roleId = UserCurrent.roleId,
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
                var a = db.Users.Find(User.Email);
                
                if (a != null)
                {
                    MessageBox.Show("Email đã tồn tại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }    
                   
                var passWord = HashPassword.Hash(User.Password);
                var NewUser = new User
                {
                    Email = User.Email,
                    NumberCard = User.NumberCard,
                    Level = User.Level,
                    Exp = "0/20",
                    Password = passWord,
                };

                db.Users.Add(NewUser);

                int count = db.SaveChanges();

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

                count = count + db.SaveChanges();
                if(count > 1)
                {
                    MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Thêm người dùng không thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

                ProfileList.AddNewItem(NewProfile);


            }
        }
    } 
}
