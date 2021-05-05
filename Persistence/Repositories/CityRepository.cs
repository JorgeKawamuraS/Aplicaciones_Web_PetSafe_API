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
    public class CityRepository : BaseRepository, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(City city)
        {
            await _context.Cities.AddAsync(city);
        }

        public async Task<City> FindById(int id)
        {
            return await _context.Cities.FindAsync(id);
        }

        public async Task<IEnumerable<City>> ListAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<IEnumerable<City>> ListByProvinceIdAsync(int provinceId)
        {
            return await _context.Cities
                .Where(c=> c.ProvinceId==provinceId)
                .Include(c=> c.Province)
                .ToListAsync();
        }

        public void Remove(City city)
        {
            _context.Cities.Remove(city);
        }

        public void Update(City city)
        {
            _context.Cities.Update(city);
        }
    }
}
