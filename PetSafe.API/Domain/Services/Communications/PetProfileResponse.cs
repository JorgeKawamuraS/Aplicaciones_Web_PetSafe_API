using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class PetProfileResponse : BaseResponse<PetProfile>
    {
        public PetProfileResponse(PetProfile resource) : base(resource)
        {
        }

        public PetProfileResponse(string message) : base(message)
        {
        }
    }
}
