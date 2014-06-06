using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AxaFailProof.Models.Mapping
{
    public class StaticPageMap : EntityTypeConfiguration<StaticPage>
    {
        public StaticPageMap()
        {
            // Primary Key
            this.HasKey(t => t.StaticID);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Body)
                .IsRequired();

            this.Property(t => t.MetaDescription)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("StaticPage");
            this.Property(t => t.StaticID).HasColumnName("StaticID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.MetaKeyWords).HasColumnName("MetaKeyWords");
            this.Property(t => t.MetaDescription).HasColumnName("MetaDescription");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
