using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class RecordatoryTypeResponse : BaseResponse<RecordatoryType>
    {
        public RecordatoryTypeResponse(RecordatoryType resource) : base(resource)
        {
        }

        public RecordatoryTypeResponse(string message) : base(message)
        {
        }
    }
}
