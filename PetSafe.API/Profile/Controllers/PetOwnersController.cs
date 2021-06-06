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
    public class PetOwnersController : ControllerBase
    {
        private readonly IPetOwnerService _petOwnerService;
        private readonly IMapper _mapper;

        public PetOwnersController(IPetOwnerService petOwnerService, IMapper mapper)
        {
            _petOwnerService = petOwnerService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PetOwnerResource>), 200)]
        public async Task<IEnumerable<PetOwnerResource>> GetAllAsync()
        {
            var petOwners = await _petOwnerService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<PetOwner>, IEnumerable<PetOwnerResource>>(petOwners);
            return resources;
        }

        [HttpPost("pets/{petId}/owners/{ownerId}")]
        public async Task<IActionResult> AssignOwnerLocation(int petId, int ownerId, [FromBody] SavePetOwnerResource resource)
        {
            var result = await _petOwnerService.AssingPetOwner(petId,ownerId, resource.Principal);
            if (!result.Success)
                return BadRequest(result.Message);

            var ownerLocationResource = _mapper.Map<PetOwner, PetOwnerResource>(result.Resource);
            return Ok(ownerLocationResource);
        }

        [HttpDelete("pets/{petId}/owners/{ownerId}")]
        public async Task<IActionResult> UnassignOwnerLocation(int petId, int ownerId)
        {
            var result = await _petOwnerService.UnassingPetOwner(petId, ownerId);
            if (!result.Success)
                return BadRequest(result.Message);

            var ownerLocationResource = _mapper.Map<PetOwner, PetOwnerResource>(result.Resource);
            return Ok(ownerLocationResource);
        }



    }
}
