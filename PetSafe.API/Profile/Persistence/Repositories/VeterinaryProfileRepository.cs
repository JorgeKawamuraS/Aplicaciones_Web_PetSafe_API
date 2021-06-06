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
    public class VeterinaryProfileRepository : BaseRepository, IVeterinaryProfileRepository
    {
        public VeterinaryProfileRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(VeterinaryProfile veterinaryProfile)
        {
            await _context.VeterinaryProfiles.AddAsync(veterinaryProfile);
        }

        public async Task<VeterinaryProfile> FindById(int id)
        {
            return await _context.VeterinaryProfiles.FindAsync(id);
        }

        public async Task<IEnumerable<VeterinaryProfile>> ListAsync()
        {
            return await _context.VeterinaryProfiles.ToListAsync();
        }

        public async Task<IEnumerable<VeterinaryProfile>> ListByCityIdAsync(int cityId)
        {
            return await _context.VeterinaryProfiles
                .Where(vp=> vp.CityId==cityId)
                .Include(vp => vp.City)
                .ToListAsync();
        }

        public async Task<IEnumerable<VeterinaryProfile>> ListByProvinceIdAsync(int provinceId)
        {
            return await _context.VeterinaryProfiles
                .Where(vp => vp.ProvinceId == provinceId)
                .Include(vp => vp.Province)
                .ToListAsync();
        }

        public void Remove(VeterinaryProfile veterinaryProfile)
        {
            _context.VeterinaryProfiles.Remove(veterinaryProfile);
        }

        public void Update(VeterinaryProfile veterinaryProfile)
        {
            _context.VeterinaryProfiles.Update(veterinaryProfile);
        }
    }
}
