using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class CityResponse : BaseResponse<City>
    {
        public CityResponse(City resource) : base(resource)
        {
        }

        public CityResponse(string message) : base(message)
        {
        }
    }
}
