using Manager_Medias.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;

        public event Action CurrentContentViewModelChanged;

        private BaseViewModel _currentViewModel;
        private BaseViewModel _contentViewModel;

        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }

        public BaseViewModel ContentViewModel
        {
            get => _contentViewModel;
            set
            {
                _contentViewModel = value;
                OnCurrentContentViewModelChanged();
            }
        }

        public void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }

        public void OnCurrentContentViewModelChanged()
        {
            CurrentContentViewModelChanged?.Invoke();
        }
    }
}