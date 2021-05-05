using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IOwnerProfileService
    {
        Task<IEnumerable<OwnerProfile>> ListAsync();
        Task<IEnumerable<OwnerProfile>> ListByPetIdAsync(int petId);
        Task<IEnumerable<OwnerProfile>> ListByCityAsync(int cityId);
        Task<IEnumerable<OwnerProfile>> ListByProvinceAsync(int provinceId);
        Task<OwnerProfileResponse> GetByIdAsync(int id);
        Task<OwnerProfileResponse> SaveAsync(OwnerProfile ownerProfile);
        Task<OwnerProfileResponse> UpdateAsync(int id, OwnerProfile ownerProfile);
        Task<OwnerProfileResponse> DeleteAsync(int id);
    }
}
