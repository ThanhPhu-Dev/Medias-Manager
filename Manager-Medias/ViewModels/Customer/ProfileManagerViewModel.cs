﻿using Manager_Medias.Commands;
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
        private const string DEFAULT_AVATAR = "default_avatar.png";
        private readonly UserStore _userStore;

        #region Command

        public ICommand SwitchProfileCmd { get; set; }
        public ICommand NewProfileCmd { get; set; }
        public ICommand OpenNewFileDialogCmd { get; set; }
        public ICommand CloseNewModalCmd { get; set; }

        public ICommand SelectedProfileCmd { get; set; }
        public ICommand OpenEditFileDialogCmd { get; set; }
        public ICommand CloseEditModalCmd { get; set; }
        public ICommand EditProfileCmd { get; set; }

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

        private string _inputProfileName;

        public string InputProfileName
        {
            get => _inputProfileName;
            set
            {
                ValidateProperty(value);
                _inputProfileName = value;
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

        private int _selectedProfileId;

        public int SelectedProfileId
        {
            get => _selectedProfileId;
            set
            {
                _selectedProfileId = value;
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
            this.ValidationRules.Add(nameof(this.InputProfileName), new List<ValidationRule>() { new EmptyStringValidationRule() });
            LoadCommand();
            LoadProfile();
        }

        public void LoadCommand()
        {
            SwitchProfileCmd = new RelayCommand<Object>(ActionSwitchProfile);
            NewProfileCmd = new RelayCommand<Object>(ActionNewProfile, (Object) => !HasErrors);
            OpenNewFileDialogCmd = new RelayCommand<Object>(ActionOpenFile);
            CloseNewModalCmd = new RelayCommand<Object>(ActionCloseModal);
            SelectedProfileCmd = new RelayCommand<Object>((Object id) =>
            {
                using (var db = new MediasManangementEntities())
                {
                    var profile = db.Profiles.Single(p => p.Id == (int)id);
                    SelectedProfileId = profile.Id;
                    PathAvatarFile = profile.Avatar;
                    InputProfileName = profile.Name;
                }
            });

            EditProfileCmd = new RelayCommand<Object>(ActionEditProfile);
            OpenEditFileDialogCmd = new RelayCommand<Object>(ActionEditFile);
            CloseEditModalCmd = new RelayCommand<Object>((Object) => ResetBinding());
        }

        public void LoadProfile()
        {
            Profiles = _userStore.Profiles;
        }

        public void ResetBinding()
        {
            PathAvatarFile = DEFAULT_AVATAR;
            InputProfileName = null;
            LoadProfile();
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
            if (string.IsNullOrEmpty(InputProfileName)) return;

            Profile profile = new Profile
            {
                Email = _userStore.Email,
                Name = InputProfileName,
                Status = 0,
            };

            if (PathAvatarFile != DEFAULT_AVATAR)
            {
                var uniqueFileName = Guid.NewGuid();
                var fileExtension = Path.GetExtension(PathAvatarFile);

                var baseFolder = AppDomain.CurrentDomain.BaseDirectory;
                var imagePath = Path.Combine(baseFolder, "Images\\Profile", $"{uniqueFileName}{fileExtension}");
                File.Copy(PathAvatarFile, imagePath);
                PathAvatarFile = $"{uniqueFileName}{fileExtension}";
            }

            profile.Avatar = PathAvatarFile;

            using (var db = new MediasManangementEntities())
            {
                db.Profiles.Add(profile);
                db.SaveChanges();
            }

            // reset
            ResetBinding();
        }

        public void ActionOpenFile(Object obj)
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
            ResetBinding();
        }

        public void ActionEditFile(Object obj)
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

        public void ActionEditProfile(Object obj)
        {
            using (var db = new MediasManangementEntities())
            {
                var profile = db.Profiles.Single(p => p.Id == SelectedProfileId);

                profile.Name = InputProfileName;
                if (Path.IsPathRooted(PathAvatarFile))
                {
                    var uniqueFileName = Guid.NewGuid();
                    var fileExtension = Path.GetExtension(PathAvatarFile);

                    var baseFolder = AppDomain.CurrentDomain.BaseDirectory;
                    var imagePath = Path.Combine(baseFolder, "Images\\Profile", $"{uniqueFileName}{fileExtension}");
                    File.Copy(PathAvatarFile, imagePath);
                    PathAvatarFile = $"{uniqueFileName}{fileExtension}";
                }
                profile.Avatar = PathAvatarFile;

                db.SaveChanges();
            }

            ResetBinding();
        }
    }
}