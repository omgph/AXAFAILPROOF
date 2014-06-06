using System;
using System.Collections.Generic;

namespace AxaFailProof.Models
{
    public partial class Location
    {
        public int LocationID { get; set; }
        public string Event { get; set; }
        public string EventDescription { get; set; }
        public string EventDate { get; set; }
        public string EventTime { get; set; }
        public string Location1 { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> Status { get; set; }
        public virtual ICollection<EventsRegistered> EventsRegistereds { get; set; }
    }
}
