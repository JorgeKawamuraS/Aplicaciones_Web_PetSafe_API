using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class ProvinceResponse : BaseResponse<Province>
    {
        public ProvinceResponse(Province resource) : base(resource)
        {
        }

        public ProvinceResponse(string message) : base(message)
        {
        }
    }
}
