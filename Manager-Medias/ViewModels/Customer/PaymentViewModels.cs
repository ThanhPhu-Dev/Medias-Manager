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
        public ICommand submit { get; set; }
        public ICommand CloseEditModalCmd { get; set; }

        public ObservableCollection<Level> lstlvl
        {
            get => (ObservableCollection<Level>)GetValue(LevelListProperty);
            set => SetValue(LevelListProperty, value);
        }

        private int lvlIdUp;

        private string _lvlIdCurrent;
        public string LvlIdCurrent
        {
            get => _lvlIdCurrent;
            set
            {
                _lvlIdCurrent = value;
                OnPropertyChanged();
            }
        }

        public User userinfo
        {
            get => (User)GetValue(userProperty);
            set => SetValue(userProperty, value);
        }

        private string _lvlNameCurrent;
        public string LevelNameCurrent
        {
            get => _lvlNameCurrent;
            set
            {
                _lvlNameCurrent = value;
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

        private string _message;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        private string _levelNameUp;
        public string LevelUpName
        {
            get => _levelNameUp;
            set
            {
                _levelNameUp = value;
                OnPropertyChanged();
            }
        }

        private string _priceLevelUp;
        public string PriceLevelUp
        {
            get => _priceLevelUp;
            set
            {
                _priceLevelUp = value;
                OnPropertyChanged();
            }
        }

        public PaymentViewModels()
        {
            //gọi hàm load giao diện
            Loaded();
            //OpenNewModal = new RelayCommand<Object>((Object o) => IsModalOpen = true);
            OpenNewModal = new RelayCommand<Object>(openmodal);
            CloseEditModalCmd = new RelayCommand<Object>((Object) => IsModalOpen = false);
            submit = new RelayCommand<Object>(submitChange);
        }

        private void submitChange(object obj)
        {
            using (var db = new MediasManangementEntities())
            {
                User user = db.Users.Where(p => p.Email == _userStore.Email).FirstOrDefault() as User;
                user.Level = lvlIdUp;
                db.SaveChanges();
            }
            IsModalOpen = false;
            LvlIdCurrent = _priceLevelUp;
            Message = "Thanh toán thành công";
        }

        private void openmodal(object obj)
        {
            lvlIdUp = int.Parse(obj.ToString());
            using (var db = new MediasManangementEntities())
            {
                Level lvlup = db.Levels.Where(p => p.Id == lvlIdUp).FirstOrDefault() as Level;
                var user = db.Users.Where(p => p.Email == _userStore.Email).FirstOrDefault() as User;
                if(lvlIdUp > user.Level)
                {
                    IsModalOpen = true;
                    LevelUpName = lvlup.Name;
                    PriceLevelUp = lvlup.Price.ToString();
                    LevelNameCurrent = user.Lvl.Name;
                }
                else
                {
                    Message = "Vui lòng chọn gói có cấp độ cao hơn cấp độ hiện tại";
                    IsModalOpen = false;
                }
            }
        }

        private void Loaded()
        {
            using (var db = new MediasManangementEntities())
            {
                //cập nhật danh sách bài hát liên quan (chung danh mục) cho UI
                lstlvl = new ObservableCollection<Level>(db.Levels.ToList());
                userinfo = db.Users.Where(p => p.Email == _userStore.Email).FirstOrDefault() as User;
                LvlIdCurrent = userinfo.Level.ToString();
            }
        }
    }
}
