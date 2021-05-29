using Manager_Medias.Commands;
using Manager_Medias.Models;
using Manager_Medias.Stores;
using Manager_Medias.Validates;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class ProfileManagerViewModel : BaseViewModel
    {
        private const string DEFAULT_AVATAR = "Profile\\default_avatar.png";
        private readonly UserStore _userStore;

        #region Command

        public ICommand SwitchProfileCmd { get; set; }
        public ICommand NewProfileCmd { get; set; }
        public ICommand OpenFileDialogCmd { get; set; }
        public ICommand CloseFileDialogCmd { get; set; }

        #endregion Command

        #region Binding

        private ObservableCollection<Profile> _profiles;

        public ObservableCollection<Profile> Profiles
        {
            get => _profiles;
            set
            {
                _profiles = value;
                OnPropertyChanged();
            }
        }

        private string _newProfileName;

        public string NewProfileName
        {
            get => _newProfileName;
            set
            {
                ValidateProperty(value);
                _newProfileName = value;
                OnPropertyChanged();
            }
        }

        private string _pathAvatarFile;

        public string PathAvatarFile
        {
            get => _pathAvatarFile;
            set
            {
                _pathAvatarFile = value;
                OnPropertyChanged();
            }
        }

        #endregion Binding

        public ProfileManagerViewModel(UserStore userStore)
        {
            _userStore = userStore;
            _pathAvatarFile = DEFAULT_AVATAR;
            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();

            // Create a Dictionary of validation rules for fast lookup.
            // Each property name of a validated property maps to one or more ValidationRule.
            this.ValidationRules.Add(nameof(this.NewProfileName), new List<ValidationRule>() { new EmptyStringValidationRule() });
            LoadCommand();
            LoadProfile();
        }

        public void LoadCommand()
        {
            SwitchProfileCmd = new RelayCommand<Object>(ActionSwitchProfile);
            NewProfileCmd = new RelayCommand<Object>(ActionNewProfile, (Object) => !HasErrors);
            OpenFileDialogCmd = new RelayCommand<Object>(ActionOpenFile);
            CloseFileDialogCmd = new RelayCommand<Object>(ActionCloseModal);
        }

        public void LoadProfile()
        {
            Profiles = _userStore.Profiles;
        }

        public void ActionSwitchProfile(Object profileId)
        {
            using (var db = new MediasManangementEntities())
            {
                var user = db.Users.Where(u => u.Email == _userStore.Email).Single();
                var currentActiveProfile = user.Profiles.Where(p => p.Status == 1).Single();

                var selectedProfile = user.Profiles.Where(p => p.Id == int.Parse(profileId.ToString())).Single();
                currentActiveProfile.Status = 0;
                selectedProfile.Status = 1;

                db.SaveChanges();
            }

            Profiles = _userStore.Profiles;
        }

        public void ActionNewProfile(Object obj)
        {
            if (string.IsNullOrEmpty(NewProfileName)) return;

            Profile profile = new Profile
            {
                Email = _userStore.Email,
                Name = NewProfileName,
                Status = 0,
            };

            if (PathAvatarFile != DEFAULT_AVATAR)
            {
                var uniqueFileName = Guid.NewGuid();
                var fileExtension = Path.GetExtension(PathAvatarFile);

                var baseFolder = AppDomain.CurrentDomain.BaseDirectory;
                var imagePath = Path.Combine(baseFolder, "Images\\Profile", $"{uniqueFileName}{fileExtension}");
                File.Copy(PathAvatarFile, imagePath);
                PathAvatarFile = $"Profile\\{uniqueFileName}{fileExtension}";

                profile.Avatar = $"{uniqueFileName}{fileExtension}";
            }
            else
            {
                profile.Avatar = "default_avatar.png";
            }

            using (var db = new MediasManangementEntities())
            {
                db.Profiles.Add(profile);
                db.SaveChanges();
            }

            // reset
            PathAvatarFile = DEFAULT_AVATAR;
            NewProfileName = string.Empty;
        }

        public void ActionOpenFile(object obj)
        {
            OpenFileDialog fd = new OpenFileDialog()
            {
                Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png"
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                PathAvatarFile = fd.FileName;
            }
        }

        public void ActionCloseModal(Object obj)
        {
            // reset
            PathAvatarFile = DEFAULT_AVATAR;
            NewProfileName = string.Empty;
        }
    }
}