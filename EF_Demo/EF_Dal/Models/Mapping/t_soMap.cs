using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace EF_Dal.Models.Mapping
{
    public class t_soMap : EntityTypeConfiguration<t_so>
    {
        public t_soMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.so_no)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.remark)
                .HasMaxLength(50);

            this.Property(t => t.last_user)
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("t_so");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.so_no).HasColumnName("so_no");
            this.Property(t => t.so_date).HasColumnName("so_date");
            this.Property(t => t.b_custom_id).HasColumnName("b_custom_id");
            this.Property(t => t.tax_rate).HasColumnName("tax_rate");
            this.Property(t => t.remark).HasColumnName("remark");
            this.Property(t => t.last_time).HasColumnName("last_time");
            this.Property(t => t.last_user).HasColumnName("last_user");

            // Relationships
            this.HasOptional(t => t.b_customer)
                .WithMany(t => t.t_so)
                .HasForeignKey(d => d.b_custom_id);

        }
    }
}
