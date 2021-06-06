using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Persistence.Repositories;
using PetSafe.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class SchedulesController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;
        private readonly IMapper _mapper;

        public SchedulesController(IScheduleService scheduleService, IMapper mapper)
        {
            _scheduleService = scheduleService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ScheduleResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _scheduleService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var scheduleResource = _mapper.Map<Schedule, ScheduleResource>(result.Resource);
            return Ok(scheduleResource);
        }
    }
}
