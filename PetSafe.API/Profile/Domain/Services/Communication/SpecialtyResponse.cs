using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class SpecialtyResponse : BaseResponse<Specialty>
    {
        public SpecialtyResponse(Specialty resource) : base(resource)
        {
        }

        public SpecialtyResponse(string message) : base(message)
        {
        }
    }
}
