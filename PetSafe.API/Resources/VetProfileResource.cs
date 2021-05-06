using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class VetProfileResource : ProfileResource
    {
        public int Code { get; set; }
        public int ExperienceYear { get; set; }
        public int UserId { get; set; }
        public UserResource User { get; set; }
        public List<VetVeterinaryResource> VetVeterinaries { get; set; }
    }
}
