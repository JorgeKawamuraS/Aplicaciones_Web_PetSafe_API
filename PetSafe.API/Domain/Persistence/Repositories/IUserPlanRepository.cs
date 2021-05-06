using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IUserPlanRepository
    {
        Task<IEnumerable<UserPlan>> ListAsync();
        Task<IEnumerable<UserPlan>> ListByUserIdAsync(int userId);
        Task<IEnumerable<UserPlan>> ListByPlanIdAsync(int planId);
        Task<IEnumerable<UserPlan>> ListByDateAsync(DateTime date);
        Task<UserPlan> FindByUserIdDateAndPlanIdAsync(int userId, int planId, DateTime date);
        Task AddAsync(UserPlan userPlan);
        void Remove(UserPlan userPlan);
        Task AssingUserPlan(int userId, int planId, DateTime date);
        void UnassingUserPlan(int userId, int planId, DateTime date);
    }
}
