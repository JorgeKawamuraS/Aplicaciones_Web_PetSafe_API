using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class AppointmentResource
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public int VeterinaryId { get; set; }
        public int VetId { get; set; }
        public int PetId { get; set; }
        public DateTime Date { get; set; }
        public bool Virtual { get; set; }
        public bool Accepted { get; set; }
        public string Description { get; set; }
    }
}
