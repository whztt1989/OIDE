﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL.MDB
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class dbDataEntities : DbContext
    {
        public dbDataEntities()
            : base("name=dbDataEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Entity> Entity { get; set; }
        public virtual DbSet<EntityData> EntityData { get; set; }
        public virtual DbSet<SceneNode> SceneNode { get; set; }
        public virtual DbSet<UI> UI { get; set; }
        public virtual DbSet<Scene> Scene { get; set; }
    }
}
