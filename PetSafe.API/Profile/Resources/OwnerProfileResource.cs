using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class OwnerProfileResource : ProfileResource
    {
        public int UserId { get; set; }
        public UserResource User { get; set; }
        public List<PetOwnerResource> PetOwners { get; set; }
        public List<OwnerLocationResource> OwnerLocations { get; set; }

    }
}
