using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AxaFailProof.Models.Mapping
{
    public class EventsRegisteredMap : EntityTypeConfiguration<EventsRegistered>
    {
        public EventsRegisteredMap()
        {
            // Primary Key
            this.HasKey(t => t.RegEventID);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(200);

            this.Property(t => t.LastName)
                .HasMaxLength(200);

            this.Property(t => t.ContactNumber)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("EventsRegistered");
            this.Property(t => t.RegEventID).HasColumnName("RegEventID");
            this.Property(t => t.LocationID).HasColumnName("LocationID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.LocationCity).HasColumnName("LocationCity");
            this.Property(t => t.ContactNumber).HasColumnName("ContactNumber");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.CardHolder).HasColumnName("CardHolder");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Terms).HasColumnName("Terms");

            // Relationships
            this.HasRequired(t => t.Location)
                .WithMany(t => t.EventsRegistereds)
                .HasForeignKey(d => d.LocationID);

        }
    }
}
