using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class VeterinarySpecialtyResource
    {
        public int SpecialtyId { get; set; }
        public SpecialtyResource Specialty { get; set; }
        public int VeterinaryId { get; set; }
        public VeterinaryProfileResource VeterinaryProfile { get; set; }
    }
}
