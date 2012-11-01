using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using EF_Dal.Models.Mapping;

namespace EF_Dal.Models
{
    public class ef_demoContext : DbContext
    {
        static ef_demoContext()
        {
            Database.SetInitializer<ef_demoContext>(null);
        }

        public ef_demoContext() : base("Name=ef_demoContext")
		{
		}

        public DbSet<b_cst_item> b_cst_item { get; set; }
        public DbSet<b_cst_item_relate> b_cst_item_relate { get; set; }
        public DbSet<b_customer> b_customer { get; set; }
        public DbSet<b_item> b_item { get; set; }
        public DbSet<t_so> t_so { get; set; }
        public DbSet<t_so_dtl> t_so_dtl { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new b_cst_itemMap());
            modelBuilder.Configurations.Add(new b_cst_item_relateMap());
            modelBuilder.Configurations.Add(new b_customerMap());
            modelBuilder.Configurations.Add(new b_itemMap());
            modelBuilder.Configurations.Add(new t_soMap());
            modelBuilder.Configurations.Add(new t_so_dtlMap());
        }
    }
}
