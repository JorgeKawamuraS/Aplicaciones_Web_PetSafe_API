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

namespace PetSafe.API.Domain.Persistence.Repositories
{
    [ApiController]
    [Route("/api/[controller]")]
    [Produces("application/json")]
    public class ChatMessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public ChatMessagesController(IMessageService messageService, IMapper mapper)
        {
            _messageService = messageService;
            _mapper = mapper;
        }

        [HttpGet("/chats/{chatId}/messages")]
        [ProducesResponseType(typeof(IEnumerable<MessageResource>), 200)]
        public async Task<IEnumerable<MessageResource>> GetAllAsync(int chatId)
        {
            var messages = await _messageService.ListByChatId(chatId);
            var resource = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);

            return resource;
        }

        [HttpPost("chats/{senderId}/messages")]
        [ProducesResponseType(typeof(MessageResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult), 404)]
        public async Task<IActionResult> PostAsync(int senderId, [FromBody] SaveMessageResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var message = _mapper.Map<SaveMessageResource, Message>(resource);
            var result = await _messageService.SaveAsync(senderId, message);

            if (!result.Success)
                return BadRequest(result.Message);

            var messageResource = _mapper.Map<Message, MessageResource>(result.Resource);
            return Ok(messageResource);
        }


    }
}
