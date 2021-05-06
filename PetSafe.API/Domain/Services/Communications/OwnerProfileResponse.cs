using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class OwnerProfileResponse : BaseResponse<OwnerProfile>
    {
        public OwnerProfileResponse(OwnerProfile resource) : base(resource)
        {
        }

        public OwnerProfileResponse(string message) : base(message)
        {
        }
    }
}
