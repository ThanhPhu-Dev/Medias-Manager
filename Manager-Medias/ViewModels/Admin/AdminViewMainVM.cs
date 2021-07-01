using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.ViewModels.Admin
{
    class AdminViewMainVM
    {
        public class MyData
        {
            public string Name { get; set; }
            public int Count { get; set; }

           
        }
        public long CapacityMedia { get; set; }
        public long CapacityMovie { get; set; }
        public string Revenue { get; set; }
        public int UserCount { get; set; }
        public int profileCount { get; set; }
        public string monthRevenue { get; set; }
        public ObservableCollection<MyData> Data { get; set; }

        public long increaseMovie { get; set; }


        public AdminViewMainVM()
        {

            DirectoryInfo dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Images");
            CapacityMedia = DirSize(dirInfo) / 1024 / 1024;

            dirInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "Images" + "\\Movie");
            CapacityMovie = DirSize(dirInfo) / 1024 / 1024;
            increaseMovie = 0;

            int perMovie, perImg, perAudio, lastMonthMovieCount;
            using(var db = new MediasManangementEntities())
            {
                double doanhthu = (double)db.Payment_History.Sum(u => u.Price);
                Revenue = doanhthu.ToString("#,#", CultureInfo.InvariantCulture);

                double doanhthuThang = 0.0;
                monthRevenue = "0";
                if (db.Payment_History.Where(p => p.DateOfPayment.Value.Month == DateTime.Today.Month).Count() != 0)
                {
                    doanhthuThang = (double)db.Payment_History.Where(p => p.DateOfPayment.Value.Month == DateTime.Today.Month).Sum(u => u.Price);
                    monthRevenue = doanhthuThang.ToString("#,#", CultureInfo.InvariantCulture);
                }

                UserCount = db.Users.Count();
                profileCount = db.Profiles.Where(p => p.Status == 1).Count();

                int movieCount = db.Movies.Count();
                int imageCount = db.Audios.Count();
                int audioCount = db.Audios.Count();

                int mediaCount = db.Medias.Count();

                perImg = imageCount * 100 / mediaCount;
                perMovie = movieCount * 100 / mediaCount;
                perAudio = audioCount * 100 / mediaCount;

                lastMonthMovieCount = db.Movies.Where(m => m.CreateAt.Value.Month == DateTime.Today.Month - 1).Count();
                if(lastMonthMovieCount != 0)
                {
                    increaseMovie = (movieCount - lastMonthMovieCount) / lastMonthMovieCount * 100;
                }
                

            }

            

            Data = new ObservableCollection<MyData>
            {
                new MyData {Name="Movie", Count = perMovie },
                new MyData {Name="Audio", Count = perAudio },
                new MyData {Name="Image", Count = perImg },
            };
        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
    }
}
