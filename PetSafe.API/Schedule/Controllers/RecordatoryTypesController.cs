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
    public class RecordatoryTypesController : ControllerBase
    {
        private readonly IRecordatoryTypeService _recordatoryTypeService;
        private readonly IMapper _mapper;

        public RecordatoryTypesController(IRecordatoryTypeService recordatoryTypeService, IMapper mapper)
        {
            _recordatoryTypeService = recordatoryTypeService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RecordatoryTypeResource>), 200)]
        public async Task<IEnumerable<RecordatoryTypeResource>> GetAllAsync()
        {
            var recordatoryTypes = await _recordatoryTypeService.ListAsync();
            var resources = _mapper.Map<IEnumerable<RecordatoryType>, IEnumerable<RecordatoryTypeResource>>(recordatoryTypes);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RecordatoryTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _recordatoryTypeService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var recordatoryTypeResource = _mapper.Map<RecordatoryType, RecordatoryResource>(result.Resource);
            return Ok(recordatoryTypeResource);

        }

        [HttpPost]
        [ProducesResponseType(typeof(RecordatoryTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveRecordatoryTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var recordatoryType = _mapper.Map<SaveRecordatoryTypeResource, RecordatoryType>(resource);
            var result = await _recordatoryTypeService.SaveAsync(recordatoryType);

            if (!result.Success)
                return BadRequest(result.Message);

            var recordatoryTypeResource = _mapper.Map<RecordatoryType, RecordatoryResource>(result.Resource);
            return Ok(recordatoryTypeResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(RecordatoryTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRecordatoryTypeResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var recordatoryType = _mapper.Map<SaveRecordatoryTypeResource, RecordatoryType>(resource);
            var result = await _recordatoryTypeService.UpdateAsync(id,recordatoryType);

            if (!result.Success)
                return BadRequest(result.Message);

            var recordatoryTypeResource = _mapper.Map<RecordatoryType, RecordatoryResource>(result.Resource);
            return Ok(recordatoryTypeResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(RecordatoryTypeResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _recordatoryTypeService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var recordatoryTypeResource = _mapper.Map<RecordatoryType, RecordatoryResource>(result.Resource);
            return Ok(recordatoryTypeResource);
        }
    }
}
