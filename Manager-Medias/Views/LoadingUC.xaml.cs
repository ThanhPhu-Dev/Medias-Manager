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
    /// Interaction logic for LoadingUC.xaml
    /// </summary>
    public partial class LoadingUC : UserControl
    {
        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set { SetValue(IsLoadingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsLoading.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsLoadingProperty =
            DependencyProperty.Register("IsLoading", typeof(bool), typeof(LoadingUC),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                    SetVisibility, null, false, UpdateSourceTrigger.PropertyChanged));

        private static void SetVisibility(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LoadingUC loading)
            {
                loading.Container.Visibility = loading.IsLoading ? Visibility.Visible : Visibility.Hidden;
            }
        }

        public LoadingUC()
        {
            InitializeComponent();
            Container.Visibility = Visibility.Hidden;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                Modal.Width = Application.Current.MainWindow.ActualWidth;
                Modal.Height = Application.Current.MainWindow.ActualHeight;

                Canvas.SetTop(LoadingWrapper, Modal.Height / 2);
                Canvas.SetLeft(LoadingWrapper, Modal.Width / 2);
            }
        }
    }
}