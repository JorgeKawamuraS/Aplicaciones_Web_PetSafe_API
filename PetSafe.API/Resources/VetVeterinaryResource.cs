using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class VetVeterinaryResource
    {
        public int VeterinaryId { get; set; }
        public VeterinaryProfileResource VeterinaryProfile { get; set; }
        public int VetId { get; set; }
        public VetProfileResource VetProfile { get; set; }
        public bool Own { get; set; }
    }
}
