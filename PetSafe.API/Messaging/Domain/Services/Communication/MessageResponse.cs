using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class MessageResponse : BaseResponse<Message>
    {
        public MessageResponse(Message resource) : base(resource)
        {
        }

        public MessageResponse(string message) : base(message)
        {
        }
    }
}
