using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class UserResource
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public bool UserTypeVet { get; set; }
        public List<UserPlanResource> UserPlans { get; set; }
    }
}