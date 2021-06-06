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
    public class VetVeterinariesController : ControllerBase
    {
        private readonly IVetVeterinaryService _vetVeterinaryService;
        private readonly IMapper _mapper;

        public VetVeterinariesController(IVetVeterinaryService vetVeterinaryService, IMapper mapper)
        {
            _vetVeterinaryService = vetVeterinaryService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<VetVeterinaryResource>), 200)]
        public async Task<IEnumerable<VetVeterinaryResource>> GetAllAsync()
        {
            var vetVeterinaries= await _vetVeterinaryService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<VetVeterinary>, IEnumerable<VetVeterinaryResource>>(vetVeterinaries);
            return resources;
        }

        [HttpPost("veterinary/{veterinaryId}/vet/{vetId}")]
        public async Task<IActionResult> AssignVetVeterinary(int veterinaryId, int vetId)
        {
            var result = await _vetVeterinaryService.AssignVetVeterinaryAsync(vetId,veterinaryId);
            if (!result.Success)
                return BadRequest(result.Message);

            var vetVeterinaryResource = _mapper.Map<VetVeterinary, VetVeterinaryResource>(result.Resource);
            return Ok(vetVeterinaryResource);
        }

        [HttpDelete("veterinary/{veterinaryId}/vet/{vetId}")]
        public async Task<IActionResult> UnassignVeterinarySpecialty(int veterinaryId, int vetId)
        {
            var result = await _vetVeterinaryService.UnassignVetVeterinaryAsync(vetId, veterinaryId);
            if (!result.Success)
                return BadRequest(result.Message);

            var vetVeterinaryResource = _mapper.Map<VetVeterinary, VetVeterinaryResource>(result.Resource);
            return Ok(vetVeterinaryResource);
        }



    }
}
