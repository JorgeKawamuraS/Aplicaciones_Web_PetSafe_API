using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class OwnerLocationResource
    {
        public int CityId { get; set; }
        public int ProvinceId { get; set; }
        public int OwnerId { get; set; }
        public DateTime Date { get; set; }
    }
}
