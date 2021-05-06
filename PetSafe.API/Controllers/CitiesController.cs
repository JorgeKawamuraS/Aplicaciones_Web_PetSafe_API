using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services;
using PetSafe.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swashbuckle.AspNetCore.Annotations;
using Supermarket.API.Extensions;

namespace PetSafe.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CitiesController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CityResource>), 200)]
        public async Task<IEnumerable<CityResource>> GetAllAsync()
        {
            var cities = await _cityService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<City>, IEnumerable<CityResource>>(cities);
            return resources;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CityResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _cityService.GetByIdAsync(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            var cityResource = _mapper.Map<City, CityResource>(result.Resource);
            return Ok(cityResource);
        }

        [HttpPost]
        [ProducesResponseType(typeof(CityResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> Postasync([FromBody] SaveCityResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var city = _mapper.Map<SaveCityResource, City>(resource);
            var result = await _cityService.SaveAsync(city);

            if (!result.Success)
                return BadRequest(result.Message);

            var cityResource = _mapper.Map<City, CityResource>(result.Resource);
            return Ok(cityResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CityResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCityResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var city = _mapper.Map<SaveCityResource, City>(resource);
            var result = await _cityService.UpdateAsync(id, city);

            if (!result.Success)
                return BadRequest(result.Message);

            var cityResource = _mapper.Map<City, CityResource>(result.Resource);
            return Ok(cityResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CityResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _cityService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var cityResource = _mapper.Map<City, CityResource>(result.Resource);
            return Ok(cityResource);

        }
    }
}
