using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AxaFailProof.Models
{
    public partial class EventsRegistered
    {
        public int RegEventID { get; set; }
        public int LocationID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LocationCity { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public System.DateTime BirthDate { get; set; }
        public Nullable<bool> CardHolder { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> Status { get; set; }
        public Nullable<bool> Terms { get; set; }
        public virtual Location Location { get; set; }
    }
}
