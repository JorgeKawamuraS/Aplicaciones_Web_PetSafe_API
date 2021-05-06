using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class Illness
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PetIllness> PetIllnesses { get; set; }
    }
}
