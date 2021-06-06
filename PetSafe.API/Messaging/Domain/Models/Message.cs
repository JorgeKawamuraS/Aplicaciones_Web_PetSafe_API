using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string UrlPhoto { get; set; }
        public string Text { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
