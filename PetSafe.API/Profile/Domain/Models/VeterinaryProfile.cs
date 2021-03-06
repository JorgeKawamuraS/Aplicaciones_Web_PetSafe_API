using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class VeterinaryProfile : Profile
    {
        public List<VetVeterinary> VetVeterinaries { get; set; }
        public List<VeterinarySpecialty> VeterinarySpecialties { get; set; }
        public List<Comment> Comments {get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
