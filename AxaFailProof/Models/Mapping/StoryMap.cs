using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace AxaFailProof.Models.Mapping
{
    public class StoryMap : EntityTypeConfiguration<Story>
    {
        public StoryMap()
        {
            // Primary Key
            this.HasKey(t => t.StoryID);

            this.Property(t => t.Author)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Image)
                .HasMaxLength(200);

            this.Property(t => t.MetaDescription)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Stories");
            this.Property(t => t.StoryID).HasColumnName("StoryID");
            this.Property(t => t.TopicID).HasColumnName("TopicID");
            this.Property(t => t.Author).HasColumnName("Author");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.Body).HasColumnName("Body");
            this.Property(t => t.CommnetsCount).HasColumnName("CommnetsCount");
            this.Property(t => t.Tags).HasColumnName("Tags");
            this.Property(t => t.MetaKeyWords).HasColumnName("MetaKeyWords");
            this.Property(t => t.MetaDescription).HasColumnName("MetaDescription");
            this.Property(t => t.DateCreated).HasColumnName("DateCreated");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Featured).HasColumnName("Featured");

            // Relationships
            this.HasRequired(t => t.Topics)
                .WithMany(t => t.Stories)
                .HasForeignKey(d => d.TopicID);

        }
    }
}
