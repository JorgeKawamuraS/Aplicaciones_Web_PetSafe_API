using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class PetOwnerResponse : BaseResponse<PetOwner>
    {
        public PetOwnerResponse(PetOwner resource) : base(resource)
        {
        }

        public PetOwnerResponse(string message) : base(message)
        {
        }
    }
}
