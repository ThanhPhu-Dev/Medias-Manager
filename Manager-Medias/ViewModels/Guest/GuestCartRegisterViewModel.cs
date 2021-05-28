using Manager_Medias.Commands;
using Manager_Medias.Models;
using Manager_Medias.Validates;
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

        public string Name
        {
            get => _name;
            set
            {
                 _name = value;
                OnPropertyChanged();
            }
        }
        public string CartNum
        {
            get => _cartNum;
            set
            {
                _cartNum = value;
                OnPropertyChanged();
            }
        }
        public string DateExpiration
        {
            get => _dateExpiration;
            set
            {
                _dateExpiration = value;
                OnPropertyChanged();
            }
        }

        public Level LevelSelected { get => levelSelected; set => levelSelected = value; }

        public GuestCartRegisterViewModel(User u)
        {
            _userCurrent = u;

            //khởi tạo các đối tượng validate
            this.Errors = new Dictionary<string, List<string>>();
            this.ValidationRules = new Dictionary<string, List<ValidationRule>>();
            this.ValidationRules.Add(nameof(this.Name), new List<ValidationRule>() { new ValidateEmailRegister() });
            this.ValidationRules.Add(nameof(this.CartNum), new List<ValidationRule>() { new ValidatePassword() });
            this.ValidationRules.Add(nameof(this.DateExpiration), new List<ValidationRule>() { new ValidatePassword() });

            //tạo command
            CmdFinish = new RelayCommand<Object[]>(Finish);
            CmdPre = new RelayCommand<Object[]>(PrePage);

            //lấy level đã chọn
            using (var db = new MediasManangementEntities())
            {
                LevelSelected = db.Levels.Where(lv => lv.Id == u.Level).Single() as Level;
            }
        }

        private void PrePage(object[] obj)
        {
            throw new NotImplementedException();
        }

        private void Finish(object[] obj)
        {
            throw new NotImplementedException();
        }
    }
}
