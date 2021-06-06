using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class PetProfileResource : ProfileResource
    {
        public List<PetTreatmentResource> PetTreatments { get; set; }
        public List<PetIllnessResource> PetIllnesses { get; set; }
        public List<PetOwnerResource> PetOwners { get; set; }

    }
}
