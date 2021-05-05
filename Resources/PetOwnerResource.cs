using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class PetOwnerResource
    {
        public int OwnerId { get; set; }
        public OwnerProfileResource OwnerProfile { get; set; }
        public int PetId { get; set; }
        public PetProfileResource PetProfile { get; set; }
        public bool Principal { get; set; }
    }
}
