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
    public class PetIllnessesController : ControllerBase
    {
        private readonly IPetIllnessService _petIllnessService;
        private readonly IMapper _mapper;

        public PetIllnessesController(IPetIllnessService petIllnessService, IMapper mapper)
        {
            _petIllnessService = petIllnessService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PetIllnessResource>), 200)]
        public async Task<IEnumerable<PetIllnessResource>> GetAllAsync()
        {
            var petIllnesses = await _petIllnessService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<PetIllness>, IEnumerable<PetIllnessResource>>(petIllnesses);
            return resources;
        }

        [HttpPost("pets/{petId}/illnesses/{illnessId}")]
        public async Task<IActionResult> AssignPetIllness(int petId, int illnessId)
        {
            var result = await _petIllnessService.AssignPetIllness(petId,illnessId);
            if (!result.Success)
                return BadRequest(result.Message);

            var petIllnesResource = _mapper.Map<PetIllness, PetIllnessResource>(result.Resource);
            return Ok(petIllnesResource);
        }

        [HttpDelete("pets/{petId}/illnesses/{illnessId}")]
        public async Task<IActionResult> UnassignPetIllness(int petId, int illnessId)
        {
            var result = await _petIllnessService.UnassignPetIllness(petId, illnessId);
            if (!result.Success)
                return BadRequest(result.Message);

            var petIllnesResource = _mapper.Map<PetIllness, PetIllnessResource>(result.Resource);
            return Ok(petIllnesResource);
        }

    }
}
