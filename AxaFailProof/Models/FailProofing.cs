using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AxaFailProof.Models
{
    public partial class FailProofing
    {
        public int FailID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LocationCity { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public System.DateTime BirthDate { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<bool> Status { get; set; }
    }
}
