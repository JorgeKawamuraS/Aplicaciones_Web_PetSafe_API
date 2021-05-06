using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class TreatmentResponse : BaseResponse<Treatment>
    {
        public TreatmentResponse(Treatment resource) : base(resource)
        {
        }

        public TreatmentResponse(string message) : base(message)
        {
        }
    }
}
