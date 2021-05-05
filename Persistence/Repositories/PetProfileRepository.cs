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
    public class PetProfileRepository : BaseRepository, IPetProfileRepository
    {
        public PetProfileRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(PetProfile petProfile)
        {
            await _context.PetProfiles.AddAsync(petProfile);
        }

        public async Task<PetProfile> FindById(int id)
        {
            return await _context.PetProfiles.FindAsync(id);
        }

        public async Task<IEnumerable<PetProfile>> ListAsync()
        {
            return await _context.PetProfiles.ToListAsync();
        }

        public void Remove(PetProfile petProfile)
        {
            _context.PetProfiles.Remove(petProfile);
        }

        public void Update(PetProfile petProfile)
        {
            _context.PetProfiles.Update(petProfile);
        }
    }
}
