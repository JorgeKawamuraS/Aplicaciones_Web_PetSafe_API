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
    public class ScheduleRepository : BaseRepository, IScheduleRepository
    {
        public ScheduleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Schedule schedule)
        {
            await _context.Schedules.AddAsync(schedule);
        }

        public async Task<Schedule> FindById(int id)
        {
            return await _context.Schedules.FindAsync(id);
        }

        public async Task<IEnumerable<Schedule>> FindByProfileId(int profileId)
        {
            return await _context.Schedules
                .Where(s=>s.ProfileId==profileId)
                .Include(s=>s.Profile)
                .ToListAsync();
        }

        public void Remove(Schedule schedule)
        {
            _context.Schedules.Remove(schedule);
        }

        public void Update(Schedule schedule)
        {
            _context.Schedules.Update(schedule);
        }

    }
}
