using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manager_Medias.CustomModels
{
    class UserCustomModel 
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Nullable<int> Level { get; set; }
        public string Code { get; set; }
        public string NumberCard { get; set; }
        public string Exp { get; set; }
        public Nullable<int> roleId { get; set; }
        public Nullable<System.DateTime> CreateAt { get; set; }
    }
}
