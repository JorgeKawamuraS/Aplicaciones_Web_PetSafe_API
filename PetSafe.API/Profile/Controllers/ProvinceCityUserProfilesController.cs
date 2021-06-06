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
    [Route("/api/provinces/{provinceId}/cities/{cityId}/users/{userId}")]
    [Produces("application/json")]
    public class ProvinceCityUserProfilesController : ControllerBase
    {
        private readonly IVetProfileService _vetProfileService;
        private readonly IOwnerProfileService _ownerProfileService;
        private readonly IMapper _mapper;
        public ProvinceCityUserProfilesController(IVetProfileService vetProfileService, IOwnerProfileService ownerProfileService, IMapper mapper)
        {
            _vetProfileService = vetProfileService;
            _ownerProfileService = ownerProfileService;
            _mapper = mapper;
        }

        [HttpPost("/owners")]
        [ProducesResponseType(typeof(OwnerProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int provinceId, int cityId, int userId,[FromBody] SaveOwnerProfileResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var ownerProfile = _mapper.Map<SaveOwnerProfileResource, OwnerProfile>(resource);
            var result = await _ownerProfileService.SaveAsync(provinceId,cityId,userId,ownerProfile);

            if (!result.Success)
                return BadRequest(result.Message);

            var ownerProfileResource = _mapper.Map<OwnerProfile, OwnerProfileResource>(result.Resource);
            return Ok(ownerProfileResource);
        }

        [HttpPost("/vets")]
        [ProducesResponseType(typeof(VetProfileResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int provinceId, int cityId, int userId, [FromBody] SaveVetProfileResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var vetProfile = _mapper.Map<SaveVetProfileResource, VetProfile>(resource);
            var result = await _vetProfileService.SaveAsync(provinceId, cityId, userId, vetProfile);

            if (!result.Success)
                return BadRequest(result.Message);

            var vetProfileResource = _mapper.Map<VetProfile, VetProfileResource>(result.Resource);
            return Ok(vetProfileResource);
        }



    }
}
