using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class SaveOwnerLocationResource
    {
        [Required]
        public DateTime Date{ get; set; }
    }
}
