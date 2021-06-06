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
    public class ChatRepository : BaseRepository, IChatRepository
    {
        public ChatRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Chat chat)
        {
            await _context.Chats.AddAsync(chat);
        }

        public async Task<Chat> FindById(int chatId)
        {
            return await _context.Chats.FindAsync(chatId);
        }

        public async Task<IEnumerable<Chat>> ListBySenderId(int senderId)
        {
            return await _context.Chats
                .Where(c=>c.SenderProfileId==senderId)
                .Include(c=>c.SenderProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<Chat>> FindByReceiverId(int receiverId)
        {
            return await _context.Chats
                .Where(c => c.ReceiverProfileId == receiverId)
                .ToListAsync();
        }

        public void Remove(Chat chat)
        {
            _context.Chats.Remove(chat);
        }

        public void Update(Chat chat)
        {
            _context.Chats.Update(chat);
        }
    }
}
