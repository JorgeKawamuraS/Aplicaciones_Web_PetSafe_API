using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class PetIllnessResponse : BaseResponse<PetIllness>
    {
        public PetIllnessResponse(PetIllness resource) : base(resource)
        {
        }

        public PetIllnessResponse(string message) : base(message)
        {
        }
    }
}
