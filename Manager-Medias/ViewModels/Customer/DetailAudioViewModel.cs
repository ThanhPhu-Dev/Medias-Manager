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
        public static readonly DependencyProperty AudioListProperty;
        public ICommand CmdSelectionChange { get; set; }
        public ICommand CmdPlayAudio { get; set; }
        public ICommand CmdPauseAudio { get; set; }
        //command like và lưu
        public ICommand CmdLike { get; set; }
        public ICommand CmdUnLike { get; set; }
        public ICommand CmdSave { get; set; }
        public ICommand CmdUnSave { get; set; }

        private MediaPlayer player = null;
        public DispatcherTimer _timer;

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
        public bool CheckLike { get => _checkLike; set => _checkLike = value; }
        public bool CheckSave { get => _checkSave; set => _checkSave = value; }

        private Audio _selectedAudio;
        private string _timeAudio;
        private bool _checkLike;
        private bool _checkSave;


        public DetailAudioViewModel()
        {
            Loaded();
            CmdSelectionChange = new RelayCommand<object>(SelectionChange);
            CmdPlayAudio = new RelayCommand<object>(PlayAudio);
            CmdPauseAudio = new RelayCommand<object>(PauseAudio);

            loadaudio("a_mp3_1.mp3");
           
        }

        private void PauseAudio(object obj)
        {
            player.Pause();
        }

        private void PlayAudio(object obj)
        {
            player.Play();
        }

        private void SelectionChange(object obj)
        {
            string audioName = (string)obj;
            //sp.Play();
            player.Stop();
            player = null;
            loadaudio(audioName);
        }
        public void loadaudio(string audioMame)
        {
            if (player == null)
            {
                player = new MediaPlayer();
                player.Open(new Uri($@"F:\2021 - 2022\UDQL2\Project\Medias-Manager\Manager-Medias\bin\Debug\Images\{audioMame}"));
                player.Play();
            }
        }

        public void Loaded()
        {
            using (var db = new MediasManangementEntities())
            {
                AudioList = new ObservableCollection<Audio>(db.Audios.ToList());

                //set selcteditem for list audio
                SelectedAudio = db.Audios.Where(a => a.Id == 1).Single() as Audio;
            }
        }
    }
}
