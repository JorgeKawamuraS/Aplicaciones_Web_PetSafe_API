using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public OwnerProfile Owner{ get; set; }
        public int VeterinaryId { get; set; }
        public VeterinaryProfile Veterinary { get; set; }
        public int VetId { get; set; }
        public VetProfile Vet { get; set; }
        public int PetId { get; set; }
        public PetProfile PetProfile { get; set; }
        public DateTime Date { get; set; }
        public bool Virtual { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
        public bool Accepted { get; set; }
        public string Description { get; set; }
    }
}