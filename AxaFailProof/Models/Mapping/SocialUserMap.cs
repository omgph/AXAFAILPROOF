using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AxaFailProof.Models.Mapping
{
    public class SocialUserMap : EntityTypeConfiguration<SocialUser>
    {
        public SocialUserMap()
        {
            // Primary Key
            this.HasKey(t => t.SocialID);

            // Properties
            this.Property(t => t.SocialID)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.SocialMedia)
                .HasMaxLength(50);

            this.Property(t => t.UserName)
                .HasMaxLength(100);

            this.Property(t => t.FullName)
                .HasMaxLength(100);

            this.Property(t => t.FirstName)
                .HasMaxLength(100);

            this.Property(t => t.LastName)
                .HasMaxLength(100);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("SocialUsers");
            this.Property(t => t.SocialID).HasColumnName("SocialID");
            this.Property(t => t.SocialMedia).HasColumnName("SocialMedia");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.FullName).HasColumnName("FullName");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
        }
    }
}
