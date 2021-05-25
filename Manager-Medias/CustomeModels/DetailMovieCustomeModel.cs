using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.CustomeModels
{
    public class DetailMovieCustomeModel
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
        public string image { get; set; }
        public string video { get; set; }
    }
}
