﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App.Db
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using App.Entity.Db;
    
    public partial class db_testEntities : DbContext
    {
        public db_testEntities()
            : base("name=db_testEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<DtTask> DtTasks { get; set; }
        public virtual DbSet<DtTasMarking> DtTasMarkings { get; set; }
        public virtual DbSet<DtUser> DtUsers { get; set; }
    }
}
