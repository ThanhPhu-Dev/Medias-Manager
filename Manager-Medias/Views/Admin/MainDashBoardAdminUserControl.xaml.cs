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
    /// Interaction logic for MainDashBoardAdminUserControl.xaml
    /// </summary>
    /// 
    static class VisibilityExtension
    {
        public static bool IsCollapsed(this Grid stk)
        {
            return stk.Visibility == Visibility.Collapsed;
        }

        public static void Show(this Grid stk)
        {
            stk.Visibility = Visibility.Visible;
        }

        public static void Collapse(this Grid stk)
        {
            stk.Visibility = Visibility.Collapsed;
        }
    }
    public partial class MainDashBoardAdminUserControl : UserControl
    {
        public MainDashBoardAdminUserControl()
        {
            InitializeComponent();
            var vm = new AdminViewMainVM();
            this.DataContext = vm;
        }
    }
}
