using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class SavePetOwnerResource
    {
        [Required]
        public bool Principal { get; set; }
    }
}
