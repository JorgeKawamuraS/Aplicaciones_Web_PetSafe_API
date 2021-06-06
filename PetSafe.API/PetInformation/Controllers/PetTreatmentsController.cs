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
    public class PetTreatmentsController : ControllerBase
    {
        private readonly IPetTreatmentService _petTreatmentService;
        private readonly IMapper _mapper;

        public PetTreatmentsController(IPetTreatmentService petTreatmentService, IMapper mapper)
        {
            _petTreatmentService = petTreatmentService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PetTreatmentResource>), 200)]
        public async Task<IEnumerable<PetTreatmentResource>> GetAllAsync()
        {
            var petTreatments = await _petTreatmentService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<PetTreatment>, IEnumerable<PetTreatmentResource>>(petTreatments);
            return resources;
        }

        [HttpPost("pets/{petId}/treatments/{treatmentId}")]
        public async Task<IActionResult> AssignPetTreatment(int petId, int treatmentId, [FromBody] SavePetTreatment resource)
        {
            var result = await _petTreatmentService.AssignPetTreatmentAsync(petId,treatmentId,resource.Date);
            if (!result.Success)
                return BadRequest(result.Message);

            var petIllnesResource = _mapper.Map<PetTreatment, PetTreatmentResource>(result.Resource);
            return Ok(petIllnesResource);
        }

        [HttpDelete("pets/{petId}/treatments/{treatmentId}")]
        public async Task<IActionResult> UnassignPetTreatment(int petId, int treatmentId, [FromBody] SavePetTreatment resource)
        {
            var result = await _petTreatmentService.UnassignPetTreatmentAsync(petId,treatmentId,resource.Date);
            if (!result.Success)
                return BadRequest(result.Message);

            var petIllnesResource = _mapper.Map<PetTreatment, PetTreatmentResource>(result.Resource);
            return Ok(petIllnesResource);
        }
    }
}
