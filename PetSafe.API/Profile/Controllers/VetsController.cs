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
    public class VetsController : ControllerBase
    {
        private readonly IVetProfileService _vetProfileService;
        private readonly IMapper _mapper;

        public VetsController(IVetProfileService vetProfileService, IMapper mapper)
        {
            _vetProfileService = vetProfileService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VetProfileResource>),200)]
        public async Task<IEnumerable<VetProfileResource>> GetAllAsync()
        {
            var vetProfiles = await _vetProfileService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<VetProfile>, IEnumerable<VetProfileResource>>(vetProfiles);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(VetProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _vetProfileService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var vetProfileResource = _mapper.Map<VetProfile, VetProfileResource>(result.Resource);
            return Ok(vetProfileResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(VetProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id,[FromBody] SaveVetProfileResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var vetProfile = _mapper.Map<SaveVetProfileResource, VetProfile>(resource);
            var result = await _vetProfileService.UpdateAsync(id,vetProfile);

            if (!result.Success)
                return BadRequest(result.Message);

            var vetProfileResource = _mapper.Map<VetProfile, VetProfileResource>(result.Resource);
            return Ok(vetProfileResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(VetProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _vetProfileService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var vetProfileResource = _mapper.Map<VetProfile, VetProfileResource>(result.Resource);
            return Ok(vetProfileResource);
        }

    }
}