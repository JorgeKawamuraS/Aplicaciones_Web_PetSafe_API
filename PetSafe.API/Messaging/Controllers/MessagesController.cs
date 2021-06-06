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
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessagesController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MessageResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var result = await _messageService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var messageResource = _mapper.Map<Message, MessageResource>(result.Resource);
            return Ok(messageResource);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(MessageResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _messageService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);

            var messageResource = _mapper.Map<Message, MessageResource>(result.Resource);
            return Ok(messageResource);
        }

    }
}