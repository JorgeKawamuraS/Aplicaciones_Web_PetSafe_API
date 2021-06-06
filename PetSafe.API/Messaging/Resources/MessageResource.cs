using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Resources
{
    public class MessageResource
    {
        public int Id { get; set; }
        public string UrlPhoto { get; set; }
        public string Text { get; set; }
    }
}
