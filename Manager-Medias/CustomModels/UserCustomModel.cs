using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.CustomModels
{
    class UserCustomModel : INotifyPropertyChanged
    {
        private string _Email;
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

        private string _Password;
        public string Password
        {
            get => _Password;
            set
            {
                if (_Password != value)
                {
                    _Password = value;
                }
                onPropertyChanged();
            }
        }

        private Nullable<int> _Level;
        public Nullable<int> Level
        {
            get => _Level;
            set
            {
                if (_Level != value)
                {
                    _Level = value;
                }
                onPropertyChanged();
            }
        }

        private string _Code;
        public string Code
        {
            get => _Code;
            set
            {
                if (_Code != value)
                {
                    _Code = value;
                }
                onPropertyChanged();
            }
        }

        private string _NumberCard;
        public string NumberCard
        {
            get => _NumberCard;
            set
            {
                if (_NumberCard != value)
                {
                    _NumberCard = value;
                }
                onPropertyChanged();
            }
        }

        private string _Exp;
        public string Exp
        {
            get => _Exp;
            set
            {
                if (_Exp != value)
                {
                    _Exp = value;
                }
                onPropertyChanged();
            }
        }

        private Nullable<int> _roleId;
        public Nullable<int> roleId
        {
            get => _roleId;
            set
            {
                if (_roleId != value)
                {
                    _roleId = value;
                }
                onPropertyChanged();
            }
        }


        private Nullable<System.DateTime> _CreateAt;
        public Nullable<System.DateTime> CreateAt
        {
            get => _CreateAt;
            set
            {
                if (_CreateAt != value)
                {
                    _CreateAt = value;
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
