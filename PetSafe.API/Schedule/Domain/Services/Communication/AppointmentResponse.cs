using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services.Communications
{
    public class AppointmentResponse : BaseResponse<Appointment>
    {
        public AppointmentResponse(Appointment resource) : base(resource)
        {
        }

        public AppointmentResponse(string message) : base(message)
        {
        }
    }
}
