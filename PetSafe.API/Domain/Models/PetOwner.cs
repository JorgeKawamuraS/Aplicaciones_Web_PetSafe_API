using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class PetOwner
    {
        public int OwnerId { get; set; }
        public OwnerProfile OwnerProfile { get; set; }
        public int PetId { get; set; }
        public PetProfile PetProfile { get; set; }
        public bool Principal { get; set; }
    }
}
