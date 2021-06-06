using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int TelephonicNumber { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public List<Chat> SenderChats { get; set; }
        public List<Chat> ReceiverChats { get; set; }
        public int ScheduleId { get; set; }
        public Schedule Schedule { get; set; }
    }
}
