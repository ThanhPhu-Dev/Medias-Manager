using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.Services
{
    public class LoadingServices
    {
        public event Action LoadingChanged;

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnLoadingChanged();
            }
        }

        private void OnLoadingChanged()
        {
            LoadingChanged?.Invoke();
        }

        public LoadingServices()
        {
            _isLoading = false;
        }
    }
}