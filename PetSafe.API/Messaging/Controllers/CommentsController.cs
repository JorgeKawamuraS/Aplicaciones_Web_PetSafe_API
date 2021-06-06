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
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CommentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id) {
            var result = await _commentService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);
            return Ok(commentResource);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CommentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveCommentResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var comment = _mapper.Map<SaveCommentResource, Comment>(resource);
            var result = await _commentService.UpdateAsync(id,comment);

            if (!result.Success)
                return BadRequest(result.Message);

            var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);
            return Ok(commentResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(CommentResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _commentService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var commentResource = _mapper.Map<Comment, CommentResource>(result.Resource);
            return Ok(commentResource);

        }
    }
}
