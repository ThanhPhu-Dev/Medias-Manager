using Manager_Medias.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.ViewModels
{
    class DetailMovie
    {

        public void text()
        {
            using (var x = new MediasManangementEntities())
            {
                Movies a = x.Movies.FirstOrDefault() as Movies;
            }
        }
    }
}
