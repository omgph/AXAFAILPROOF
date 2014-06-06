using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AxaFailProof.Models.Mapping
{
    public class ContactUMap : EntityTypeConfiguration<ContactU>
    {
        public ContactUMap()
        {
            // Primary Key
            this.HasKey(t => t.ContactID);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("ContactUs");
            this.Property(t => t.ContactID).HasColumnName("ContactID");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
        }
    }
}
