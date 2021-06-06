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
    public class VeterinarySpecialtiesController : ControllerBase
    {
        private readonly IVeterinarySpecialtyService _veterinarySpecialtyService;
        private readonly IMapper _mapper;

        public VeterinarySpecialtiesController(IVeterinarySpecialtyService veterinarySpecialtyService, IMapper mapper)
        {
            _veterinarySpecialtyService = veterinarySpecialtyService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VeterinarySpecialtyResource>), 200)]
        public async Task<IEnumerable<VeterinarySpecialtyResource>> GetAllAsync()
        {
            var veterinarySpecialties = await _veterinarySpecialtyService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<VeterinarySpecialty>, IEnumerable<VeterinarySpecialtyResource>>(veterinarySpecialties);
            return resources;
        }

        [HttpPost("veterinary/{veterinaryId}/specialty/{specialtyId}")]
        public async Task<IActionResult> AssignVeterinarySpecialty(int veterinaryId, int specialtyId)
        {
            var result = await _veterinarySpecialtyService.AssignVeterinarySpecialtyAsync(veterinaryId,specialtyId);
            if (!result.Success)
                return BadRequest(result.Message);

            var veterinarySpecialtyResource = _mapper.Map<VeterinarySpecialty, VeterinarySpecialtyResource>(result.Resource);
            return Ok(veterinarySpecialtyResource);
        }

        [HttpDelete("veterinary/{veterinaryId}/specialty/{specialtyId}")]
        public async Task<IActionResult> UnassignVeterinarySpecialty(int veterinaryId, int specialtyId)
        {
            var result = await _veterinarySpecialtyService.UnassignVeterinarySpecialtyAsync(veterinaryId, specialtyId);
            if (!result.Success)
                return BadRequest(result.Message);

            var veterinarySpecialtyResource = _mapper.Map<VeterinarySpecialty, VeterinarySpecialtyResource>(result.Resource);
            return Ok(veterinarySpecialtyResource);
        }

    }
}