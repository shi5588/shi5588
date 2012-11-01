using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace EF_Dal.Models.Mapping
{
    public class b_itemMap : EntityTypeConfiguration<b_item>
    {
        public b_itemMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.item_no)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.item_name)
                .IsRequired()
                .HasMaxLength(150);

            this.Property(t => t.units)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.remark)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("b_item");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.item_no).HasColumnName("item_no");
            this.Property(t => t.item_name).HasColumnName("item_name");
            this.Property(t => t.units).HasColumnName("units");
            this.Property(t => t.min_stock).HasColumnName("min_stock");
            this.Property(t => t.remark).HasColumnName("remark");
        }
    }
}
