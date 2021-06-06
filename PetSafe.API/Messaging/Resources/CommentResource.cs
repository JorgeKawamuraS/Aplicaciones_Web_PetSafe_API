using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class CommentResource
    {
        public int Id { get; set; }
        public int OwnerProfileId { get; set; }
        public OwnerProfileResource OwnerProfile { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int StarQuanity { get; set; }
    }
}
