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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MediasManangementEntities : DbContext
    {
        public MediasManangementEntities()
            : base("name=MediasManangementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Album_Detail> Album_Details { get; set; }
        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Audio_Category> Audio_Categories { get; set; }
        public virtual DbSet<Audio> Audios { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Media_Category> Media_Categories { get; set; }
        public virtual DbSet<Media> Medias { get; set; }
        public virtual DbSet<Movie_Category> Movie_Categories { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<My_List> My_Lists { get; set; }
        public virtual DbSet<Payment_History> Payment_History { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<View_History> View_History { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Movie_classify> Movie_classify { get; set; }
    }
}
