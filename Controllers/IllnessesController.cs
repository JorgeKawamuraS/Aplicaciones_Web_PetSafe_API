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
    public class IllnessesController : ControllerBase
    {
        private readonly IIllnessService _illnessService;
        private readonly IMapper _mapper;

        public IllnessesController(IIllnessService illnessService, IMapper mapper)
        {
            _illnessService = illnessService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<IllnessResource>> GetAllAsync()
        {
            var illnesses = await _illnessService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<Illness>, IEnumerable<IllnessResource>>(illnesses);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IllnessResource),200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _illnessService.GetByIdAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var illnessResource = _mapper.Map<Illness, IllnessResource>(result.Resource);
            return Ok(illnessResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(IllnessResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveIllnessResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var illness = _mapper.Map<SaveIllnessResource, Illness>(resource);
            var result = await _illnessService.SaveAsync(illness);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var illnessResource = _mapper.Map<Illness, IllnessResource>(result.Resource);
            return Ok(illnessResource);
        }

        [HttpPut]
        [ProducesResponseType(typeof(IllnessResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveIllnessResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var illness = _mapper.Map<SaveIllnessResource, Illness>(resource);
            var result = await _illnessService.UpdateAsync(id,illness);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var illnessResource = _mapper.Map<Illness, IllnessResource>(result.Resource);
            return Ok(illnessResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(IllnessResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _illnessService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var illnessResource = _mapper.Map<Illness, IllnessResource>(result.Resource);
            return Ok(illnessResource);
        }


    }
}
