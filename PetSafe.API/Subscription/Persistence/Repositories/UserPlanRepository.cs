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
    public class UserPlanRepository : BaseRepository, IUserPlanRepository
    {
        public UserPlanRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(UserPlan userPlan)
        {
            await _context.UserPlans.AddAsync(userPlan);
        }

        public async Task AssingUserPlan(int userId, int planId, DateTime date)
        {
            UserPlan userPlan = await FindByUserIdDateAndPlanIdAsync(userId,planId,date);
            if (userPlan==null)
            {
                userPlan = new UserPlan { UserId = userId, PlanId = planId, DateOfUpdate = date };
                await AddAsync(userPlan);
            }
        }

        public async Task<UserPlan> FindByUserIdDateAndPlanIdAsync(int userId, int planId, DateTime date)
        {
            return await _context.UserPlans.FindAsync(userId,planId,date);
        }

        public async Task<IEnumerable<UserPlan>> ListAsync()
        {
            return await _context.UserPlans
                .Include(up=>up.User)
                .Include(up => up.Plan)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserPlan>> ListByDateAsync(DateTime date)
        {
            return await _context.UserPlans
                .Where(up=>up.DateOfUpdate==date)
                .Include(up => up.User)
                .Include(up => up.Plan)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserPlan>> ListByPlanIdAsync(int planId)
        {
            return await _context.UserPlans
                .Where(up => up.PlanId == planId)
                .Include(up => up.Plan)
                .ToListAsync();
        }

        public async Task<IEnumerable<UserPlan>> ListByUserIdAsync(int userId)
        {
            return await _context.UserPlans
                .Where(up => up.UserId == userId)
                .Include(up => up.User)
                .ToListAsync();
        }

        public void Remove(UserPlan userPlan)
        {
            _context.UserPlans.Remove(userPlan);
        }

        public async void UnassingUserPlan(int userId, int planId, DateTime date)
        {
            UserPlan userPlan = await FindByUserIdDateAndPlanIdAsync(userId, planId, date);
            if (userPlan != null)
            {
                Remove(userPlan);
            }
        }
    }
}
