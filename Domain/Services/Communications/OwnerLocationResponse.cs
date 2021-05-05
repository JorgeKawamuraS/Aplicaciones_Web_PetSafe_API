using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class OwnerLocationResponse : BaseResponse<OwnerLocation>
    {
        public OwnerLocationResponse(string message) : base(message)
        {
        }

        public OwnerLocationResponse(OwnerLocation resource) : base(resource)
        {
        }
    }
}
