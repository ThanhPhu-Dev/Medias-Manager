using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.CustomeModels
{
    public class DetailMovieCustomModel
    {
        public int Id { get; set; }
        public string Level { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int like { get; set; }
        public int view { get; set; }
        public string Time { get; set; }
        public double IMDB { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public string Directors { get; set; }
        public string Nation { get; set; }
    }
}
