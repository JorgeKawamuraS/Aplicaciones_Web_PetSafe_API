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
    public class ChatsController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IMapper _mapper;

        public ChatsController(IChatService chatService, IMapper mapper)
        {
            _chatService = chatService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ChatResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _chatService.GetByIdAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var chatResource = _mapper.Map<Chat, ChatResource>(result.Resource);
            return Ok(chatResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ChatResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _chatService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var chatResource = _mapper.Map<Chat, ChatResource>(result.Resource);
            return Ok(chatResource);
        }
    }
}