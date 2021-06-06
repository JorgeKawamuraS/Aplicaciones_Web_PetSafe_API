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
    public class PetsController : ControllerBase
    {
        private readonly IPetProfileService _petProfileService;
        private readonly IMapper _mapper;

        public PetsController(IPetProfileService petProfileService, IMapper mapper)
        {
            _petProfileService = petProfileService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PetProfileResource>), 200)]
        public async Task<IEnumerable<PetProfileResource>> GetAllAsync()
        {
            var petProfiles = await _petProfileService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<PetProfile>, IEnumerable<PetProfileResource>>(petProfiles);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PetProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _petProfileService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var petProfileResource = _mapper.Map<PetProfile, PetProfileResource>(result.Resource);
            return Ok(petProfileResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PetProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id,[FromBody] SavePetProfileResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var petProfile = _mapper.Map<SavePetProfileResource, PetProfile>(resource);
            var result = await _petProfileService.UpdateAsync(id,petProfile);

            if (!result.Success)
                return BadRequest(result.Message);

            var petProfileResource = _mapper.Map<PetProfile, PetProfileResource>(result.Resource);
            return Ok(petProfileResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(PetProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _petProfileService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var petProfileResource = _mapper.Map<PetProfile, PetProfileResource>(result.Resource);
            return Ok(petProfileResource);
        }

    }
}