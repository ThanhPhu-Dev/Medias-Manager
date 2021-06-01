using Manager_Medias.CustomModels;
using Manager_Medias.Models;
using Manager_Medias.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.ViewModels.Customer
{
    public class PaymentHistoryViewModel : BaseViewModel
    {
        UserStore usercurrent;
        public PaymentHistoryViewModel(UserStore userStore)
        {
            this.usercurrent = userStore;
            loaded();

        }
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        private string _numberCard;
        public string NumberCard
        {
            get => _numberCard;
            set
            {
                _numberCard = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<InfoPaymentHistoryCustomModelcs> _infoPM;
        public ObservableCollection<InfoPaymentHistoryCustomModelcs> InfoPM
        {
            get => _infoPM;
            set
            {
                _infoPM = value;
                OnPropertyChanged();
            }
        }

        public void loaded()
        {
            _email = usercurrent.Email;
            _name = usercurrent.CurrentProfile.Name;
            _numberCard = usercurrent.CurrentUser.NumberCard;
            InfoPM = new ObservableCollection<InfoPaymentHistoryCustomModelcs>();
            using (var db = new MediasManangementEntities())
            {
                var lst = db.Payment_History.Where(p => p.Email == usercurrent.Email).OrderByDescending(s => s.DateOfPayment).ToList();
                for (int i = 0; i < lst.Count; i++)
                {
                    InfoPaymentHistoryCustomModelcs info = new InfoPaymentHistoryCustomModelcs(lst[i] as Payment_History);
                    info.STT = i + 1;
                    InfoPM.Add(info);
                }

            }
        }
    }
}