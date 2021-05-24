using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Manager_Medias.ViewModels.Customer
{
    public class DetailAudioViewModel : BaseViewModel
    {
        public static readonly DependencyProperty AudioListProperty;
        static DetailAudioViewModel()
        {
            AudioListProperty = DependencyProperty.Register("AudioList", typeof(ObservableCollection<Audio>), typeof(DetailAudioViewModel));
        }
        public ObservableCollection<Audio> AudioList
        {
            get => (ObservableCollection<Audio>)GetValue(AudioListProperty);
            set => SetValue(AudioListProperty, value);
        }
        public DetailAudioViewModel()
        {
            using (var db = new MediasManangementEntities())
            {
                AudioList = new ObservableCollection<Audio>(db.Audios.ToList());
            }
        }
    }
}
