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
    public partial class LoadingUC : UserControl, INotifyPropertyChanged
    {
        private double _windowWidth;
        private double _windowHeight;
        private double _halfWidth;
        private double _halfHeight;

        public event PropertyChangedEventHandler PropertyChanged;

        #region Binding

        public double WindowWidth
        {
            get => _windowWidth;
            set
            {
                _windowWidth = value;
                OnPropertyChanged();
            }
        }

        public double WindowHeight
        {
            get => _windowHeight;
            set
            {
                _windowHeight = value;
                OnPropertyChanged();
            }
        }

        public double HalfWidth
        {
            get => _halfWidth;
            set
            {
                _halfWidth = value;
                OnPropertyChanged();
            }
        }

        public double HalfHeight
        {
            get => _halfHeight;
            set
            {
                _halfHeight = value;
                OnPropertyChanged();
            }
        }

        #endregion Binding

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
            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;
            Container.Visibility = Visibility.Hidden;
            DataContext = this;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime)
            {
                WindowWidth = Application.Current.MainWindow.ActualWidth;
                WindowHeight = Application.Current.MainWindow.ActualHeight;

                HalfWidth = WindowWidth / 2;
                HalfHeight = WindowHeight / 2;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}