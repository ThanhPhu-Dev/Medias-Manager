using Manager_Medias.CustomModels;
using Manager_Medias.Models;
using Manager_Medias.ViewModels.Admin;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        readonly AdminViewVM vm;
        public AccountManagerUserControl()
        {
            InitializeComponent();
            filterPanel.Collapse();
            addUserPanel.Collapse();
            updateUserPanel.Collapse();

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

        int select;
        private void UsersDg_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            cbProfile.SelectedIndex = -1;
            txtbName.Text = "";
            txtbStatus.Text = "";
            imgAvatar.Visibility = Visibility.Hidden;

            
        }

        private void UsersDg_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (select == UsersDg.SelectedIndex)
            {
                UsersDg.SelectedIndex = -1;
            }
            select = UsersDg.SelectedIndex;
        }

        private void cbProfile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if(cbProfile.SelectedIndex >= 0)
            {
                imgAvatar.Visibility = Visibility.Visible;
            }

        }

        private void btAdvancedSearch_Click(object sender, RoutedEventArgs e)
        {
            addUserPanel.Collapse();
            if (filterPanel.IsCollapsed())
            {
                filterPanel.Show();
            }
            else
            {
                filterPanel.Collapse();
            }
        }

        private void btAddUser_Click(object sender, RoutedEventArgs e)
        {
            //cbRoles.SelectedIndex = 0;
            txtavatar.Text = null;
            txtbEmailAdd.Text = "";
            txtbLevelAdd.Text = "";
            txtbNumCardAdd.Text = "";
            txtbPasswordAdd.Text = "";

            txtbNameAdd.Text = "";
            imgAvatarAdd.Source = null;

            filterPanel.Collapse();
            if (addUserPanel.IsCollapsed())
            {
                addUserPanel.Show();

            }
            else
            {
                addUserPanel.Collapse();
            }

            cbProfile.SelectedIndex = -1;
        }

        private void OpenDialogChooseImg(int where)
        {
            var screen = new OpenFileDialog();
            //byte[] value = null;
            screen.Filter = "Image files (*.png,*jpg)|*png;*jpg";
            if (screen.ShowDialog() == true)
            {
                var filename = screen.FileName;
                var Image = new BitmapImage(new Uri(filename, UriKind.Absolute));

                if (where == 1)
                {
                    imgAvatarAdd.Source = Image;
                    txtavatar.Text = filename;
                }    
                    
                else
                {
                    imgAvatar.Visibility = Visibility.Visible;
                    txtavatar2.Text = filename;
                }    
            }
        }
        private void btChooseImgAdd_Click(object sender, RoutedEventArgs e)
        {

            OpenDialogChooseImg(1);
            //txtavatar.Text = Encoding.Default.GetString(array);

        }

        private void btChooseAvatar_Click(object sender, RoutedEventArgs e)
        {
             OpenDialogChooseImg(2);
            //txtavatar.Text = Encoding.Default.GetString(array);
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btAddUser_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btAddUserCmd_Click(object sender, RoutedEventArgs e)
        {
            cbProfile.SelectedIndex = -1;
        }

        private void UsersDg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void btOpenUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            txtbPasswordUpdate.Text = "";
            profilePanel.Collapse();
            updateUserPanel.Show();
        }

        private void btProfileList_Click(object sender, RoutedEventArgs e)
        {
            profilePanel.Show();
            updateUserPanel.Collapse();
        }

        private void searchTb_TextChanged(object sender, TextChangedEventArgs e)
        {

            int filterLevel = 0;
            if(_check != null)
            {
                filterLevel = int.Parse(_check.Tag.ToString());
            }
            int filterRole = cbSearchRole.SelectedIndex + 1;

            if(UsersDg != null)
            {
                this.UsersDg.CommitEdit();
                this.UsersDg.CancelEdit();
            }
            else
            {
                return;
            }

            string filter = "";
            if(searchTb != null)
            {
                filter = searchTb.Text;
            }
            ICollectionView cv = CollectionViewSource.GetDefaultView(UsersDg.ItemsSource);

            if (filterRole == 3 && filterLevel == 0 && filter == "")
                cv.Filter = null;
            else
            {
                if(cbSearch.SelectedIndex == 0)
                {

                    cv.Filter = o =>
                    {
                        UserCustomModel p = o as UserCustomModel;
                        if (filterLevel == 0)
                        {
                            return (p.roleId == filterRole) && (p.Email.ToUpper().Contains(filter.ToUpper()));
                        }
                        if (filterRole == 3)
                        {
                            return (p.Level == filterLevel) && (p.Email.ToUpper().Contains(filter.ToUpper()));
                        }
                        return (p.roleId == filterRole) && (p.Level == filterLevel) && (p.Email.ToUpper().Contains(filter.ToUpper()));
                    };
                }
                else
                {
                    cv.Filter = o =>
                    {
                        UserCustomModel p = o as UserCustomModel;
                        if (filterLevel == 0)
                        {
                            return (p.roleId == filterRole) && (p.NumberCard.ToUpper().Contains(filter.ToUpper()));
                        }
                        if (filterRole == 3)
                        {
                            return (p.Level == filterLevel) && (p.NumberCard.ToUpper().Contains(filter.ToUpper()));
                        }
                        return (p.roleId == filterRole) && (p.Level == filterLevel) && (p.NumberCard.ToUpper().Contains(filter.ToUpper()));

                    };
                }
                
            }
        }

        private void cbSearch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            searchTb.Text = "";
        }

        private void btSearch_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void cbSearchRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            searchTb_TextChanged(null, null);
        }

        RadioButton _check;
        private void rdBt4_Checked(object sender, RoutedEventArgs e)
        {
            var checkedValue = levelPanel1.Children.OfType<RadioButton>()
                             .FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value);
            if (checkedValue == null)
            {
                checkedValue = levelPanel2.Children.OfType<RadioButton>()
                             .FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value);
            }
            _check = checkedValue;
            searchTb_TextChanged(null, null);
        }
    }
}
