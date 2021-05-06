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
    public class OwnerLocationRepository : BaseRepository, IOwnerLocationRepository
    {
        public OwnerLocationRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(OwnerLocation ownerLocation)
        {
            await _context.OwnerLocations.AddAsync(ownerLocation);
        }

        public async Task AssignOwnerLocation(int ownerId, int provinceId, int cityId, DateTime date)
        {
            OwnerLocation ownerLocation = await FindByOwnerIdAndCityIdAndProvinceIdAndDateAsync(ownerId, provinceId, cityId, date);
            if (ownerLocation==null)
            {
                ownerLocation = new OwnerLocation { CityId=cityId, OwnerId=ownerId, ProvinceId=provinceId,Date=date };
                await AddAsync(ownerLocation);
            }
        }

        public async Task<OwnerLocation> FindByOwnerIdAndCityIdAndProvinceIdAndDateAsync(int ownerId, int provinceId, int cityId, DateTime date)
        {
            return await _context.OwnerLocations.FindAsync(cityId,date,ownerId,provinceId);
        }

        public async Task<IEnumerable<OwnerLocation>> ListAsync()
        {
            return await _context.OwnerLocations
                .Include(ol=>ol.City)
                .Include(ol => ol.Province)
                .Include(ol => ol.OwnerProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<OwnerLocation>> ListByCityIdAsync(int cityId)
        {
            return await _context.OwnerLocations
                .Where(ol => ol.CityId == cityId)
                .Include(ol => ol.City)
                .ToListAsync();
        }

        public async Task<IEnumerable<OwnerLocation>> ListByDateAsync(DateTime date)
        {
            return await _context.OwnerLocations
                .Where(ol => ol.Date == date)
                .Include(ol => ol.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<OwnerLocation>> ListByOwnerProfileIdAsync(int ownerId)
        {
            return await _context.OwnerLocations
                .Where(ol => ol.OwnerId == ownerId)
                .Include(ol => ol.OwnerProfile)
                .ToListAsync();
        }

        public async Task<IEnumerable<OwnerLocation>> ListByProvinceIdAsync(int provinceId)
        {
            return await _context.OwnerLocations
                .Where(ol => ol.ProvinceId == provinceId)
                .Include(ol => ol.Province)
                .ToListAsync();
        }

        public void Remove(OwnerLocation ownerLocation)
        {
            _context.OwnerLocations.Remove(ownerLocation);
        }

        public async void UnassingOwnerLocation(int ownerId, int provinceId, int cityId, DateTime date)
        {
            OwnerLocation ownerLocation = await FindByOwnerIdAndCityIdAndProvinceIdAndDateAsync(ownerId, provinceId, cityId, date);
            if (ownerLocation != null)
            {
                Remove(ownerLocation);
            }
        }
    }
}