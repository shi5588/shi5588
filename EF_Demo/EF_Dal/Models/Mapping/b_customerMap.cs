using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace EF_Dal.Models.Mapping
{
    public class b_customerMap : EntityTypeConfiguration<b_customer>
    {
        public b_customerMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.custom_no)
                .HasMaxLength(10);

            this.Property(t => t.custom_name)
                .HasMaxLength(50);

            this.Property(t => t.addr)
                .HasMaxLength(50);

            this.Property(t => t.remark)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("b_customer");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.custom_no).HasColumnName("custom_no");
            this.Property(t => t.custom_name).HasColumnName("custom_name");
            this.Property(t => t.addr).HasColumnName("addr");
            this.Property(t => t.remark).HasColumnName("remark");
        }
    }
}
