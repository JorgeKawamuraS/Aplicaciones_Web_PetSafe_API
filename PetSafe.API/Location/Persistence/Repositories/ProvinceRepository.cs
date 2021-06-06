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
    public class ProvinceRepository : BaseRepository, IProvinceRepository
    {
        public ProvinceRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Province province)
        {
            await _context.Provinces.AddAsync(province);
        }

        public async Task<Province> FindById(int id)
        {
            return await _context.Provinces.FindAsync(id);
        }

        public async Task<IEnumerable<Province>> ListAsync()
        {
            return await _context.Provinces.ToListAsync();
        }

        public void Remove(Province province)
        {
            _context.Provinces.Remove(province);
        }

        public void Update(Province province)
        {
            _context.Provinces.Update(province);
        }
    }
}
