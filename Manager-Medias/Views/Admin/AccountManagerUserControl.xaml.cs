using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Manager_Medias.Views.Admin
{
    /// <summary>
    /// Interaction logic for AccountManagerUserControl.xaml
    /// </summary>
    public partial class AccountManagerUserControl : UserControl
    {
        public AccountManagerUserControl()
        {
            InitializeComponent();
            filterPanel.Collapse();


            List<User> users = new List<User>();
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com", Level = 3, Password = "John Doe", CreateAt = new DateTime(2020, 7, 23), Balance = 120000, cardNum = "691100003613474", Accumulated = 1247, Code = "FHTY7V" });
            users.Add(new User() { Email = "thanhphuLe@1245yahoo.com", Level = 2, Password = "5363425235", CreateAt = new DateTime(2020, 3, 3), Balance = 130000, cardNum = "691145615274", Accumulated = 1247, Code = "DFG464" });
            users.Add(new User() { Email = "dk66nguyen@1245yahoo.com", Level = 3, Password = "ghg245235235", CreateAt = new DateTime(2020, 2, 27), Balance = 20000, cardNum = "64560003615274", Accumulated = 1, Code = "FGHF34" });
            users.Add(new User() { Email = "duongtrongFuh@1245yahoo.com", Level = 1, Password = "2352668hjnf", CreateAt = new DateTime(2020, 11, 13), Balance = 120000, cardNum = "645600003615274", Accumulated = 144, Code = "FHTY7V" });
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com", Level = 1, Password = "zxczxc697445", CreateAt = new DateTime(2020, 7, 3), Balance = 20000, cardNum = "4600003615274", Accumulated = 52, Code = "34534GHFH" });
            users.Add(new User() { Email = "nghiaDxDz@1245yahoo.com", Level = 1, Password = "45kli1024", CreateAt = new DateTime(2020, 11, 23), Balance = 190000, cardNum = "69456003615274", Accumulated = 13, Code = "FHT5Y7V" });
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com", Level = 2, Password = "xvcv3463hj,", CreateAt = new DateTime(2020, 9, 11), Balance = 180000, cardNum = "69235003615274", Accumulated = 46, Code = "GFG3536" });
            users.Add(new User() { Email = "minhthong_ars@1245yahoo.com", Level = 3, Password = "sdfs34fg3", CreateAt = new DateTime(2020, 2, 22), Balance = 20000, cardNum = "6917503615274", Accumulated = 135, Code = "F353HHTY7V" });
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com", Level = 2, Password = "fdgfdfg", CreateAt = new DateTime(2020, 7, 23), Balance = 129000, cardNum = "6934563245274", Accumulated = 1247, Code = "343HGH" });
            users.Add(new User() { Email = "lethiC@1245yahoo.com", Level = 3, Password = "JohnDoe", CreateAt = new DateTime(2020, 1, 16), Balance = 80000, cardNum = "69113463615274", Accumulated = 345, Code = "46HJMH3" });
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com", Level = 2, Password = "JohnDoe", CreateAt = new DateTime(2020, 7, 6), Balance = 90000, cardNum = "691346274", Accumulated = 23, Code = "DFDFEH" });
            users.Add(new User() { Email = "vovanminhra@1245yahoo.com", Level = 1, Password = "45kli1024", CreateAt = new DateTime(2020, 7, 23), Balance = 990000, cardNum = "6911003455274", Accumulated = 1247, Code = "FHTY7V" });
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com", Level = 2, Password = "John Doe", CreateAt = new DateTime(2020, 6, 6), Balance = 120000, cardNum = "6234100003615274", Accumulated = 467, Code = "DGĐFDF" });
            users.Add(new User() { Email = "JohnDoe@1245yahoo.com", Level = 3, Password = "45ksdwtli1024", CreateAt = new DateTime(2020, 7, 23), Balance = 120000, cardNum = "69234003615274", Accumulated = 1247, Code = "BFGE47657" });

            UsersDg.ItemsSource = users;
        }

        private void filterOpenImg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (filterPanel.IsCollapsed())
            {
                filterPanel.Show();

            }
            else
            {
                filterPanel.Collapse();
            }
        }
    }
}
