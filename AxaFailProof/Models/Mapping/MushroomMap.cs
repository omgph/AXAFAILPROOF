using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AxaFailProof.Models.Mapping
{
    public class MushroomMap : EntityTypeConfiguration<Mushroom>
    {
        public MushroomMap()
        {
            // Primary Key
            this.HasKey(t => t.MushroomID);

            // Properties
            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Body)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Mushroom");
            this.Property(t => t.MushroomID).HasColumnName("MushroomID");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
