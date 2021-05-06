using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class VeterinaryProfileResponse : BaseResponse<VeterinaryProfile>
    {
        public VeterinaryProfileResponse(VeterinaryProfile resource) : base(resource)
        {
        }

        public VeterinaryProfileResponse(string message) : base(message)
        {
        }
    }
}
