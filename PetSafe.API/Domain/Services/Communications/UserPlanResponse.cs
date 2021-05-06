using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class UserPlanResponse : BaseResponse<UserPlan>
    {
        public UserPlanResponse(UserPlan resource) : base(resource)
        {
        }

        public UserPlanResponse(string message) : base(message)
        {
        }
    }
}
