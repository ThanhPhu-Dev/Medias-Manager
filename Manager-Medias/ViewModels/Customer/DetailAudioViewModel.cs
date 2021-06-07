using Manager_Medias.Commands;
using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Manager_Medias.ViewModels.Customer
{
    public class DetailAudioViewModel : BaseViewModel
    {
        private int currentProfile = 0;
        public static readonly DependencyProperty AudioListProperty;
        public ICommand CmdSelectionChange { get; set; }
        public ICommand CmdPlayAudio { get; set; }
        public ICommand CmdPauseAudio { get; set; }
        public ICommand CmdWindowLoaded { get; set; }

        //command like và lưu
        public ICommand CmdLike { get; set; }

        public ICommand CmdSave { get; set; }

        private MediaPlayer player = null;
        public DispatcherTimer _timer;
        public int Level => (int)_userStore.CurrentUser.Level;

        static DetailAudioViewModel()
        {
            AudioListProperty = DependencyProperty.Register("AudioList", typeof(ObservableCollection<Audio>), typeof(DetailAudioViewModel));
        }

        public ObservableCollection<Audio> AudioList
        {
            get => (ObservableCollection<Audio>)GetValue(AudioListProperty);
            set => SetValue(AudioListProperty, value);
        }

        public Audio SelectedAudio { get => _selectedAudio; set => _selectedAudio = value; }
        public string TimeAudio { get => _timeAudio; set => _timeAudio = value; }
        private double _sliderValue = 0;

        public double SliderValue
        {
            get => _sliderValue;
            set
            {
                _sliderValue = value;
                OnPropertyChanged();
            }
        }

        public bool CheckLike
        {
            get => _checkLike;
            set
            {
                _checkLike = value;
                OnPropertyChanged();
            }
        }

        public bool CheckSave
        {
            get => _checkSave;
            set
            {
                _checkSave = value;
                OnPropertyChanged();
            }
        }

        public string Test { get => test; set => test = value; }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        private Audio _selectedAudio;
        private string _timeAudio;
        private bool _checkLike;
        private bool _checkSave;
        private string test;
        private string _message;
        private int historyID { get; set; }
        private int id;

        public DetailAudioViewModel(int audioid)
        {
            currentProfile = _userStore.CurrentProfile.Id;
            this.id = audioid;

            Loaded(audioid);
            LoadCommand();

            CreateHistory();
        }

        public DetailAudioViewModel(int audioid, double time)
        {
            currentProfile = _userStore.CurrentProfile.Id;
            this.id = audioid;
            LoadCommand();
            Loaded(audioid);

            CreateHistory();

            GetTimeStartMedia(time);
        }

        public void LoadCommand()
        {
            CmdWindowLoaded = new RelayCommand<object>(WindowLoaded);
            CmdSelectionChange = new RelayCommand<object>(SelectionChange);
            CmdLike = new RelayCommand<object>(Likemt);
            CmdSave = new RelayCommand<object>(Savemt);
        }

        public void WindowLoaded(object o)
        {
            _navigationStore.CurrentContentViewModelChanged += OnClosingViewModel;
            Application.Current.MainWindow.Closed += MainWindow_Closed;
        }

        public void GetTimeStartMedia(double time)
        {
            SliderValue = time;
        }

        public void CreateHistory()
        {
            using (var db = new MediasManangementEntities())
            {
                View_History ht = new View_History
                {
                    Date = DateTime.Now,
                    IdMedia = this.id,
                    IdProfile = _userStore.CurrentProfile.Id,
                    time = "0",
                };

                db.View_History.Add(ht);
                db.SaveChanges();

                this.historyID = ht.Id;
            }
        }

        private void Savemt(object obj)
        {
            int mediaId = (int)obj;
            My_List li = new My_List()
            {
                IdProfile = currentProfile,
                IdMedia = mediaId,
                Date = DateTime.Now,
            };
            using (var db = new MediasManangementEntities())
            {
                if (CheckSave)
                {
                    var likeSelect = db.My_Lists.Where(l => l.IdMedia == mediaId && l.IdProfile == li.IdProfile).Single() as My_List;
                    db.My_Lists.Remove(likeSelect);
                    CheckSave = false;
                    if (db.SaveChanges() > 0)
                    {
                        Message = "Đã xóa khỏi danh sách cá nhân";
                    }
                }
                else
                {
                    db.My_Lists.Add(li);
                    CheckSave = true;
                    if (db.SaveChanges() > 0)
                    {
                        Message = "Đã thêm vào danh sách cá nhân";
                    }
                }
            }
        }

        private void Likemt(object obj)
        {
            int mediaId = (int)obj;
            Like li = new Like()
            {
                IdProfile = currentProfile,
                IdMedia = mediaId,
                Date = DateTime.Now,
            };
            using (var db = new MediasManangementEntities())
            {
                if (CheckLike)
                {
                    var likeSelect = db.Likes.Where(l => l.IdMedia == mediaId && l.IdProfile == li.IdProfile).Single() as Like;
                    db.Likes.Remove(likeSelect);
                    CheckLike = false;
                    if (db.SaveChanges() > 0)
                    {
                        Message = "Đã xóa khỏi danh sách yêu thích";
                    }
                }
                else
                {
                    db.Likes.Add(li);
                    CheckLike = true;
                    if (db.SaveChanges() > 0)
                    {
                        Message = "Đã thêm vào danh sách yêu thích";
                    }
                }
            }
        }

        private void SaveHistoryTime()
        {
            using (var db = new MediasManangementEntities())
            {
                int milisecond = (int)SliderValue;

                var ht = db.View_History.Single(h => h.Id == this.historyID);
                ht.time = milisecond.ToString();

                db.SaveChanges();
            }
        }

        private void SelectionChange(object obj)
        {
            if (obj != null)
            {
                this.id = (int)obj;
                SaveHistoryTime();
                SliderValue = 0;
                //tạo lịch sử đã xem
                CreateHistory();
                //check lại like và save của bài hát đang chọn
                LoadLikeAndSave();
            }
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            SaveHistoryTime();
        }

        private void OnClosingViewModel()
        {
            SaveHistoryTime();
            // Remove event
            _navigationStore.CurrentContentViewModelChanged -= OnClosingViewModel;
            Application.Current.MainWindow.Closed -= MainWindow_Closed;
        }

        public void Loaded(int audioid)
        {
            using (var db = new MediasManangementEntities())
            {
                //set selcteditem for list audio
                SelectedAudio = db.Audios.Where(a => a.Id == audioid).Single() as Audio;

                //cập nhật danh sách bài hát liên quan (chung danh mục) cho UI
                AudioList = new ObservableCollection<Audio>(db.Audios.Include("Media").Include("Audio_Categories").ToList());
                LoadLikeAndSave();
            }
        }

        private void LoadLikeAndSave()
        {
            using (var db = new MediasManangementEntities())
            {
                //ktr xem đã like và lưu bài nhạc này chưa
                //chưa có user id
                var nLike = db.Likes.Where(l => l.IdMedia == SelectedAudio.Id && l.IdProfile == currentProfile).Count();
                var nSave = db.My_Lists.Where(l => l.IdMedia == SelectedAudio.Id && l.IdProfile == currentProfile).Count();

                CheckLike = true ? nLike > 0 : false;
                CheckSave = true ? nSave > 0 : false;
            }
        }
    }
}