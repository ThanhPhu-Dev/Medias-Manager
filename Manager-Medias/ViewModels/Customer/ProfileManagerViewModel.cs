using Manager_Medias.Commands;
using Manager_Medias.Models;
using Manager_Medias.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class ProfileManagerViewModel : BaseViewModel
    {
        private readonly UserStore _userStore;

        #region Command

        public ICommand SwitchProfileCmd { get; set; }
        public ICommand NewProfileCmd { get; set; }
        public ICommand OpenFileDialogCmd { get; set; }

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
                _newProfileName = value;
                OnPropertyChanged();
            }
        }

        #endregion Binding

        public ProfileManagerViewModel(UserStore userStore)
        {
            _userStore = userStore;
            LoadCommand();
            LoadProfile();
        }

        public void LoadCommand()
        {
            SwitchProfileCmd = new RelayCommand<Object>(ActionSwitchProfile);
            NewProfileCmd = new RelayCommand<Object>(ActionNewProfile);
            OpenFileDialogCmd = new RelayCommand<Object>(ActionOpenFile);
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
        }

        public void ActionOpenFile(object obj)
        {
            OpenFileDialog fd = new OpenFileDialog()
            {
                Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif"
            };

            if (fd.ShowDialog() == DialogResult.OK)
            {
                var uniqueFileName = Guid.NewGuid();
                var fileExtension = Path.GetExtension(fd.FileName);

                var baseFolder = AppDomain.CurrentDomain.BaseDirectory;
                var imagePath = Path.Combine(baseFolder, "Images\\Profile", $"{uniqueFileName}{fileExtension}");

                //File.Copy(fd.FileName, imagePath);
            }
        }
    }
}