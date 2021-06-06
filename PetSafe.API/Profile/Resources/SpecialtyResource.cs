using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class SpecialtyResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<VeterinarySpecialtyResource> VeterinarySpecialties { get; set; }
    }
}
