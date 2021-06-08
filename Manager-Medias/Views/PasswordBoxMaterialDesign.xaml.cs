using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Manager_Medias.Views
{
    /// <summary>
    /// Interaction logic for PasswordBoxMaterialDesign.xaml
    /// </summary>
    public partial class PasswordBoxMaterialDesign : UserControl
    {
        private bool _isPasswordChanging;

        public string Hint
        {
            get { return (string)GetValue(HintProperty); }
            set { SetValue(HintProperty, value); }
        }

        public static readonly DependencyProperty HintProperty =
            DependencyProperty.Register("Hint", typeof(string), typeof(PasswordBoxMaterialDesign),
                new FrameworkPropertyMetadata("hint", FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    HintPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));

        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordBoxMaterialDesign),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    PasswordPropertyChanged, null, false, UpdateSourceTrigger.PropertyChanged));

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBoxMaterialDesign passwordBox)
            {
                passwordBox.updatePassword();
            }
        }

        private static void HintPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PasswordBoxMaterialDesign passwordBox)
            {
                passwordBox.updateHint();
            }
        }

        private void updateHint()
        {
            MaterialDesignThemes.Wpf.HintAssist.SetHint(PwBox, Hint);
        }

        public PasswordBoxMaterialDesign()
        {
            InitializeComponent();
            MaterialDesignThemes.Wpf.HintAssist.SetHint(PwBox, "Mật khẩu");
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            _isPasswordChanging = true;
            Password = PwBox.Password;
            _isPasswordChanging = false;
        }

        private void updatePassword()
        {
            if (!_isPasswordChanging)
            {
                PwBox.Password = Password;
            }
        }
    }
}