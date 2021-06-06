using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IProfileService
    {
        Task<IEnumerable<Profile>> ListAsync();
        Task<IEnumerable<Profile>> ListByCityIdAsync(int cityId);
        Task<IEnumerable<Profile>> ListByProvinceIdAsync(int provinceId);
        Task<ProfileResponse> GetByIdAsync(int id);
        Task<ProfileResponse> SaveAsync(int cityId, int provinceId, Profile profile);
        Task<ProfileResponse> UpdateAsync(int id,Profile profile);
        Task<ProfileResponse> DeleteAsync(int id);
    }
}
