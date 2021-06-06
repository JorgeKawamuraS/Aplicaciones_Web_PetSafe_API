using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class PetTreatmentResource
    {
        public int PetId { get; set; }
        public PetProfileResource PetProfile { get; set; }
        public int TreatmentId { get; set; }
        public TreatmentResource Treatment { get; set; }
        public DateTime Date { get; set; }
    }
}
