using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class PetIllness
    {
        public int PetId { get; set; }
        public PetProfile PetProfile { get; set; }
        public int IllnesstId { get; set; }
        public Illness Illness { get; set; }
    }
}
