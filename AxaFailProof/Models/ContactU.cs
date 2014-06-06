using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AxaFailProof.Models
{
    public partial class ContactU
    {
        public int ContactID { get; set; }
        [Required(ErrorMessage = "Please enter your name.")]
        public string Name { get; set; }
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid email address format.")]
        [Required(ErrorMessage = "Please enter your email address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your message.")]
        public string Message { get; set; }
        public System.DateTime DateCreated { get; set; }
    }
}
