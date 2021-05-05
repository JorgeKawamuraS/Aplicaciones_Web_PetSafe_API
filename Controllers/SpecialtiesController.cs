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
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtyService _specialtyService;
        private readonly IMapper _mapper;

        public SpecialtiesController(ISpecialtyService specialtyService, IMapper mapper)
        {
            _specialtyService = specialtyService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SpecialtyResource>), 200)]
        public async Task<IEnumerable<SpecialtyResource>> GetAllAsync()
        {
            var specialties = await _specialtyService.ListAsync();
            var resurces = _mapper
                .Map<IEnumerable<Specialty>, IEnumerable<SpecialtyResource>>(specialties);
            return resurces;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SpecialtyResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _specialtyService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var specialtyResource = _mapper.Map<Specialty, SpecialtyResource>(result.Resource);
            return Ok(specialtyResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SpecialtyResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveSpecialtyResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var specialty = _mapper.Map<SaveSpecialtyResource, Specialty>(resource);
            var result = await _specialtyService.SaveAsync(specialty);

            if (!result.Success)
                return BadRequest(result.Message);
            var specialtyResource = _mapper.Map<Specialty, SpecialtyResource>(result.Resource);
            return Ok(specialtyResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(SpecialtyResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id,[FromBody] SaveSpecialtyResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var specialty = _mapper.Map<SaveSpecialtyResource, Specialty>(resource);
            var result = await _specialtyService.UpdateAsync(id,specialty);

            if (!result.Success)
                return BadRequest(result.Message);
            var specialtyResource = _mapper.Map<Specialty, SpecialtyResource>(result.Resource);
            return Ok(specialtyResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(SpecialtyResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _specialtyService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            var specialtyResource = _mapper.Map<Specialty, SpecialtyResource>(result.Resource);
            return Ok(specialtyResource);
        }
    }
}