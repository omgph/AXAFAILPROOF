using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AxaFailProof.Models
{
    public partial class Story
    {
        public int StoryID { get; set; }
        public Nullable<int> TopicID { get; set; }
        public string Author { get; set; }
        [Required(ErrorMessage = "Please enter title.")]
        public string Title { get; set; }
        public string Image { get; set; }
        [Required(ErrorMessage = "Please enter your story.")]
        public string Body { get; set; }
        public int CommnetsCount { get; set; }
        public string Tags { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> Featured { get; set; }
        public virtual Topic Topics { get; set; }
    }
}
