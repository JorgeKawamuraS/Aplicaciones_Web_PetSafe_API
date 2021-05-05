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
    public class OwnerProfileRepository : BaseRepository, IOwnerProfileRepository
    {
        public OwnerProfileRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(OwnerProfile ownerProfile)
        {
            await _context.OwnerProfiles.AddAsync(ownerProfile);
        }

        public async Task<OwnerProfile> FindById(int id)
        {
            return await _context.OwnerProfiles.FindAsync(id);
        }

        public async Task<IEnumerable<OwnerProfile>> ListAsync()
        {
            return await _context.OwnerProfiles.ToListAsync();
        }

        public void Remove(OwnerProfile ownerProfile)
        {
            _context.OwnerProfiles.Remove(ownerProfile);
        }

        public void Update(OwnerProfile ownerProfile)
        {
            _context.OwnerProfiles.Update(ownerProfile);
        }
    }
}
