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
    [Route("/api/")]
    [Produces("application/json")]
    public class VeterinaryCommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public VeterinaryCommentsController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet("veterinary/{veterinaryId}/comments")]
        [ProducesResponseType(typeof(IEnumerable<CommentResource>), 200)]
        public async Task<IEnumerable<CommentResource>> GetAllAsync(int veterinaryId)
        {
            var comments = await _commentService.ListByVeterinaryProfileId(veterinaryId);
            var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
            return resources;
        }
    }
}
