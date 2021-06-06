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
    public class OwnerLocationsController : ControllerBase
    {
        private readonly IOwnerLocationService _ownerLocationService;
        private readonly IMapper _mapper;

        public OwnerLocationsController(IOwnerLocationService ownerLocationService, IMapper mapper)
        {
            _ownerLocationService = ownerLocationService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OwnerLocationResource>), 200)]
        public async Task<IEnumerable<OwnerLocationResource>> GetAllAsync()
        {
            var ownerLocations = await _ownerLocationService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<OwnerLocation>, IEnumerable<OwnerLocationResource>>(ownerLocations);
            return resources;
        }

        [HttpPost("provinces/{provinceId}/cities/{cityId}/owners/{ownerId}")]
        public async Task<IActionResult> AssignOwnerLocation(int provinceId, int cityId, int ownerId, [FromBody] SaveOwnerLocationResource resource)
        {
            var result = await _ownerLocationService.AssingOwnerLocation(ownerId, provinceId, cityId, resource.Date);
            if (!result.Success)
                return BadRequest(result.Message);

            var ownerLocationResource = _mapper.Map<OwnerLocation, OwnerLocationResource>(result.Resource);
            return Ok(ownerLocationResource);
        }

        [HttpDelete("provinces/{provinceId}/cities/{cityId}/owners/{ownerId}")]
        public async Task<IActionResult> UnassignOwnerLocation(int provinceId, int cityId, int ownerId, [FromBody] SaveOwnerLocationResource resource)
        {
            var result = await _ownerLocationService.UnassingOwnerLocation(ownerId, provinceId, cityId, resource.Date);
            if (!result.Success)
                return BadRequest(result.Message);

            var ownerLocationResource = _mapper.Map<OwnerLocation, OwnerLocationResource>(result.Resource);
            return Ok(ownerLocationResource);
        }
    }
}
