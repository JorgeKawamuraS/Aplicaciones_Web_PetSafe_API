using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class PetTreatment
    {
        public int PetId { get; set; }
        public PetProfile PetProfile { get; set;}
        public int TreatmentId { get; set;}
        public Treatment Treatment { get; set;}
        public DateTime Date { get; set; }
    }
}