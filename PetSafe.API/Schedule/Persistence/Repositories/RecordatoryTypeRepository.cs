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
    public class RecordatoryTypeRepository : BaseRepository, IRecordatoryTypeRepository
    {
        public RecordatoryTypeRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(RecordatoryType recordatoryType)
        {
            await _context.RecordatoryTypes.AddAsync(recordatoryType);
        }

        public async Task<RecordatoryType> FindById(int id)
        {
            return await _context.RecordatoryTypes.FindAsync(id);
        }

        public async Task<IEnumerable<RecordatoryType>> ListAsync()
        {
            return await _context.RecordatoryTypes.ToListAsync();
        }

        public void Remove(RecordatoryType recordatoryType)
        {
            _context.RecordatoryTypes.Remove(recordatoryType);
        }

        public void Update(RecordatoryType recordatoryType)
        {
            _context.RecordatoryTypes.Update(recordatoryType);
        }
    }
}
