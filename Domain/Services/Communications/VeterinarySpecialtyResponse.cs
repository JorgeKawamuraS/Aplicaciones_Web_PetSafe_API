using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class VeterinarySpecialtyResponse : BaseResponse<VeterinarySpecialty>
    {
        public VeterinarySpecialtyResponse(VeterinarySpecialty resource) : base(resource)
        {
        }

        public VeterinarySpecialtyResponse(string message) : base(message)
        {
        }
    }
}
