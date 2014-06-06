using System;
using System.Collections.Generic;

namespace AxaFailProof.Models
{
    public partial class SocialUser
    {
        public string SocialID { get; set; }
        public string SocialMedia { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    }
}
