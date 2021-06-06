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
    [Route("/api/")]
    [Produces("application/json")]
    public class ProfileVeterinaryComments : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public ProfileVeterinaryComments(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpPost("owners/{ownerId}/veterinaries/{veterinaryId}/comments")]
        [ProducesResponseType(typeof(CommentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int ownerId, int veterinaryId, [FromBody] SaveCommentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var comment = _mapper.Map<SaveCommentResource, Comment>(resource);
            var result = await _commentService.SaveAsync(ownerId, veterinaryId, comment);

            if (!result.Success)
                return BadRequest(result.Message);

            var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);
            return Ok(commentResource);
        }

    }
}
