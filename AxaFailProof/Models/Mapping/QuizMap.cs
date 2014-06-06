using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AxaFailProof.Models.Mapping
{
    public class QuizMap : EntityTypeConfiguration<Quiz>
    {
        public QuizMap()
        {
            // Primary Key
            this.HasKey(t => t.QuizID);

            // Properties
            this.Property(t => t.FirstName)
                .HasMaxLength(200);

            this.Property(t => t.LocationCity)
                .HasMaxLength(200);

            this.Property(t => t.LastName)
                .HasMaxLength(200);

            this.Property(t => t.ContactNumber)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Quiz");
            this.Property(t => t.QuizID).HasColumnName("QuizID");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LocationCity).HasColumnName("LocationCity");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.ContactNumber).HasColumnName("ContactNumber");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.BirthDate).HasColumnName("BirthDate");
            this.Property(t => t.Score).HasColumnName("Score");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.Status).HasColumnName("Status");
        }
    }
}
