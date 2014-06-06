using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AxaFailProof.Models
{
    public partial class Comment
    {
        public int CommentID { get; set; }
        public int StoryID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
