using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> ListByChatId(int chatSenderId);
        Task<Message> FindById(int messageId);
        Task AddAsync(Message message);
        void Update(Message message);
        void Remove(Message message);
    }
}
