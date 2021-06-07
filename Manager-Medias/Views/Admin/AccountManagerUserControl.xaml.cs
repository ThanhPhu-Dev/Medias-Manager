using Manager_Medias.Models;
using Manager_Medias.ViewModels.Admin;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
            addUserPanel.Collapse();

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
            txtbCodeAdd.Text = "";
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
        }

        private void OpenDialogChooseImg(int where)
        {
            var screen = new OpenFileDialog();
            //byte[] value = null;
            if (screen.ShowDialog() == true)
            {
                var filename = screen.FileName;
                var Image = new BitmapImage(new Uri(filename, UriKind.Absolute));

                if (where == 1)
                {
                    imgAvatarAdd.Source = Image;
                    txtavatar.Text = filename.Substring(filename.LastIndexOf("\\") + 1);
                }    
                    
                else
                {
                    imgAvatar.Source = Image;
                    txtavatar.Text = filename.Substring(filename.LastIndexOf("\\"));
                }    
                   
                //var encoder = new JpegBitmapEncoder();
                //encoder.Frames.Add(BitmapFrame.Create(Image));

                
                //using (var stream = new MemoryStream())
                //{
                //    encoder.Save(stream);
                //    value = stream.ToArray();
                //}
            }
            //return value;
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
    }
}
