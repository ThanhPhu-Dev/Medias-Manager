﻿using Manager_Medias.Commands;
using Manager_Medias.Services;
using Manager_Medias.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Manager_Medias.ViewModels.Customer
{
    public class MainLayoutViewModel : BaseViewModel
    {
        private readonly UserStore _userStore;

        //public Profile currentProfile => _userStore.Profiles

        #region Command

        public ICommand ComboboxAccountCmd { get; set; }

        public ICommand NavigateMovieCmd { get; }
        public ICommand NavigateAlbumCmd { get; }
        public ICommand NavigateAudioCmd { get; }
        public ICommand NavigateAccountCmd { get; }

        #endregion Command

        public MainLayoutViewModel(UserStore userStore, NavigationStore navigationStore)
        {
            _userStore = userStore;
            _navigationStore = navigationStore;
            ComboboxAccountCmd = new RelayCommand<SelectionChangedEventArgs>(ComboboxAccountChanged);

            NavigateAccountCmd = new NavigateCommand<MainAccountView>(
                new NavigationService<MainAccountView>(_navigationStore, () => new MainAccountView(userStore, _navigationStore)));

            _navigationStore.CurrentContentViewModelChanged += _navigationStore_CurrentContentViewModelChanged;
        }

        private void ComboboxAccountChanged(SelectionChangedEventArgs e)
        {
            e.Handled = true;
        }
    }
}