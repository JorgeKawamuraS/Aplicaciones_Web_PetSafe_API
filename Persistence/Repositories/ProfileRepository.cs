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
    public class ProfileRepository : BaseRepository, IProfileRepository
    {
        public ProfileRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Profile profile)
        {
            await _context.Profiles.AddAsync(profile);
        }

        public async Task<Profile> FindByIdAsync(int id)
        {
            return await _context.Profiles.FindAsync(id);
        }

        public async Task<IEnumerable<Profile>> ListAsync()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<IEnumerable<Profile>> ListByCityIdAsync(int cityId)
        {
            return await _context.Profiles.
                Where(p=>p.CityId==cityId)
                .Include(p => p.City)
                .ToListAsync();
        }

        public async Task<IEnumerable<Profile>> ListByProvinceIdAsync(int provinceId)
        {
            return await _context.Profiles.
               Where(p => p.ProvinceId == provinceId)
               .Include(p => p.Province)
               .ToListAsync();
        }

        public void Remove(Profile profile)
        {
            _context.Profiles.Remove(profile);
        }

        public void Update(Profile profile)
        {
            _context.Profiles.Update(profile);
        }
    }
}
