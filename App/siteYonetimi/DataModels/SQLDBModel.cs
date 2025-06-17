using siteYonetimi.SQLTables;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siteYonetimi.DataModels
{
    //code first bağlantıları için gerekli tanımlamaları burada yapıyoruz
    public partial class SQLDBModel : DbContext
    {
        public SQLDBModel(DbConnection Bağlantı, bool contextOwnsConnection) : base(Bağlantı, true)
        { }
        public virtual DbSet<blok> Bloks { get; set; }
        public virtual DbSet<daire> Daires { get; set; }
        public virtual DbSet<iller> İllers { get; set; }
        public virtual DbSet<kisiler> Kisilers { get; set; }
        public virtual DbSet<kisiParametre> KisiParametres { get; set; }
        public virtual DbSet<parametreler> Parametrelers { get; set; }
        public virtual DbSet<site> Sites { get; set; }
        public virtual DbSet<siteParametre> SiteParametres { get; set; }
        public virtual DbSet<daireKisi> DaireKisis { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<blok>(); 
            modelBuilder.Entity<daire>();
            modelBuilder.Entity<iller>();
            modelBuilder.Entity<kisiler>();
            modelBuilder.Entity<kisiParametre>();
            modelBuilder.Entity<parametreler>();
            modelBuilder.Entity<site>();
            modelBuilder.Entity<siteParametre>();
            modelBuilder.Entity<daireKisi>();
        }
    }
}


