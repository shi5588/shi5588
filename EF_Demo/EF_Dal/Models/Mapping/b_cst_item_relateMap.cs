using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace EF_Dal.Models.Mapping
{
    public class b_cst_item_relateMap : EntityTypeConfiguration<b_cst_item_relate>
    {
        public b_cst_item_relateMap()
        {
            // Primary Key
            this.HasKey(t => t.id);

            // Properties
            this.Property(t => t.remark)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("b_cst_item_relate");
            this.Property(t => t.id).HasColumnName("id");
            this.Property(t => t.b_item_id).HasColumnName("b_item_id");
            this.Property(t => t.b_cst_item_id).HasColumnName("b_cst_item_id");
            this.Property(t => t.unit_rate).HasColumnName("unit_rate");
            this.Property(t => t.remark).HasColumnName("remark");

            // Relationships
            this.HasRequired(t => t.b_cst_item)
                .WithMany(t => t.b_cst_item_relate)  //如果改为.HasMany(t => t.b_cst_item_relate),则成一对多。
                .HasForeignKey(d => d.b_cst_item_id);//单向一对一引用

            this.HasRequired(t => t.b_item)
                .WithMany(t => t.b_cst_item_relate)
                .HasForeignKey(d => d.b_item_id);   //单向一对一引用

        }
    }
}
