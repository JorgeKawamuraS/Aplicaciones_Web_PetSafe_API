using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class SaveAppointmentResource
    {
        public DateTime Date { get; set; }
        public bool Virtual { get; set; }
        public bool Accepted { get; set; }
        public string Description { get; set; }
    }
}
