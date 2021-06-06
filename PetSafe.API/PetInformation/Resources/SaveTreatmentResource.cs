using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class SaveTreatmentResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }
    }
}
