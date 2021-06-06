using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class RecordatoryResponse : BaseResponse<Recordatory>
    {
        public RecordatoryResponse(Recordatory resource) : base(resource)
        {
        }

        public RecordatoryResponse(string message) : base(message)
        {
        }
    }
}
