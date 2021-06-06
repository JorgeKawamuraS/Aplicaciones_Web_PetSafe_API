using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class VeterinaryProfileResource : ProfileResource
    {
        public List<VetVeterinaryResource> VetVeterinaries { get; set; }
        public List<VeterinarySpecialtyResource> VeterinarySpecialties { get; set; }
        public List<CommentResource> Comments { get; set; }
    }
}
