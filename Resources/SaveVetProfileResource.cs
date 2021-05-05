using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class SaveVetProfileResource : SaveProfileResource
    {
        [Required]
        public int Code { get; set; }

        [Required]
        public int ExperienceYear { get; set; }
    }
}
