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
    
    public partial class My_Lists
    {
        public Nullable<int> IdProfile { get; set; }
        public Nullable<int> IdMedia { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public int Id { get; set; }
    
        public virtual Media Media { get; set; }
    }
}
