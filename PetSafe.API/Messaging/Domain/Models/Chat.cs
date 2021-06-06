using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public List<Message> Messages { get; set; }
        public int SenderProfileId { get; set; }
        public Profile SenderProfile { get; set; }
        public int ReceiverProfileId { get; set; }
        public Profile ReceiverProfile { get; set; }
        public int PetId { get; set; }
        public PetProfile Pet { get; set; }

    }
}
