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
    public class VetProfileRepository : BaseRepository, IVetProfileRepository
    {
        public VetProfileRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(VetProfile vetProfile)
        {
            await _context.VetProfiles.AddAsync(vetProfile);
        }

        public async Task<VetProfile> FindById(int id)
        {
            return await _context.VetProfiles.FindAsync(id);
        }

        public async Task<IEnumerable<VetProfile>> ListAsync()
        {
            return await _context.VetProfiles.ToListAsync();
        }

        public void Remove(VetProfile vetProfile)
        {
            _context.VetProfiles.Remove(vetProfile);
        }

        public void Update(VetProfile vetProfile)
        {
            _context.VetProfiles.Update(vetProfile);
        }
    }
}
