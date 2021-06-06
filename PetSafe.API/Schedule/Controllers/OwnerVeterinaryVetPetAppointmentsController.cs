using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services;
using PetSafe.API.Resources;
using Supermarket.API.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Controllers
{
    [ApiController]
    [Route("/api/owners/{ownerId}/veterinaries/{veterinaryId}/vet/{vetId}/pet/{petId}")]
    [Produces("application/json")]
    public class OwnerVeterinaryVetPetAppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public OwnerVeterinaryVetPetAppointmentsController(IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        [HttpPost("/appointments")]
        [ProducesResponseType(typeof(AppointmentResource),200)]
        [ProducesResponseType(typeof(BadRequestResult), 200)]
        public async Task<IActionResult> PostAsync(int ownerId, int veterinaryId, int vetId, int petId, [FromBody] SaveAppointmentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var appointment = _mapper.Map<SaveAppointmentResource, Appointment>(resource);
            var result = await _appointmentService.SaveAsync(ownerId,veterinaryId,vetId,petId,appointment);

            if (!result.Success)
                return BadRequest(result.Message);

            var appointmentResource = _mapper.Map<Appointment, AppointmentResource>(result.Resource);

            return Ok(appointmentResource);
        }

    }
}
