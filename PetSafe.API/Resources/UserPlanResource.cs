using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class UserPlanResource
    {
        public int UserId { get; set; }
        public UserResource User { get; set; }
        public int PlanId { get; set; }
        public PlanResource Plan { get; set; }
        public DateTime DateOfUpdate { get; set; }
    }
}
