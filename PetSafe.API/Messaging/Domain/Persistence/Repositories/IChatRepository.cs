using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IChatRepository
    {
        Task<IEnumerable<Chat>> FindByReceiverId(int receiverId);
        Task<IEnumerable<Chat>> ListBySenderId(int senderId);
        Task<Chat> FindById(int chatId);
        Task AddAsync(Chat chat);
        void Update(Chat chat);
        void Remove(Chat chat);
    }
}
