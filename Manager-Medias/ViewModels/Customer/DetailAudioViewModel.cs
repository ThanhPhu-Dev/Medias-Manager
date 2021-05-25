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

namespace Manager_Medias.ViewModels.Customer
{
    public class DetailAudioViewModel : BaseViewModel
    {
        public static readonly DependencyProperty AudioListProperty;
        public ICommand CmdSelectionChange { get; set; }

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
        private Audio _selectedAudio;
        
        public DetailAudioViewModel()
        {
            Loaded();
            CmdSelectionChange = new RelayCommand<object>(SelectionChange);
        }

        private void SelectionChange(object obj)
        {
            byte[] result = System.IO.File.ReadAllBytes(@"F:\2021 - 2022\UDQL2\Project\Medias-Manager\Manager-Medias\bin\Debug\Images\1.mp3");
            System.IO.MemoryStream ms = new System.IO.MemoryStream(result);
            SoundPlayer sp = new SoundPlayer(ms);
            sp.Play();
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
