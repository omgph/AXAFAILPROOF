using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AxaFailProof.Models.Mapping
{
    public class HealthRiskScoreMap : EntityTypeConfiguration<HealthRiskScore>
    {
        public HealthRiskScoreMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.SaveFor)
                .HasMaxLength(4000);

            this.Property(t => t.Owned)
                .HasMaxLength(4000);

            this.Property(t => t.Bday)
                .HasMaxLength(50);

            this.Property(t => t.Email)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("HealthRiskScore");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.Bday).HasColumnName("Bday");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Smoker).HasColumnName("Smoker");
            this.Property(t => t.Pounds).HasColumnName("Pounds");
            this.Property(t => t.Feet).HasColumnName("Feet");
            this.Property(t => t.Inches).HasColumnName("Inches");
            this.Property(t => t.ParentsIllness).HasColumnName("ParentsIllness");
            this.Property(t => t.YouIllness).HasColumnName("YouIllness");
            this.Property(t => t.Alcohol).HasColumnName("Alcohol");
            this.Property(t => t.Coffee).HasColumnName("Coffee");
            this.Property(t => t.Exercise).HasColumnName("Exercise");
            this.Property(t => t.Income).HasColumnName("Income");
            this.Property(t => t.Savings).HasColumnName("Savings");
            this.Property(t => t.SaveFor).HasColumnName("SaveFor");
            this.Property(t => t.Owned).HasColumnName("Owned");
            this.Property(t => t.Email).HasColumnName("Email");
        }
    }
}
