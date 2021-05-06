using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IOwnerLocationRepository
    {
        Task<IEnumerable<OwnerLocation>> ListAsync();
        Task<IEnumerable<OwnerLocation>> ListByOwnerProfileIdAsync(int ownerId);
        Task<IEnumerable<OwnerLocation>> ListByProvinceIdAsync(int provinceId);
        Task<IEnumerable<OwnerLocation>> ListByCityIdAsync(int cityId);
        Task<IEnumerable<OwnerLocation>> ListByDateAsync(DateTime date);
        Task<OwnerLocation> FindByOwnerIdAndCityIdAndProvinceIdAndDateAsync(int ownerId, int provinceId, int cityId,DateTime date);
        Task AddAsync(OwnerLocation ownerLocation);
        void Remove(OwnerLocation ownerLocation);
        Task AssignOwnerLocation(int ownerId, int provinceId, int cityId, DateTime date);
        void UnassingOwnerLocation(int ownerId, int provinceId, int cityId, DateTime date);
    }
}
