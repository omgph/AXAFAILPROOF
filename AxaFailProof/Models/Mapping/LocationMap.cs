using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AxaFailProof.Models.Mapping
{
    public class LocationMap : EntityTypeConfiguration<Location>
    {
        public LocationMap()
        {
            // Primary Key
            this.HasKey(t => t.LocationID);

            // Properties
            this.Property(t => t.Location1)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Event)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Location");
            this.Property(t => t.LocationID).HasColumnName("LocationID");
            this.Property(t => t.Event).HasColumnName("Event");
            this.Property(t => t.EventDescription).HasColumnName("EventDescription");
            this.Property(t => t.EventDate).HasColumnName("EventDate");
            this.Property(t => t.EventTime).HasColumnName("EventTime");
            this.Property(t => t.Location1).HasColumnName("Location");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
