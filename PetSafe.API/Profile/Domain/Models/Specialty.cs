using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<VeterinarySpecialty> VeterinarySpecialties { get; set; }
    }
}
