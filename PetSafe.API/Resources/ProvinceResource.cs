using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class ProvinceResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CityResource> Cities { get; set; }
        public List<ProfileResource> Profiles { get; set; }
    }
}
