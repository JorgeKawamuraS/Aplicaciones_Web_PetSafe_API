using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class PetIllnessResource
    {
        public int PetId { get; set; }
        public int IllnesstId { get; set; }
        public IllnessResource Illness { get; set; }
    }
}
