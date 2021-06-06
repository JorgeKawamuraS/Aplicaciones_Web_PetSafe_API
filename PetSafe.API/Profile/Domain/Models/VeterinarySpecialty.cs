using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class VeterinarySpecialty
    {
        public int SpecialtyId { get; set; }
        public Specialty Specialty { get; set; }
        public int VeterinaryId { get; set; }
        public VeterinaryProfile VeterinaryProfile { get; set; }

    }
}
