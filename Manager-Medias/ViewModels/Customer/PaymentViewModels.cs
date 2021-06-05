using Manager_Medias.Commands;
using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class PaymentViewModels :BaseViewModel
    {
        public static readonly DependencyProperty LevelListProperty =
            DependencyProperty.Register("lstlvl", typeof(ObservableCollection<Level>), typeof(PaymentViewModels));
        public static readonly DependencyProperty userProperty =
            DependencyProperty.Register("userinfo", typeof(User), typeof(PaymentViewModels));
        public ICommand OpenNewModal { get; set; }

        public ObservableCollection<Level> lstlvl
        {
            get => (ObservableCollection<Level>)GetValue(LevelListProperty);
            set => SetValue(LevelListProperty, value);
        }

        public User userinfo
        {
            get => (User)GetValue(userProperty);
            set => SetValue(userProperty, value);
        }

        private string lvl;
        public string Level
        {
            get => lvl;
            set
            {
                lvl = value;
                OnPropertyChanged();
            }
        }

        private bool _isModalOpen = false;
        public bool IsModalOpen
        {
            get => _isModalOpen;
            set
            {
                _isModalOpen = value;
                OnPropertyChanged();
            }
        }
        public PaymentViewModels()
        {
            //gọi hàm load giao diện
            Loaded();
            OpenNewModal = new RelayCommand<Object>((Object o) => IsModalOpen = true);
        }

        private void Loaded()
        {
            using (var db = new MediasManangementEntities())
            {
                //cập nhật danh sách bài hát liên quan (chung danh mục) cho UI
                lstlvl = new ObservableCollection<Level>(db.Levels.ToList());
                userinfo = db.Users.Where(p => p.Email == _userStore.Email).FirstOrDefault() as User;
            }
        }
    }
}
