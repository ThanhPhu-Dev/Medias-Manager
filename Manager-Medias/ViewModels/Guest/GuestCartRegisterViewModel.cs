using Manager_Medias.Commands;
using Manager_Medias.Models;
using Manager_Medias.Stores;
using Manager_Medias.Validates;
using Manager_Medias.ViewModels.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Guest
{
    public class GuestCartRegisterViewModel : BaseViewModel
    {
        private User _userCurrent;
        private string _name;
        private string _cartNum;
        private string _dateExpiration;
        private Level levelSelected;
        public ICommand CmdPre { get; }
        public ICommand CmdFinish { get; }
        private bool _checkApply;

        #region BindingProperty

        public string Name
        {
            get => _name;
            set
            {
                ValidateProperty(value);
                _name = value;
                OnPropertyChanged();
            }
        }

        public string CartNum
        {
            get => _cartNum;
            set
            {
                ValidateProperty(value);
                _cartNum = value;
                OnPropertyChanged();
            }
        }

        public string DateExpiration
        {
            get => _dateExpiration;
            set
            {
                ValidateProperty(value);
                _dateExpiration = value;
                OnPropertyChanged();
            }
        }

        public bool CheckApply
        {
            get => _checkApply;
            set
            {
                ValidateProperty(value);
                _checkApply = value;
                OnPropertyChanged();
            }
        }

        public Level LevelSelected { get => levelSelected; set => levelSelected = value; }

        #endregion BindingProperty

        public GuestCartRegisterViewModel(User u)
        {
            _userCurrent = u;

            //tạo command
            CmdFinish = new RelayCommand<Object[]>(Finish, (Object[] obj) => !HasErrors);
            CmdPre = new RelayCommand<Object[]>(PrePage);

            //khởi tạo các đối tượng validate
            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();
            this.ValidationRules.Add(nameof(this.Name), new List<ValidationRule>() { new AccountManagerValidationRule() });
            this.ValidationRules.Add(nameof(this.CartNum), new List<ValidationRule>() { new CartNumberValidationRule() });
            this.ValidationRules.Add(nameof(this.DateExpiration), new List<ValidationRule>() { new DateExpirationValidationRule() });
            this.ValidationRules.Add(nameof(this.CheckApply), new List<ValidationRule>() { new CheckAppyValidationRule() });

            //lấy level đã chọn
            using (var db = new MediasManangementEntities())
            {
                LevelSelected = db.Levels.Where(lv => lv.Id == u.Level).Single() as Level;
            }

            //set chấp nhận điều khoản là false
            CheckApply = false;
        }

        private void PrePage(object[] obj)
        {
            _navigationStore.ContentViewModel = new GuestLevelRegisterViewModel(_userCurrent);
        }

        private void Finish(object[] obj)
        {
            Profile profile = new Profile()
            {
                Email = _userCurrent.Email,
                Name = obj[0].ToString(),
                Status = 1,
            };

            using (var db = new MediasManangementEntities())
            {
                var user = db.Users.Where(u => u.Email == _userCurrent.Email).Single() as User;
                
                //update cart cho user
                user.NumberCard = obj[1].ToString();
                user.Exp = obj[2].ToString();
                //db.SaveChanges();

                //tạo user
                db.Profiles.Add(profile);
                db.SaveChanges();

                //chuyển trang dên trang chủ
                _userStore = new UserStore(user);
                _navigationStore.CurrentViewModel = new MainLayoutViewModel();
                _navigationStore.ContentViewModel = new HomeViewModel();
            }
        }
    }
}