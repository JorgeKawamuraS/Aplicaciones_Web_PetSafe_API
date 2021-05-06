using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class VetProfile : Profile
    {
        public int Code { get; set; }
        public int ExperienceYear { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<VetVeterinary> VetVeterinaries { get; set; }
    }
}