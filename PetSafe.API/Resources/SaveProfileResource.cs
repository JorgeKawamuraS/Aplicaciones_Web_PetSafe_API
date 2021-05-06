using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class SaveProfileResource
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int TelephonicNumber { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public int ProvinceId { get; set; }
    }
}
