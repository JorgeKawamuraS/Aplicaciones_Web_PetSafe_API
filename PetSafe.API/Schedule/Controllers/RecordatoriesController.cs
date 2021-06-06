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
    public class RecordatoriesController : ControllerBase
    {
        private readonly IRecordatoryService _recordatoryService;
        private readonly IMapper _mapper;

        public RecordatoriesController(IRecordatoryService recordatoryService, IMapper mapper)
        {
            _recordatoryService = recordatoryService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RecordatoryResource),200)]
        [ProducesResponseType(typeof(BadRequestResult),404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _recordatoryService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var recordatoriesResource = _mapper.Map<Recordatory, RecordatoryResource>(result.Resource);
            return Ok(recordatoriesResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RecordatoryResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRecordatoryResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var recordatory = _mapper.Map<SaveRecordatoryResource, Recordatory>(resource);
            var result = await _recordatoryService.UpdateAsync(id,recordatory);

            if (!result.Success)
                return BadRequest(result.Message);
            var recordatoriesResource = _mapper.Map<Recordatory, RecordatoryResource>(result.Resource);
            return Ok(recordatoriesResource);

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(RecordatoryResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _recordatoryService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            var recordatoriesResource = _mapper.Map<Recordatory, RecordatoryResource>(result.Resource);
            return Ok(recordatoriesResource);

        }

    }
}
