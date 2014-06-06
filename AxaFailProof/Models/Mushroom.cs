using System;
using System.Collections.Generic;

namespace AxaFailProof.Models
{
    public partial class Mushroom
    {
        public int MushroomID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
