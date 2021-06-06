using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class UserPlan
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
        public DateTime DateOfUpdate { get; set; }

    }
}