using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class PetTreatmentResponse : BaseResponse<PetTreatment>
    {
        public PetTreatmentResponse(PetTreatment resource) : base(resource)
        {
        }

        public PetTreatmentResponse(string message) : base(message)
        {
        }
    }
}
