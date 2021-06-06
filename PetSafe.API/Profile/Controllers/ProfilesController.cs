using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
    public class ProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IMapper _mapper;

        public ProfilesController(IProfileService profileService, IMapper mapper)
        {
            _profileService = profileService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProfileResource>), 200)]
        public async Task<IEnumerable<ProfileResource>> GetAllAsync()
        {
            var profiles = await _profileService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Domain.Models.Profile>, IEnumerable<ProfileResource>>(profiles);
            return resources;
        }


        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProfileResource),200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _profileService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var profileResource = _mapper.Map<Domain.Models.Profile, ProfileResource>(result.Resource);
            return Ok(profileResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id,[FromBody] SaveProfileResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var profile = _mapper.Map<SaveProfileResource, Domain.Models.Profile>(resource);
            var result = await _profileService.UpdateAsync(id,profile);
            if (!result.Success)
                return BadRequest(result.Message);

            var profileResource = _mapper.Map<Domain.Models.Profile, ProfileResource>(result.Resource);
            return Ok(profileResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _profileService.DeleteAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var profileResource = _mapper.Map<Domain.Models.Profile, ProfileResource>(result.Resource);
            return Ok(profileResource);
        }

    }
}
