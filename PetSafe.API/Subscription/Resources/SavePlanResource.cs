using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class SavePlanResource
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
