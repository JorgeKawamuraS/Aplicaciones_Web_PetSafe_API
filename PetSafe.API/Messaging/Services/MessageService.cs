using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Persistence.Repositories;
using PetSafe.API.Domain.Services;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Services
{
    public class MessageService : IMessageService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IChatRepository chatRepository, 
            IMessageRepository messageRepository, IUnitOfWork unitOfWork)
        {
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResponse> DeleteAsync(int messageId)
        {
            var existingMessage = await _messageRepository.FindById(messageId);
            if (existingMessage==null)
            {
                return new MessageResponse("Message not found");
            }
            try
            {
                _messageRepository.Remove(existingMessage);
                await _unitOfWork.CompleteAsync();

                return new MessageResponse(existingMessage);
            }
            catch (Exception ex)
            {
                return new MessageResponse($"An error ocurred while deleting message: {ex.Message}");
            }
        }

        public async Task<MessageResponse> GetByIdAsync(int messageId)
        {
            var existingMessage = await _messageRepository.FindById(messageId);
            if (existingMessage == null)
            {
                return new MessageResponse("Message not found");
            }
            return new MessageResponse(existingMessage);
        }

        public async Task<IEnumerable<Message>> ListByChatId(int chatId)
        {
            return await _messageRepository.ListByChatId(chatId);
        }

        public async Task<MessageResponse> SaveAsync(int senderId,Message message)
        {
            var existingSenderChat = await _chatRepository.FindById(senderId);
            if (existingSenderChat==null)
                return new MessageResponse("Chat ot found");
            try
            {
                IEnumerable<Chat> chats = await _chatRepository.ListBySenderId(existingSenderChat.ReceiverProfileId);
                int receiverId=0;
                chats.ToList().ForEach(chat => {
                    if (chat.ReceiverProfileId == senderId && existingSenderChat.PetId==chat.PetId)
                        receiverId = chat.Id;
                });

                if (receiverId == 0)
                {
                    Chat chat = new Chat
                    {
                        SenderProfileId = existingSenderChat.ReceiverProfileId,
                        ReceiverProfileId = existingSenderChat.SenderProfileId,
                        PetId = existingSenderChat.PetId
                    };
                    await _chatRepository.AddAsync(chat);
                    await _unitOfWork.CompleteAsync();
                    receiverId = chat.Id;
                }

                message.ChatId = receiverId;
                await _messageRepository.AddAsync(message);
                await _unitOfWork.CompleteAsync();
                message.ChatId = existingSenderChat.ReceiverProfileId;
                await _messageRepository.AddAsync(message);
                await _unitOfWork.CompleteAsync();

                return new MessageResponse(message);
            }
            catch (Exception ex)
            {
                return new MessageResponse($"An error ocurred while saving message: {ex.Message}");
            }
        }
    }
}
