using Microsoft.EntityFrameworkCore;
using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Persistence.Context;
using PetSafe.API.Domain.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Persistence.Repositories
{
    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
        }

        public async Task<Message> FindById(int messageId)
        {
            return await _context.Messages.FindAsync(messageId);
        }

        public async Task<IEnumerable<Message>> ListByChatId(int chatReceiverId)
        {
            return await _context.Messages
                .Where(m=>m.ChatId==chatReceiverId)
                .Include(m=>m.Chat)
                .ToListAsync();
        }

        public void Remove(Message message)
        {
            _context.Messages.Remove(message);
        }

        public void Update(Message message)
        {
            _context.Messages.Update(message);
        }
    }
}
