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
    public class ProvincesController : ControllerBase
    {
        private readonly IProvinceService _provinceService;
        private readonly IMapper _mapper;

        public ProvincesController(IProvinceService provinceService, IMapper mapper)
        {
            _provinceService = provinceService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProvinceResource>), 200)]
        public async Task<IEnumerable<ProvinceResource>> GetAllAsync()
        {
            var provinces = await _provinceService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Province>, IEnumerable<ProvinceResource>>(provinces);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProvinceResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _provinceService.GetByIdAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var provinceResource = _mapper.Map<Province, ProvinceResource>(result.Resource);
            return Ok(provinceResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ProvinceResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync([FromBody] SaveProvinceResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var province = _mapper.Map<SaveProvinceResource, Province>(resource);
            var result = await _provinceService.SaveAsync(province);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var provinceResource = _mapper.Map<Province, ProvinceResource>(result.Resource);
            return Ok(provinceResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ProvinceResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveProvinceResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var province = _mapper.Map<SaveProvinceResource, Province>(resource);
            var result = await _provinceService.UpdateAsync(id, province);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var provinceResource = _mapper.Map<Province, ProvinceResource>(result.Resource);
            return Ok(provinceResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ProvinceResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _provinceService.DeleteAsync(id);

            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var provinceResource = _mapper.Map<Province, ProvinceResource>(result.Resource);
            return Ok(provinceResource);
        }

    }
}
