//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Manager_Medias.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Audio
    {
        public int Id { get; set; }
        public Nullable<int> IdCategory { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Mp3 { get; set; }
        public string Time { get; set; }
        public string Singer { get; set; }
    
        public virtual Audio_Category Audio_Categories { get; set; }
        public virtual Media Media { get; set; }
    }
}
