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
    [Route("/api/cities/{cityId}/veterinary")]
    public class CitiesVeterinaryController : ControllerBase
    {
        private readonly IVeterinaryProfileService _veterinaryProfileService;
        private readonly IMapper _mapper;

        public CitiesVeterinaryController(IVeterinaryProfileService veterinaryProfileService, IMapper mapper)
        {
            _veterinaryProfileService = veterinaryProfileService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<VeterinaryProfileResource>> GetAllByCityIdAsync(int cityId)
        {
            var veterinaryProfiles = await _veterinaryProfileService.ListByCityIdAsync(cityId);
            var resources = _mapper.Map<IEnumerable<VeterinaryProfile>, IEnumerable<VeterinaryProfileResource>>(veterinaryProfiles);
            return resources;
        }
    }
}
