using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class ChatResource
    {
        public int Id { get; set; }
        public List<MessageResource> Messages { get; set; }
        public int SenderProfileId { get; set; }
        public ProfileResource SenderProfile { get; set; }
        public int ReceiverProfileId { get; set; }
        public ProfileResource ReceiverProfile { get; set; }
        public int PetId { get; set; }
        public PetProfileResource PetProfile { get; set; }
    }
}
