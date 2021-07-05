using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.CustomModels
{
    class ProfileCustomModel : INotifyPropertyChanged
    {
        private int _Id { get; set; }
        public int Id
        {
            get => _Id;
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                }
                onPropertyChanged();
            }
        }
        private string _Email { get; set; }
        public string Email
        {
            get => _Email;
            set
            {
                if (_Email != value)
                {
                    _Email = value;
                }
                onPropertyChanged();
            }
        }
        private string _Name { get; set; }
        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                }
                onPropertyChanged();
            }
        }
        private string _Avatar { get; set; }
        public string Avatar
        {
            get => _Avatar;
            set
            {
                if (_Avatar != value)
                {
                    _Avatar = value;
                }
                onPropertyChanged();
            }
        }
        private Nullable<int> _Status { get; set; }
        public int Status
        {
            get => (int)_Status;
            set
            {
                if (_Status != value)
                {
                    _Status = value;
                }
                onPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void onPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
