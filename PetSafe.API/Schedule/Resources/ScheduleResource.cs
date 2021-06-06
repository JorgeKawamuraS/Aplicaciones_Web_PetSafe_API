using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class ScheduleResource
    {
        public int Id { get; set; }
        public List<AppointmentResource> Appointments { get; set; }
        public List<RecordatoryResource> Recordatories { get; set; }
        public int ProfileId { get; set; }
        public ProfileResource Profile { get; set; }
    }
}
