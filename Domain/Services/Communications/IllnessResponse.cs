using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class IllnessResponse : BaseResponse<Illness>
    {
        public IllnessResponse(Illness resource) : base(resource)
        {
        }

        public IllnessResponse(string message) : base(message)
        {
        }
    }
}
