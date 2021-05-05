using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class VetVeterinaryResponse : BaseResponse<VetVeterinary>
    {
        public VetVeterinaryResponse(VetVeterinary resource) : base(resource)
        {
        }

        public VetVeterinaryResponse(string message) : base(message)
        {
        }
    }
}
