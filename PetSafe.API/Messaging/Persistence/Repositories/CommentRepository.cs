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
    public class CommentRepository : BaseRepository, ICommentRepository
    {
        public CommentRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
        }

        public async Task<Comment> FindById(int commentId)
        {
            return await _context.Comments.FindAsync(commentId);
        }

        public async Task<IEnumerable<Comment>> ListByVeterinaryProfileId(int veterinaryProfileId)
        {
            return await _context.Comments
                .Where(c => c.VeterinaryProfileId == veterinaryProfileId)
                .Include(c => c.VeterinaryProfile)
                .ToListAsync();
        }

        public void Remove(Comment comment)
        {
            _context.Comments.Remove(comment);
        }

        public void Update(Comment comment)
        {
            _context.Comments.Update(comment);
        }
    }
}
