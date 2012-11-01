using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace EF_Dal.Models.Mapping
{
    public class t_so_dtlMap : EntityTypeConfiguration<t_so_dtl>
    {
        public t_so_dtlMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.last_user)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("t_so_dtl");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.t_so_id).HasColumnName("t_so_id");
            this.Property(t => t.b_item_id).HasColumnName("b_item_id");
            this.Property(t => t.qty).HasColumnName("qty");
            this.Property(t => t.last_time).HasColumnName("last_time");
            this.Property(t => t.last_user).HasColumnName("last_user");
            this.Property(t => t.b_cst_item_id).HasColumnName("b_cst_item_id");
            this.Property(t => t.cst_qty).HasColumnName("cst_qty");

            // Relationships
            this.HasOptional(t => t.b_cst_item)
                .WithMany(t => t.t_so_dtl)
                .HasForeignKey(d => d.b_cst_item_id);
            this.HasRequired(t => t.b_item)
                .WithMany(t => t.t_so_dtl)
                .HasForeignKey(d => d.b_item_id);
            this.HasRequired(t => t.t_so)
                .WithMany(t => t.t_so_dtl)
                .HasForeignKey(d => d.t_so_id);

        }
    }
}
