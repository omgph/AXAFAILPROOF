using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AxaFailProof.Models;
using System.Collections;
using PagedList;
using Utils;

namespace AxaFailProof.Models
{
    public class AxaViewModel
    {
        public List<Banner> HomeBanners { get; set; }
        public IEnumerable<Banner> MainBanner { get; set; }
        public IEnumerable<Banner> Banner { get; set; }
        public IEnumerable<Banner> FotterBanner { get; set; }
        public IEnumerable<Story> FeaturedStory { get; set; }
        public IEnumerable<Story> ResentPost { get; set; }
        public IEnumerable<Story> StoryDetail { get; set; }
        public IEnumerable<Story> HealthFacts { get; set; }

        public List<Location> Locations { get; set; }
        public List<Story> Stories { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Topic> Topics { get; set; }
        public List<Story> DateArchive { get; set; }

        public IPagedList<Story> SearchResults { get; set; }

        public Mushroom Mushroom1 { get; set; }
        public Mushroom Mushroom2 { get; set; }
        public Mushroom Mushroom3 { get; set; }
        public Mushroom Mushroom4 { get; set; }

        public Mushroom Question1 { get; set; }
        public Mushroom Question2 { get; set; }
        public Mushroom Question3 { get; set; }
        public Mushroom Question4 { get; set; }
        public Mushroom Question5 { get; set; }
        public Mushroom Question6 { get; set; }

        public StaticPage StaticPage { get; set; }
        public EventsRegistered EventsRegistered { get; set; }
        public ContactU ContactUs { get; set; }
        public Story Story { get; set; }
        public Comment Comment { get; set; }
        public Quiz Quiz { get; set; }
        public SocialUser SocialUser { get; set; }
        public FailProofing FailProofing { get; set; }
        public YahooProfile Profile { get; set; }

        public int StoryID { get; set; }
        public string SearchBox { get; set; }
    }
}