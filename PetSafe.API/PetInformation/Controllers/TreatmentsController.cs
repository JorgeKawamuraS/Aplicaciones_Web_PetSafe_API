using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Persistence.Repositories;
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
    public class TreatmentsController : ControllerBase
    {
        private readonly ITreatmentService _treatmentService;
        private readonly IMapper _mapper;

        public TreatmentsController(ITreatmentService treatmentService, IMapper mapper)
        {
            _treatmentService = treatmentService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TreatmentResource>),200)]
        public async Task<IEnumerable<TreatmentResource>> GetAllAsync()
        {
            var treatments = await _treatmentService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Treatment>, IEnumerable<TreatmentResource>>(treatments);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TreatmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _treatmentService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var treatmentResource = _mapper.Map<Treatment, TreatmentResource>(result.Resource);
            return Ok(treatmentResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TreatmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveTreatmentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var treatment = _mapper.Map<SaveTreatmentResource, Treatment>(resource);
            var result = await _treatmentService.SaveAsync(treatment);

            if (!result.Success)
                return BadRequest(result.Message);

            var treatmentResource = _mapper.Map<Treatment, TreatmentResource>(result.Resource);
            return Ok(treatmentResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TreatmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id,[FromBody] SaveTreatmentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var treatment = _mapper.Map<SaveTreatmentResource, Treatment>(resource);
            var result = await _treatmentService.UpdateAsync(id,treatment);

            if (!result.Success)
                return BadRequest(result.Message);

            var treatmentResource = _mapper.Map<Treatment, TreatmentResource>(result.Resource);
            return Ok(treatmentResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(TreatmentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _treatmentService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var treatmentResource = _mapper.Map<Treatment, TreatmentResource>(result.Resource);
            return Ok(treatmentResource);
        }
    }
}