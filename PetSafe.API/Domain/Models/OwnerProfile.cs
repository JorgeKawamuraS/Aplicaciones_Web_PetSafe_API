using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class OwnerProfile : Profile
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public List<OwnerLocation> OwnerLocations { get; set; }
        public List<PetOwner> PetOwners { get; set; }
    }
}
