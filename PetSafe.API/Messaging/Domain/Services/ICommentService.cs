using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> ListByVeterinaryProfileId(int veterinaryProfileId);
        Task<CommentResponse> GetByIdAsync(int commentId);
        Task<CommentResponse> SaveAsync(int ownerId, int veterinaryId, Comment comment);
        Task<CommentResponse> UpdateAsync(int commentId, Comment comment);
        Task<CommentResponse> DeleteAsync(int commentId);

    }
}
