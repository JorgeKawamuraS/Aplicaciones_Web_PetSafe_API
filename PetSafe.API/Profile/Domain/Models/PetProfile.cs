using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class PetProfile : Profile
    {
        public List<PetTreatment> PetTreatments { get; set; }
        public List<PetIllness> PetIllnesses { get; set; }
        public List<PetOwner> PetOwners { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Recordatory> Recordatories { get; set; }
        public List<Chat> Chats { get; set; }
    }
}
