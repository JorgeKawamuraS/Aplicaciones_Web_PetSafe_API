using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class PlanResponse : BaseResponse<Plan>
    {
        public PlanResponse(Plan resource) : base(resource)
        {
        }

        public PlanResponse(string message) : base(message)
        {
        }
    }
}
