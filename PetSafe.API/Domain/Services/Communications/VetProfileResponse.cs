using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class VetProfileResponse : BaseResponse<VetProfile>
    {
        public VetProfileResponse(VetProfile resource) : base(resource)
        {
        }

        public VetProfileResponse(string message) : base(message)
        {
        }
    }
}
