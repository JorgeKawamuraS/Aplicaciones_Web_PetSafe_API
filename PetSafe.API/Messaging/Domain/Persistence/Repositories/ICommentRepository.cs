using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> ListByVeterinaryProfileId(int veterinaryProfileId);
        Task<Comment> FindById(int commentId);
        Task AddAsync(Comment comment);
        void Update(Comment comment);
        void Remove(Comment comment);
    }
}
