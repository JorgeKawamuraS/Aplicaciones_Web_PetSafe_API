using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public List<OwnerLocation> OwnerLocations { get; set; }
        public List<Profile> Profiles { get; set; }
    }
}
