using System;
using System.Collections.Generic;

namespace AxaFailProof.Models
{
    public partial class StaticPage
    {
        public int StaticID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
