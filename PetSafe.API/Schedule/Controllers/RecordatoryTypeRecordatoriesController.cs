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
    public class RecordatoryTypeRecordatoriesController : ControllerBase
    {
        private readonly IRecordatoryService _recordatoryService;
        private readonly IMapper _mapper;

        public RecordatoryTypeRecordatoriesController(IRecordatoryService recordatoryService, IMapper mapper)
        {
            _recordatoryService = recordatoryService;
            _mapper = mapper;
        }

        [HttpGet("recordatoryTypes/{recordatoryTypeId}/recordatories")]
        [ProducesResponseType(typeof(IEnumerable<RecordatoryResource>), 200)]
        public async Task<IEnumerable<RecordatoryResource>> GetAllByReocrdatoryTypeId(int recordatoryTypeId)
        {
            var recordatories = await _recordatoryService.ListByScheduleId(recordatoryTypeId);
            var resources = _mapper.Map<IEnumerable<Recordatory>, IEnumerable<RecordatoryResource>>(recordatories);
            return resources;
        }


    }
}
