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
    public class VeterinariesController : ControllerBase
    {
        private readonly IVeterinaryProfileService _veterinaryProfileService;
        private readonly IMapper _mapper;

        public VeterinariesController(IVeterinaryProfileService veterinaryProfileService, IMapper mapper)
        {
            _veterinaryProfileService = veterinaryProfileService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VeterinaryProfileResource>), 200)]
        public async Task<IEnumerable<VeterinaryProfileResource>> GetAllAsync()
        {
            var veterinaryProfiles = await _veterinaryProfileService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<VeterinaryProfile>, IEnumerable<VeterinaryProfileResource>>(veterinaryProfiles);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(VeterinaryProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _veterinaryProfileService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var veterinaryProfileResource = _mapper.Map<VeterinaryProfile, VeterinaryProfileResource>(result.Resource);
            return Ok(veterinaryProfileResource);

        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(VeterinaryProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id,[FromBody] SaveVeterinaryProfileResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var veterinaryProfile = _mapper.Map<SaveVeterinaryProfileResource, VeterinaryProfile>(resource);
            var result = await _veterinaryProfileService.UpdateAsync(id,veterinaryProfile);

            if (!result.Success)
                return BadRequest(result.Message);

            var veterinaryProfileResource = _mapper.Map<VeterinaryProfile, VeterinaryProfileResource>(result.Resource);
            return Ok(veterinaryProfileResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(VeterinaryProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        { 
            var result = await _veterinaryProfileService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var veterinaryProfileResource = _mapper.Map<VeterinaryProfile, VeterinaryProfileResource>(result.Resource);
            return Ok(veterinaryProfileResource);
        }


    }
}