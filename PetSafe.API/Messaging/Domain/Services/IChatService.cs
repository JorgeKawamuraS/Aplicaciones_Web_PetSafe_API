using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IChatService
    {
        Task<IEnumerable<Chat>> ListBySenderId(int senderId);
        Task<IEnumerable<Chat>> ListByReceiverId(int receiverId);
        Task<ChatResponse> GetByIdAsync(int chatId);
        Task<ChatResponse> SaveAsync(int chatId, int receiverId, int petId);
        Task<ChatResponse> DeleteAsync(int chatId);
    }
}
