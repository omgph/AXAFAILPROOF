using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AxaFailProof.Models.Mapping
{
    public class BannerMap : EntityTypeConfiguration<Banner>
    {
        public BannerMap()
        {
            // Primary Key
            this.HasKey(t => t.BannerID);

            // Properties
            this.Property(t => t.Position)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Image)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Label)
                .HasMaxLength(100);

            this.Property(t => t.Button)
                .HasMaxLength(100);

            this.Property(t => t.Link)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Banner");
            this.Property(t => t.BannerID).HasColumnName("BannerID");
            this.Property(t => t.Position).HasColumnName("Position");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.Label).HasColumnName("Label");
            this.Property(t => t.Button).HasColumnName("Button");
            this.Property(t => t.Link).HasColumnName("Link");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
