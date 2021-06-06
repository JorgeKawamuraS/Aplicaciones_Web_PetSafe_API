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
    [Route("/api/")]
    [Produces("application/json")]
    public class OwnerScheduleRecordatoryTypePetRecordatoriesController : ControllerBase
    {
        private readonly IRecordatoryService _recordatoryService;
        private readonly IMapper _mapper;

        public OwnerScheduleRecordatoryTypePetRecordatoriesController(IRecordatoryService recordatoryService, IMapper mapper)
        {
            _recordatoryService = recordatoryService;
            _mapper = mapper;
        }

        [HttpPost("owners/{ownerId}/schedules/{scheduleId}/recordatoryTypes/{recordatoryTypeId}/pets/{petId}/recordatories")]
        [ProducesResponseType(typeof(RecordatoryResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int ownerId, int scheduleId, int recordatoryTypeId, int petId, [FromBody] SaveRecordatoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var recordatory = _mapper.Map<SaveRecordatoryResource, Recordatory>(resource);
            var result = await _recordatoryService.SaveOwnerAsync(ownerId, scheduleId, recordatoryTypeId, petId, recordatory);

            if (!result.Success)
                return BadRequest(result.Message);
            var recordatoriesResource = _mapper.Map<Recordatory, RecordatoryResource>(result.Resource);
            return Ok(recordatoriesResource);
        }

    }
}
