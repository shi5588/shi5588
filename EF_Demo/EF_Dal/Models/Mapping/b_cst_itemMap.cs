using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace EF_Dal.Models.Mapping
{
    public class b_cst_itemMap : EntityTypeConfiguration<b_cst_item>
    {
        public b_cst_itemMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.ten_code)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.item_no)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.item_type)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.item_name)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.units)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("b_cst_item");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.ten_code).HasColumnName("ten_code");
            this.Property(t => t.item_no).HasColumnName("item_no");
            this.Property(t => t.item_type).HasColumnName("item_type");
            this.Property(t => t.item_name).HasColumnName("item_name");
            this.Property(t => t.units).HasColumnName("units");
        }
    }
}
