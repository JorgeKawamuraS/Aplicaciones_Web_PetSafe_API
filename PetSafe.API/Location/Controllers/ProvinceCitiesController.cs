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
    [Route("/api/provinces/{provinceId}/cities")]
    [Produces("application/json")]
    public class ProvinceCitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public ProvinceCitiesController(ICityService cityService, IMapper mapper)
        {
            _cityService = cityService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(CityResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IEnumerable<CityResource>> GetAllByProvinceIdAsync(int provinceId)
        {
            var cities = await _cityService.ListByProvinceIdAsync(provinceId);
            var resources = _mapper
                .Map<IEnumerable<City>, IEnumerable<CityResource>>(cities);
            return resources;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CityResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> Postasync(int provinceId,[FromBody] SaveCityResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var city = _mapper.Map<SaveCityResource, City>(resource);
            var result = await _cityService.SaveAsync(provinceId,city);

            if (!result.Success)
                return BadRequest(result.Message);

            var cityResource = _mapper.Map<City, CityResource>(result.Resource);
            return Ok(cityResource);
        }


    }
}
