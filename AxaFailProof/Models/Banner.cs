using System;
using System.Collections.Generic;

namespace AxaFailProof.Models
{
    public partial class Banner
    {
        public int BannerID { get; set; }
        public string Position { get; set; }
        public string Image { get; set; }
        public string Label { get; set; }
        public string Button { get; set; }
        public string Link { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
