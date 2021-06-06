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
    public class RecordatoryRepository : BaseRepository, IRecordatoryRepository
    {
        public RecordatoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Recordatory recordatory)
        {
            await _context.Recordatories.AddAsync(recordatory);
        }

        public async Task<Recordatory> FindById(int recordatoryId)
        {
            return await _context.Recordatories.FindAsync(recordatoryId);
        }

        public async Task<IEnumerable<Recordatory>> ListByRecordatoryTypeId(int recordatoryTypeId)
        {
            return await _context.Recordatories
                .Where(r=>r.RecordatoryTypeId==recordatoryTypeId)
                .Include(r=>r.RecordatoryType)
                .ToListAsync();
        }

        public async Task<IEnumerable<Recordatory>> ListByScheduleId(int scheduleId)
        {
            return await _context.Recordatories
                   .Where(r => r.ScheduleId== scheduleId)
                   .Include(r => r.Schedule)
                   .ToListAsync();
        }

        public void Remove(Recordatory recordatory)
        {
            _context.Recordatories.Remove(recordatory);
        }

        public void Update(Recordatory recordatory)
        {
            _context.Recordatories.Update(recordatory);
        }
    }
}
