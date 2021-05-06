using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class VetVeterinary
    {
        public int VeterinaryId { get; set; }
        public VeterinaryProfile VeterinaryProfile { get; set; }
        public int VetId { get; set; }
        public VetProfile VetProfile { get; set; }
        public bool Own { get; set; }
    }
}
