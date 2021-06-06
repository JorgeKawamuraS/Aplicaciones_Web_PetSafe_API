using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int OwnerProfileId { get; set; }
        public OwnerProfile OwnerProfile { get; set; }
        public int VeterinaryProfileId { get; set; }
        public VeterinaryProfile VeterinaryProfile { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int StarsQuantity { get; set; }

    }
}
