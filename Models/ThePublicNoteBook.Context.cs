﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ThePublicNoteBook.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ThePublicNoteBookEntities : DbContext
    {
        public ThePublicNoteBookEntities()
            : base("name=ThePublicNoteBookEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ArticleLike> ArticleLikes { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<UserLevel> UserLevels { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
