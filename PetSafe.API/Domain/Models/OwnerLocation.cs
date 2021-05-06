using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class OwnerLocation
    {
        public int CityId { get; set; }
        public City City { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public int OwnerId { get; set; }
        public OwnerProfile OwnerProfile { get; set; }
        public DateTime Date { get; set; }
    }
}
