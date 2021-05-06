using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class Province
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<City> Cities { get; set; }
        public List<OwnerLocation> OwnerLocations { get; set; }
        public List<Profile> Profiles { get; set; }
    }
}
