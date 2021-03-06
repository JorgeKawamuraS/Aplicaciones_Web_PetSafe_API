using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface ICityService
    {
        Task<IEnumerable<City>> ListAsync();
        Task<IEnumerable<City>> ListByProvinceIdAsync(int provinceId);
        Task<IEnumerable<City>> ListByOwnerProfileIdAsync(int ownerProfileId);
        Task<CityResponse> GetByIdAsync(int cityId);
        Task<CityResponse> SaveAsync(int provinceId, City city);
        Task<CityResponse> UpdateAsync(int cityId, City city);
        Task<CityResponse> DeleteAsync(int cityId);
    }
}
