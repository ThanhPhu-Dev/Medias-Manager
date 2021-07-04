using Manager_Medias.Commands;
using Manager_Medias.CustomModels;
using Manager_Medias.Functions;
using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
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

        public static readonly DependencyProperty RolesListProperty;
        public static readonly DependencyProperty RolesProperty;


        public ICommand CmdAddUser { get; }
        public ICommand CmdAddProfile { get; }
        public ICommand CmdUpdateUser { get; }
        public ICommand CmdUpdatePassword { get; }
        public ICommand CmdDeleteUser { get; }
        public ICommand CmdDeleteProfile { get; }
        public ICommand CmdUpdateProfile { get; }


        static AdminViewVM()
        {

            UserListProperty = DependencyProperty.Register("UserList",
                                typeof(ListCollectionView), typeof(AdminViewVM));

            ProfileListProperty = DependencyProperty.Register("ProfileList",
                                typeof(ListCollectionView), typeof(AdminViewVM));

            RolesListProperty = DependencyProperty.Register("RolesList",
                               typeof(ListCollectionView), typeof(AdminViewVM));

            UserProperty = DependencyProperty.Register("User",
                            typeof(UserCustomModel), typeof(AdminViewVM));

            ProfileProperty = DependencyProperty.Register("Profile",
                            typeof(ProfileCustomModel), typeof(AdminViewVM));

            RolesProperty = DependencyProperty.Register("Role",
                            typeof(Role), typeof(AdminViewVM));
        }

        public ListCollectionView UserList
        {

            get => (ListCollectionView)GetValue(UserListProperty);
            set => SetValue(UserListProperty, value);
        }


        public string VirtualPassword { get; set; }

        public int UserCount { get; set; }
        public int VipUserCount { get; set; }
        public int ProfileCount { get; set; }
        public int NewUserCount { get; set; }
        public ListCollectionView ProfileList
        {
            get => (ListCollectionView)GetValue(ProfileListProperty);
            set => SetValue(ProfileListProperty, value);
        }
        public ListCollectionView RolesList
        {
            get => (ListCollectionView)GetValue(RolesListProperty);
            set => SetValue(RolesListProperty, value);
        }
        public Role Role
        {
            get => (Role)GetValue(RolesProperty);
            set => SetValue(RolesProperty, value);
        }
        public UserCustomModel User
        {
            get => (UserCustomModel)GetValue(UserProperty);
            set => SetValue(UserProperty, value);
        }

        public ProfileCustomModel Profile
        {
            get => (ProfileCustomModel)GetValue(ProfileProperty);
            set => SetValue(ProfileProperty, value);
        }

        public AdminViewVM()
        {
            VirtualPassword = "";
            CmdAddUser = new RelayCommand<object>(AddUser);
            CmdAddProfile = new RelayCommand<object>(AddProfile);
            CmdUpdateUser = new RelayCommand<object>(UpdateUser);
            CmdUpdatePassword = new RelayCommand<object>(UpdatePassword);
            CmdDeleteUser = new RelayCommand<object>(DeleteUser);
            CmdDeleteProfile = new RelayCommand<object>(DeleteProfile);
            CmdUpdateProfile = new RelayCommand<object>(UpdateProfile);

            using (var db = new MediasManangementEntities())
            {
                var getUser = db.Users.ToList();

                BindingList<UserCustomModel> userCustomList = new BindingList<UserCustomModel>();
                foreach (User u in getUser) 
                {
                    var user = new UserCustomModel
                    {
                        Code = u.Code,
                        Email = u.Email,
                        Exp = u.Exp,
                        Level = u.Level,
                        NumberCard = u.NumberCard,
                        Password = u.Password,
                        roleId = u.roleId,
                        CreateAt = u.CreateAt
                    };

                    userCustomList.Add(user);
                }

                UserList = new ListCollectionView(userCustomList);
                
                RolesList = new ListCollectionView(db.Roles.ToList());

                UserCount = db.Users.Count();
                ProfileCount = db.Profiles.Count();

                DateTime now = DateTime.Today;
                NewUserCount = db.Users.Where(u => u.CreateAt.Value.Month == now.Month).Count();

                VipUserCount = db.Users.Where(u => u.Level == 3).Count();
            }

            RolesList.CurrentChanged += (_obj2, e3) =>
            {
                var RoleCurrent = RolesList.CurrentItem as Role;
                if (RoleCurrent == null)
                    return;
                Role = new Role
                {
                    Id = RoleCurrent.Id,
                    Name = RoleCurrent.Name,
                };
            };
            UserList.CurrentChanged += (_, e) =>
            {             
                var UserCurrent = UserList.CurrentItem as UserCustomModel;
                if (UserCurrent == null)
                    return;
                User = new UserCustomModel
                {
                    Email = UserCurrent.Email,
                    Password = "",
                    Level = UserCurrent.Level,
                    Exp = UserCurrent.Exp,
                    Code = UserCurrent.Code,
                    NumberCard = UserCurrent.NumberCard,
                    roleId = UserCurrent.roleId,
                    CreateAt = UserCurrent.CreateAt
                };

                using (var db = new MediasManangementEntities())
                {
                    var getProfile = new ListCollectionView(db.Profiles.Where(p => p.Email == UserCurrent.Email).ToList());
                    var listPro = new BindingList<ProfileCustomModel>();
                    foreach(Profile p in getProfile)
                    {
                        var get = new ProfileCustomModel
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Status = (int)p.Status,
                            Avatar = p.Avatar
                        };

                        listPro.Add(get);
                    }
                    ProfileList = new ListCollectionView(listPro);
                }

                ProfileList.CurrentChanged += (obj, e2) =>
                {
                    var currentProfile = ProfileList.CurrentItem as ProfileCustomModel;
                    if (currentProfile == null)
                        return;

                    Profile = new ProfileCustomModel
                    {
                        Id = currentProfile.Id,
                        Name = currentProfile.Name,
                        Status = (int)currentProfile.Status,
                        Avatar = currentProfile.Avatar
                    };

                    //MessageBox.Show(Profile.Avatar);
                };

            };
        }
        private void DeleteProfile(object obj)
        {
            using (var db = new MediasManangementEntities())
            {
                var profile = db.Profiles.FirstOrDefault(x => x.Id == Profile.Id);

                db.Likes.RemoveRange(db.Likes.Where(x => x.IdProfile == Profile.Id));
                db.View_History.RemoveRange(db.View_History.Where(x => x.IdProfile == Profile.Id));
                db.Profiles.Remove(profile);

                if (db.SaveChanges() > 0)
                {
                    MessageBox.Show("Đã xóa hồ sơ!");
                    if (ProfileList.IsAddingNew)
                    {
                        ProfileList.CancelNew();
                    }
                    ProfileList.Remove(ProfileList.CurrentItem);
                }
                else
                {
                    MessageBox.Show("Xóa hồ sơ không thành công!");
                }
            }
        }
        private void DeleteUser(object obj)
        {
            using (var db = new MediasManangementEntities())
            {
                var profile = db.Profiles.Where(x => x.Email == User.Email).ToList();
                foreach(var pro in profile)
                {
                    db.Likes.RemoveRange(db.Likes.Where(x => x.IdProfile == pro.Id));
                    db.View_History.RemoveRange(db.View_History.Where(x => x.IdProfile == pro.Id));
                    db.Profiles.Remove(pro);
                }
                db.Payment_History.RemoveRange(db.Payment_History.Where(x => x.Email == User.Email));
                db.SaveChanges();

                var userDel = db.Users.FirstOrDefault(u => u.Email == User.Email);
                if (userDel == null)
                    return;
                db.Users.Remove(userDel);

                if (db.SaveChanges() > 0)
                {
                    MessageBox.Show("Đã xóa người dùng!");
                    if (UserList.IsAddingNew)
                    {
                        UserList.CancelNew();
                    }
                    UserList.Remove(UserList.CurrentItem);
                }
                else
                {
                    MessageBox.Show("Xóa người dùng không thành công!");
                }
            }
        }
        private void UpdatePassword(object obj)
        {
            using (var db = new MediasManangementEntities())
            {
                var UserUpdate = db.Users.FirstOrDefault(u => u.Email == User.Email);     
                UserUpdate.Password = HashPassword.Hash(VirtualPassword);

                if (db.SaveChanges() > 0)
                {
                    MessageBox.Show("Đã thay đổi mật khẩu!");
                }
                else
                {
                    MessageBox.Show("Thay đổi mật khẩu thất bại");
                }

            }
        }

        private void UpdateProfile(object obj)
        {
            if(ProfileList.Count == 0)
            {
                MessageBox.Show("Không có hồ sơ để cập nhật");
                return;
            }
            using (var db = new MediasManangementEntities())
            {
                var ProfileUpdate = db.Profiles.FirstOrDefault(p => p.Id == Profile.Id);
                ProfileUpdate.Name = Profile.Name;
                ProfileUpdate.Status = Profile.Status;
                if (File.Exists(Profile.Avatar))
                {
                    var currentfolder = AppDomain.CurrentDomain.BaseDirectory;
                    string url = currentfolder + "Images\\Profile\\";

                    var tailFile = Profile.Avatar.Substring(Profile.Avatar.LastIndexOf("."));
                    var newFileNameAvt = string.Format(@"{0}{1}", Guid.NewGuid(), tailFile);
                    var newPathAvt = url + newFileNameAvt;
                    File.Copy(Profile.Avatar, newPathAvt);

                    ProfileUpdate.Avatar = newFileNameAvt;
                }
                else
                {
                    ProfileUpdate.Avatar = Profile.Avatar;
                }
               
                if (db.SaveChanges() > 0)
                {
                    MessageBox.Show("Cập nhật hồ sơ thành công");
                }
                else
                {
                    MessageBox.Show("Không có thay đổi hoặc cập nhật thất bại");
                }

                var proCur = ProfileList.CurrentItem as ProfileCustomModel;
                proCur.Name = Profile.Name;
                proCur.Status = Profile.Status;
                proCur.Avatar = Profile.Avatar;
            }
        }
        private void UpdateUser(object obj)
        {
            using (var db = new MediasManangementEntities())
            {
                var UserUpdate = db.Users.FirstOrDefault(u=> u.Email == User.Email);
                UserUpdate.Level = User.Level;
                UserUpdate.NumberCard = User.NumberCard;
                UserUpdate.Exp = User.Exp;
                UserUpdate.roleId = Role.Id;
                

                if (db.SaveChanges() > 0)
                {
                    MessageBox.Show("Cập nhật thành công");

                    var userCur = UserList.CurrentItem as UserCustomModel;
                    userCur.Level = User.Level;
                    userCur.NumberCard = User.NumberCard;
                    userCur.Exp = User.Exp;
                    userCur.roleId = Role.Id;
                    userCur.CreateAt = User.CreateAt;
                }
                else
                {
                    MessageBox.Show("Không có thay đổi hoặc cập nhật thất bại");
                }

            }
        }

        private void AddProfile(object obj)
        {


            using (var db = new MediasManangementEntities())
            {
                var rd = new Random();
                
                int maxId = db.Profiles.Max(p => p.Id);
                int newID = maxId + 1;

                string newFileNameAvt = Profile.Avatar;
                if (File.Exists(Profile.Avatar))
                {
                    var currentfolder = AppDomain.CurrentDomain.BaseDirectory;
                    string url = currentfolder + "Images\\Profile\\";

                    var tailFile = Profile.Avatar.Substring(Profile.Avatar.LastIndexOf("."));
                    newFileNameAvt = string.Format(@"{0}{1}", Guid.NewGuid(), tailFile);
                    var newPathAvt = url + newFileNameAvt;
                    File.Copy(Profile.Avatar, newPathAvt);
                }
                

                var NewProfile = new Profile
                {
                    Name = Profile.Name,
                    Email = User.Email,
                    Id = newID,
                    Avatar = newFileNameAvt,
                    Status = Profile.Status,
                };

                db.Profiles.Add(NewProfile);
                if (db.SaveChanges() > 0)
                {
                    MessageBox.Show("Thêm hồ sơ thành công!");
                    var NewProfileItem = new ProfileCustomModel
                    {
                        Name = Profile.Name,
                        Email = User.Email,
                        Id = newID,
                        Avatar = newFileNameAvt,
                        Status = Profile.Status,
                    };
                    ProfileList.AddNewItem(NewProfileItem);
                }
                else
                {
                    MessageBox.Show("Thêm hồ sơ thất bại!");
                }

                

            }
        }
        private void AddUser(object obj)
        {
            using (var db = new MediasManangementEntities())
            {
                var find = db.Users.Find(User.Email);

                if (find != null)
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
                    Exp = "",
                    Password = passWord,
                    roleId = Role.Id,
                    CreateAt = DateTime.Now
                };

                db.Users.Add(NewUser);
                int count = db.SaveChanges();

                UserCustomModel newU = new UserCustomModel
                {
                    Email = User.Email,
                    NumberCard = User.NumberCard,
                    Level = User.Level,
                    Exp = "",
                    Password = passWord,
                    roleId = Role.Id,
                    CreateAt = DateTime.Now
                };

                UserList.AddNewItem(newU);

                var rd = new Random();
                int maxId = db.Profiles.Max(p => p.Id);
                int newID = maxId + 1;

                string newFileNameAvt = Profile.Avatar;
                if (File.Exists(Profile.Avatar))
                {
                    var currentfolder = AppDomain.CurrentDomain.BaseDirectory;
                    string url = currentfolder + "Images\\Profile\\";

                    var tailFile = Profile.Avatar.Substring(Profile.Avatar.LastIndexOf("."));
                    newFileNameAvt = string.Format(@"{0}{1}", Guid.NewGuid(), tailFile);
                    var newPathAvt = url + newFileNameAvt;
                    File.Copy(Profile.Avatar, newPathAvt);
                }

                var NewProfile = new Profile
                {
                    Name = Profile.Name,
                    Email = User.Email,
                    Id = newID,
                    Avatar = newFileNameAvt,
                    Status = 1,
                };

                db.Profiles.Add(NewProfile);

                count = count + db.SaveChanges();


                if (count > 1)
                {
                    MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                    var NewProfiletem = new ProfileCustomModel
                    {
                        Name = Profile.Name,
                        Email = User.Email,
                        Id = newID,
                        Avatar = newFileNameAvt,
                        Status = 1,
                    };

                    ProfileList.AddNewItem(NewProfiletem);

                    ProfileList.CommitNew();
                    ProfileList.CancelNew();
                    ProfileList.CancelEdit();
                }
                else
                {
                    MessageBox.Show("Thêm người dùng không thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }
    } 
}
