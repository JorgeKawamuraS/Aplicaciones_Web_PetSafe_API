using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool UserTypeVet { get; set; }
        public List<UserPlan> UserPlans { get; set; }
        public int OwnerId { get; set; }
        public OwnerProfile OwnerProfile { get; set; }
        public int VetId { get; set; }
        public VetProfile VetProfile { get; set; }
    }
}