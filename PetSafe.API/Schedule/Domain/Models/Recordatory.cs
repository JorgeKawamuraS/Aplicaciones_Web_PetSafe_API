using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class Recordatory
    {
        public int Id { get; set; }
        public int VetId{ get; set; }
        public VetProfile Vet { get; set; }
        public int OwnerId { get; set; }
        public OwnerProfile Owner { get; set; }
        public int PetId { get; set; }
        public PetProfile Pet { get; set; }
        public string Duration { get; set; }
        public string Description { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public int RecordatoryTypeId { get; set; }
        public RecordatoryType RecordatoryType { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
