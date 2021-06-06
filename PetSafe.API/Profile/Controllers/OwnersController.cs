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
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerProfileService _ownerProfileService;
        private readonly IMapper _mapper;

        public OwnersController(IOwnerProfileService ownerProfileService, IMapper mapper)
        {
            _ownerProfileService = ownerProfileService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OwnerProfileResource>), 200)]
        public async Task<IEnumerable<OwnerProfileResource>> GetAllAsync()
        {
            var ownerProfiles = await _ownerProfileService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<OwnerProfile>, IEnumerable<OwnerProfileResource>>(ownerProfiles);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OwnerProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _ownerProfileService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var ownerProfileResource = _mapper.Map<OwnerProfile, OwnerProfileResource>(result.Resource);
            return Ok(ownerProfileResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OwnerProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveOwnerProfileResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var ownerProfile = _mapper.Map<SaveOwnerProfileResource, OwnerProfile>(resource);
            var result = await _ownerProfileService.UpdateAsync(id,ownerProfile);

            if (!result.Success)
                return BadRequest(result.Message);

            var ownerProfileResource = _mapper.Map<OwnerProfile, OwnerProfileResource>(result.Resource);
            return Ok(ownerProfileResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(OwnerProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _ownerProfileService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var ownerProfileResource = _mapper.Map<OwnerProfile, OwnerProfileResource>(result.Resource);
            return Ok(ownerProfileResource);
        }

    }
}