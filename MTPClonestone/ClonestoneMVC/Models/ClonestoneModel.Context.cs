﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClonestoneMVC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class ClonestoneEntities : DbContext
    {
        public ClonestoneEntities()
            : base("name=ClonestoneEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblability> tblabilities { get; set; }
        public virtual DbSet<tblcard> tblcards { get; set; }
        public virtual DbSet<tblclass> tblclasses { get; set; }
        public virtual DbSet<tbldeck> tbldecks { get; set; }
        public virtual DbSet<tbldeckcard> tbldeckcards { get; set; }
        public virtual DbSet<tbledit> tbledits { get; set; }
        public virtual DbSet<tbleditFile> tbleditFiles { get; set; }
        public virtual DbSet<tblgame> tblgames { get; set; }
        public virtual DbSet<tbllogin> tbllogins { get; set; }
        public virtual DbSet<tblperson> tblpersons { get; set; }
        public virtual DbSet<tblrole> tblroles { get; set; }
        public virtual DbSet<tbltype> tbltypes { get; set; }
    
        public virtual ObjectResult<spGetMana5_Result> spGetMana5()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetMana5_Result>("spGetMana5");
        }
    }
}
