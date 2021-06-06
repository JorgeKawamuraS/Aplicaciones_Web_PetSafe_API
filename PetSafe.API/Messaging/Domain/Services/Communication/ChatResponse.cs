using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class ChatResponse : BaseResponse<Chat>
    {
        public ChatResponse(Chat resource) : base(resource)
        {
        }

        public ChatResponse(string message) : base(message)
        {
        }
    }
}
