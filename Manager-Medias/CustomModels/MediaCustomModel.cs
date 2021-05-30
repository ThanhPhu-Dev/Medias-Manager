﻿using System;
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
        public string MediaType { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Time { get; set; }
    }
}