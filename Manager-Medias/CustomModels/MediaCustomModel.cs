using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.CustomModels
{
    public class MediaCustomModel
    {
        public int ID { get; set; } // like / history
        public int MediaID { get; set; }
        public int HistoryID { get; set; }
        public string MediaType { get; set; }
        public DateTime Date { get; set; }
        public DateTime WatchedDate { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Time { get; set; }
        public double TimeWatched { get; set; }
        public bool IsWatched { get; set; }
        public string WatchedPercent { get; set; }

        public MediaCustomModel()
        {
            ID = -1;
            MediaID = -1;
            MediaType = string.Empty;
            Date = new DateTime();
            WatchedDate = new DateTime();
            Name = string.Empty;
            Image = string.Empty;
            Time = string.Empty;
            TimeWatched = 0;
            IsWatched = false;
            WatchedPercent = string.Empty;
        }
    }
}