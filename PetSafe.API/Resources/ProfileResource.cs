using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class ProfileResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int TelephonicNumber { get; set; }
        public int CityId { get; set; }
        public CityResource City { get; set; }
        public int ProvinceId { get; set; }
        public ProvinceResource Province { get; set; }
    }
}
