using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IOwnerLocationService
    {
        Task<IEnumerable<OwnerLocation>> ListAsync();
        Task<IEnumerable<OwnerLocation>> ListByOwnerProfileIdAsync(int ownerId);
        Task<IEnumerable<OwnerLocation>> ListByProvinceIdAsync(int provinceId);
        Task<IEnumerable<OwnerLocation>> ListByCityIdAsync(int cityId);
        Task<IEnumerable<OwnerLocation>> ListByDateAsync(DateTime date);
        Task<OwnerLocationResponse> AssingOwnerLocation(int ownerId, int provinceId, int cityId, DateTime date);
        Task<OwnerLocationResponse> UnassingOwnerLocation(int ownerId, int provinceId, int cityId, DateTime date);
    }
}
