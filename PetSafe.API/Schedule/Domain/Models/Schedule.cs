using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Recordatory> Recordatories { get; set; }
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }
}
}
