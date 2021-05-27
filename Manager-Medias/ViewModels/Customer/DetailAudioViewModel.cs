﻿using Manager_Medias.Commands;
using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Manager_Medias.ViewModels.Customer
{
    public class DetailAudioViewModel : BaseViewModel
    {
        private int currentProfile = 0;
        public static readonly DependencyProperty AudioListProperty;
        public ICommand CmdSelectionChange { get; set; }
        public ICommand CmdPlayAudio { get; set; }
        public ICommand CmdPauseAudio { get; set; }
        //command like và lưu
        public ICommand CmdLike { get; set; }
        public ICommand CmdSave { get; set; }

        private MediaPlayer player = null;
        public DispatcherTimer _timer;

        static DetailAudioViewModel()
        {
            AudioListProperty = DependencyProperty.Register("AudioList", typeof(ObservableCollection<Audio>), typeof(DetailAudioViewModel));
        }
        public ObservableCollection<Audio> AudioList
        {
            get => (ObservableCollection<Audio>)GetValue(AudioListProperty);
            set => SetValue(AudioListProperty, value);
        }

        public Audio SelectedAudio { get => _selectedAudio; set => _selectedAudio = value; }
        public string TimeAudio { get => _timeAudio; set => _timeAudio = value; }
        public bool CheckLike
        {
            get => _checkLike;
            set
            {
                _checkLike = value;
                OnPropertyChanged();
            }
        }
        public bool CheckSave
        {
            get => _checkSave;
            set
            {
                _checkSave = value;
                OnPropertyChanged();
            }

        }
        public string Test { get => test; set => test = value; }

        private Audio _selectedAudio;
        private string _timeAudio;
        private bool _checkLike;
        private bool _checkSave;
        private string test;


        public DetailAudioViewModel(int profileid, int audioid)
        {
            currentProfile = profileid;

            Loaded(audioid);
            CmdSelectionChange = new RelayCommand<object>(SelectionChange);
            CmdPlayAudio = new RelayCommand<object>(PlayAudio);
            CmdPauseAudio = new RelayCommand<object>(PauseAudio);
            CmdLike = new RelayCommand<object>(Likemt);
            CmdSave = new RelayCommand<object>(Savemt);

            loadaudio(SelectedAudio.Mp3);
        }

        private void Savemt(object obj)
        {
            int mediaId = (int)obj;
            My_List li = new My_List()
            {
                IdProfile = currentProfile,
                IdMedia = mediaId,
                //date

            };
            using (var db = new MediasManangementEntities())
            {
                if (CheckSave)
                {
                    var likeSelect = db.My_Lists.Where(l => l.IdMedia == mediaId).Single() as My_List;
                    db.My_Lists.Remove(likeSelect);
                    CheckSave = false;
                    if (db.SaveChanges() > 0)
                    {
                        MessageBox.Show("Đã bỏ lưu");
                    }
                    else
                    {
                        MessageBox.Show("Đã lưu bài hát");
                    }
                }
                else
                {
                    db.My_Lists.Add(li);
                    CheckSave = true;
                    if (db.SaveChanges() > 0)
                    {
                        MessageBox.Show("Đã lưu bài hát");
                    }
                    else
                    {
                        MessageBox.Show("Đã bỏ lưu");
                    }
                }

            }
        }

        private void Likemt(object obj)
        {
            int mediaId = (int)obj;
            Like li = new Like()
            {
                IdProfile = currentProfile,
                IdMedia = mediaId,
                //date

            };
            using (var db = new MediasManangementEntities())
            {
                if (CheckLike)
                {
                    var likeSelect = db.Likes.Where(l => l.IdMedia == mediaId).Single() as Like;
                    db.Likes.Remove(likeSelect);
                    CheckLike = false;
                    if (db.SaveChanges() > 0)
                    {
                        MessageBox.Show("Đã xóa khỏi ds yêu thích");
                    }
                    else
                    {
                        MessageBox.Show("Đã thêm vào ds yêu thích");
                    }
                }
                else
                {
                    db.Likes.Add(li);
                    CheckLike = true;
                    if (db.SaveChanges() > 0)
                    {
                        MessageBox.Show("Đã thêm vào ds yêu thích");
                    }
                    else
                    {
                        MessageBox.Show("Đã xóa khỏi ds yêu thích");
                    }
                }

            }
        }

        private void PauseAudio(object obj)
        {
            player.Pause();
        }

        private void PlayAudio(object obj)
        {
            player.Play();
        }

        private void SelectionChange(object obj)
        {
            //check lại like và save của bài hát này 
            LoadLikeAndSave();
            string audioName = (string)obj;
            //sp.Play();
            player.Stop();
            player = null;
            loadaudio(audioName);
        }
        public void loadaudio(string audioMame)
        {
            if (player == null)
            {
                player = new MediaPlayer();
                var currentfolder = AppDomain.CurrentDomain.BaseDirectory;
                string url = currentfolder + "Images\\" + audioMame;
                player.Open(new Uri(url));
                player.Play();
            }
        }

        public void Loaded(int audioid)
        {
            using (var db = new MediasManangementEntities())
            {
                //set selcteditem for list audio
                SelectedAudio = db.Audios.Where(a => a.Id == audioid).Single() as Audio;

                //cập nhật danh sách bài hát liên quan (chung danh mục) cho UI
                AudioList = new ObservableCollection<Audio>(db.Audios.Where(au => au.IdCategory == SelectedAudio.IdCategory).ToList());

               
                LoadLikeAndSave();
            }
        }

        private void LoadLikeAndSave()
        {
            using (var db = new MediasManangementEntities())
            {
                //ktr xem đã like và lưu bài nhạc này chưa 
                //chưa có user id
                var nLike = db.Likes.Where(l => l.IdMedia == SelectedAudio.Id && l.IdProfile == currentProfile).Count();
                var nSave = db.My_Lists.Where(l => l.IdMedia == SelectedAudio.Id && l.IdProfile == currentProfile).Count();

                CheckLike = true ? nLike > 0 : false;
                CheckSave = true ? nSave > 0 : false;
            }
        }
    }
}
