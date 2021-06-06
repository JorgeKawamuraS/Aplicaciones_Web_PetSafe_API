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
    [Route("/api/provinces/{provinceId}/cities/{cityId}")]
    [Produces("application/json")]
    public class ProvinceCityProfilesController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IVeterinaryProfileService _veterinaryProfileService;
        private readonly IPetProfileService _petProfileService;
        private readonly IMapper _mapper;

        public ProvinceCityProfilesController(IProfileService profileService, IVeterinaryProfileService veterinaryProfileService, 
            IPetProfileService petProfileService, IMapper mapper)
        {
            _profileService = profileService;
            _veterinaryProfileService = veterinaryProfileService;
            _petProfileService = petProfileService;
            _mapper = mapper;
        }


        [HttpPost("/profiles")]
        [ProducesResponseType(typeof(ProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int provinceId,int cityId,[FromBody] SaveProfileResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var profile = _mapper.Map<SaveProfileResource, Domain.Models.Profile>(resource);
            var result = await _profileService.SaveAsync(cityId,provinceId,profile);
            if (!result.Success)
                return BadRequest(result.Message);

            var profileResource = _mapper.Map<Domain.Models.Profile, ProfileResource>(result.Resource);
            return Ok(profileResource);
        }

        [HttpPost("/pets")]
        [ProducesResponseType(typeof(PetProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int provinceId, int cityId, [FromBody] SavePetProfileResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var petProfile = _mapper.Map<SavePetProfileResource, PetProfile>(resource);
            var result = await _petProfileService.SaveAsync(cityId, provinceId, petProfile);

            if (!result.Success)
                return BadRequest(result.Message);

            var petProfileResource = _mapper.Map<PetProfile, PetProfileResource>(result.Resource);
            return Ok(petProfileResource);
        }

        [HttpPost("/veterinaries")]
        [ProducesResponseType(typeof(VeterinaryProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int provinceId, int cityId, [FromBody] SaveVeterinaryProfileResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var veterinaryProfile = _mapper.Map<SaveVeterinaryProfileResource, VeterinaryProfile>(resource);
            var result = await _veterinaryProfileService.SaveAsync(cityId, provinceId, veterinaryProfile);

            if (!result.Success)
                return BadRequest(result.Message);

            var veterinaryProfileResource = _mapper.Map<VeterinaryProfile, VeterinaryProfileResource>(result.Resource);
            return Ok(veterinaryProfileResource);

        }




    }
}
