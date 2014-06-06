using System;
using System.Collections.Generic;

namespace AxaFailProof.Models
{
    public partial class Topic
    {
        public int TopicID { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> Status { get; set; }
        public virtual ICollection<Story> Stories { get; set; }
    }
}
