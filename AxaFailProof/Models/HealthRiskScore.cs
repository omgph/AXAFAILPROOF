using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AxaFailProof.Models
{
    public partial class HealthRiskScore
    {
        public int ID { get; set; }
        public string Bday { get; set; }
        public string Gender { get; set; }
        public string Smoker { get; set; }
        public int Pounds { get; set; }
        public int Feet { get; set; }
        public int Inches { get; set; }
        public string ParentsIllness { get; set; }
        public string YouIllness { get; set; }
        public int Alcohol { get; set; }
        public int Coffee { get; set; }
        public string Exercise { get; set; }
        public int Income { get; set; }
        public int Savings { get; set; }
        public string SaveFor { get; set; }
        public string Owned { get; set; }
        public string Email { get; set; }
    }
}
