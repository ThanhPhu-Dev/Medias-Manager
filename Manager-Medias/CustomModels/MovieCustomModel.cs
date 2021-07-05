using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.CustomModels
{
    public class MovieCustomModel : INotifyPropertyChanged
    {
        private int _Id;
        public int Id
        {
            get => _Id;
            set
            {
                if (_Id != value)
                {
                    _Id = value;
                    onPropertyChanged();
                }
            }
        }

        private Nullable<int> _IdCategory;
        public int IdCategory
        {
            get => (int)_IdCategory;
            set
            {
                if (_IdCategory != value)
                {
                    _IdCategory = value;
                    onPropertyChanged();
                }
            }
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    onPropertyChanged();
                }
            }
        }

        private string _Description;
        public string Description
        {
            get => _Description;
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    onPropertyChanged();
                }
            }
        }
        private Nullable<int> _IdClassifiles;
        public Nullable<int> IdClassifiles
        {
            get => (int)_IdClassifiles;
            set
            {
                if (_IdClassifiles != value)
                {
                    _IdClassifiles = value;

                }
                onPropertyChanged();
            }
        }
        private Nullable<double> _IMDB;
        public double IMDB
        {
            get => (double)_IMDB;
            set
            {
                if (_IMDB != value)
                {
                    _IMDB = value;
                    
                }
                onPropertyChanged();
            }
        }

        private string _Poster;
        public string Poster
        {
            get => _Poster;
            set
            {
                if (_Poster != value)
                {
                    _Poster = value;
                    onPropertyChanged();
                }
            }
        }

        private Nullable<int> _Likes;
        public int Likes
        {
            get => (int)_Likes;
            set
            {
                if (_Likes != value)
                {
                    _Likes = value;
                    onPropertyChanged();
                }
            }
        }

        private Nullable<int> _Age;
        public int Age
        {
            get => (int)_Age;
            set
            {
                if (_Age != value)
                {
                    _Age = value;
                }
                onPropertyChanged();
            }
            
        }

        private Nullable<int> _NumberOfViews;
        public int NumberOfViews
        {
            get => (int)_NumberOfViews;
            set
            {
                if (_NumberOfViews != value)
                {
                    _NumberOfViews = value;
                    onPropertyChanged();
                }
            }
        }

        private string _Video;
        public string Video
        {
            get => _Video;
            set
            {
                if (_Video != value)
                {
                    _Video = value;
                    onPropertyChanged();
                }
            }
        }

        private string _Season;
        public string Season
        {
            get => _Season;
            set
            {
                if (_Season != value)
                {
                    _Season = value;
                    onPropertyChanged();
                }
            }
        }

        private string _Time;
        public string Time
        {
            get => _Time;
            set
            {
                if (_Time != value)
                {
                    _Time = value;
                    onPropertyChanged();
                }
            }
        }

        private string _Directors;
        public string Directors
        {
            get => _Directors;
            set
            {
                if (_Directors != value)
                {
                    _Directors = value;
                }
                onPropertyChanged();
            }
        }

        private string _Nation;
        public string Nation
        {
            get => _Nation;
            set
            {
                if (_Nation != value)
                {
                    _Nation = value;
                    onPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        void onPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
