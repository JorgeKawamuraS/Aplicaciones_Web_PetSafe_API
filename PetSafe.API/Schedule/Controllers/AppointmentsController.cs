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
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IMapper _mapper;

        public AppointmentsController(IAppointmentService appointmentService, IMapper mapper)
        {
            _appointmentService = appointmentService;
            _mapper = mapper;
        }

        //Pasarlo a SchedulesController
        [HttpGet("/schedules/{scheduleId}")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentResource>), 200)]
        public async Task<IEnumerable<AppointmentResource>> GetAllAsync(int scheduleId)
        {
            var appointments = await _appointmentService.ListByScheduleId(scheduleId);
            var resources = _mapper
                .Map<IEnumerable<Appointment>, IEnumerable<AppointmentResource>>(appointments);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AppointmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _appointmentService.GetByIdAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var appointmentResource = _mapper.Map<Appointment, AppointmentResource>(result.Resource);
            return Ok(appointmentResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AppointmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveAppointmentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var appointment = _mapper.Map<SaveAppointmentResource, Appointment>(resource);
            var result = await _appointmentService.UpdateAsync(id,appointment);


            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var appointmentResource = _mapper.Map<Appointment, AppointmentResource>(result.Resource);
            return Ok(appointmentResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AppointmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _appointmentService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var appointmentResource = _mapper.Map<Appointment, AppointmentResource>(result.Resource);
            return Ok(appointmentResource);
        }

    }
}
