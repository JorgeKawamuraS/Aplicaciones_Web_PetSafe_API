using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services;
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
    public class ScheduleRecordatoriesController : ControllerBase
    {
        private readonly IRecordatoryService _recordatoryService;
        private readonly IMapper _mapper;

        public ScheduleRecordatoriesController(IRecordatoryService recordatoryService, IMapper mapper)
        {
            _recordatoryService = recordatoryService;
            _mapper = mapper;
        }

        [HttpGet("schedules/{scheduleId}/recordatories")]
        [ProducesResponseType(typeof(IEnumerable<RecordatoryResource>), 200)]
        public async Task<IEnumerable<RecordatoryResource>> GetAllByScheduleId(int scheduleId)
        {
            var recordatories = await _recordatoryService.ListByScheduleId(scheduleId);
            var resources = _mapper.Map<IEnumerable<Recordatory>, IEnumerable<RecordatoryResource>>(recordatories);
            return resources;
        }


    }
}
