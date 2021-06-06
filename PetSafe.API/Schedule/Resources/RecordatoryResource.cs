using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class RecordatoryResource
    {
        public int Id { get; set; }
        public int VetId { get; set; }
        public VetProfileResource Vet { get; set; }
        public int OwnerId { get; set; }
        public OwnerProfileResource Owner { get; set; }
        public int PetId { get; set; }
        public PetProfileResource Pet { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int RecordatoryTypeId { get; set; }
        public RecordatoryTypeResource RecordatoryType { get; set; }
    }
}
