using Manager_Medias.ViewModels.Admin;
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
        AdminViewVM vm;
        public AccountManagerUserControl()
        {
            InitializeComponent();
            filterPanel.Collapse();

            vm = new AdminViewVM();
            DataContext = vm;
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
