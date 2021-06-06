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
    public class ProfileChatsController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IMapper _mapper;

        public ProfileChatsController(IChatService chatService, IMapper mapper)
        {
            _chatService = chatService;
            _mapper = mapper;
        }

        [HttpGet("profiles/{profileid}/chats")]
        [ProducesResponseType(typeof(IEnumerable<ChatResource>), 200)]
        public async Task<IEnumerable<ChatResource>> GetAllBySenderIdAsync(int profileId)
        {
            var chats = await _chatService.ListBySenderId(profileId);
            var resources = _mapper.Map<IEnumerable<Chat>, IEnumerable<ChatResource>>(chats);
            return resources;
        }

        [HttpGet("profiles/{profileid}/profiles/{receiverid}/chats")]
        [ProducesResponseType(typeof(ChatResource), 200)]
        public async Task<IEnumerable<ChatResource>> GetByReceiverIdAsync(int profileId,int receiverId)
        {
            var chats = await _chatService.ListByReceiverId(receiverId);
            var resources = _mapper.Map<IEnumerable<Chat>, IEnumerable<ChatResource>>(chats);
            return resources;
        }

        [HttpPost("profiles/{profileid}/profiles/{receiverid}/pets/{petId}")]
        [ProducesResponseType(typeof(ChatResource), 200)]
        [ProducesResponseType(typeof(BadRequestResult),404)]
        public async Task<IActionResult> PostAsync(int profileId, int receiverId, int petId)
        {
            var result = await _chatService.SaveAsync(profileId, receiverId, petId);

            if (!result.Success)
                return BadRequest(result.Message);

            var chatResource = _mapper.Map<Chat, ChatResource>(result.Resource);
            return Ok(chatResource);
        }

    }
}
