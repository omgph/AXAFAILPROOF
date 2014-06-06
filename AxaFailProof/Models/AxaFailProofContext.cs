using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AxaFailProof.Models.Mapping;

namespace AxaFailProof.Models
{
    public partial class AxaFailProofContext : DbContext
    {
        static AxaFailProofContext()
        {
            Database.SetInitializer<AxaFailProofContext>(null);
        }

        public AxaFailProofContext()
            : base("Name=AxaFailProofContext")
        {
        }

        public DbSet<Banner> Banners { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ContactU> ContactUs { get; set; }
        public DbSet<EventsRegistered> EventsRegistereds { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Mushroom> Mushrooms { get; set; }
        public DbSet<SocialUser> SocialUsers { get; set; }
        public DbSet<StaticPage> StaticPages { get; set; }
        public DbSet<Story> Stories { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<FailProofing> FailProofing { get; set; }
        public DbSet<HealthRiskScore> HealthRiskScore { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BannerMap());
            modelBuilder.Configurations.Add(new CommentMap());
            modelBuilder.Configurations.Add(new ContactUMap());
            modelBuilder.Configurations.Add(new EventsRegisteredMap());
            modelBuilder.Configurations.Add(new LocationMap());
            modelBuilder.Configurations.Add(new MushroomMap());
            modelBuilder.Configurations.Add(new SocialUserMap());
            modelBuilder.Configurations.Add(new StaticPageMap());
            modelBuilder.Configurations.Add(new StoryMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TopicMap());
            modelBuilder.Configurations.Add(new QuizMap());
            modelBuilder.Configurations.Add(new FailProofingMap());
            modelBuilder.Configurations.Add(new HealthRiskScoreMap());
        }
    }
}
