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
    public class UserPlansController : ControllerBase
    {
        private readonly IUserPlanService _userPlanService;
        private readonly IMapper _mapper;

        public UserPlansController(IUserPlanService userPlanService, IMapper mapper)
        {
            _userPlanService = userPlanService;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserPlanResource>), 200)]
        public async Task<IEnumerable<UserPlanResource>> GetAllAsync()
        {
            var userPlans = await _userPlanService.ListAsync();
            var resources = _mapper
                .Map<IEnumerable<UserPlan>, IEnumerable<UserPlanResource>>(userPlans);
            return resources;
        }

        [HttpPost("users/{userId}/plans/{planId}")]
        public async Task<IActionResult> AssignUserPlan(int userId, int planId, [FromBody] SaveUserPlanResource resource)
        {
            var result = await _userPlanService.AssignUserPlanAsync(userId,planId,resource.DateOfUpdate);
            if (!result.Success)
                return BadRequest(result.Message);

            var petIllnesResource = _mapper.Map<UserPlan, UserPlanResource>(result.Resource);
            return Ok(petIllnesResource);
        }

        [HttpDelete("users/{userId}/plans/{planId}")]
        public async Task<IActionResult> UnassignUserPlan(int userId, int planId, [FromBody] SaveUserPlanResource resource)
        {
            var result = await _userPlanService.UnassignUserPlanAsync(userId, planId, resource.DateOfUpdate);
            if (!result.Success)
                return BadRequest(result.Message);

            var petIllnesResource = _mapper.Map<UserPlan, UserPlanResource>(result.Resource);
            return Ok(petIllnesResource);
        }




    }
}
