using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> ListByChatId(int chatId);
        Task<MessageResponse> GetByIdAsync(int messageId);
        Task<MessageResponse> SaveAsync(int senderId, Message message);
        Task<MessageResponse> DeleteAsync(int messageId);
    }
}