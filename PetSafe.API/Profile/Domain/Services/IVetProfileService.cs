using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IVetProfileService
    {
        Task<IEnumerable<VetProfile>> ListAsync();
        Task<IEnumerable<VetProfile>> ListByVeterinaryIdAsync(int veterinaryId);
        Task<VetProfileResponse> GetByIdAsync(int id);
        Task<VetProfileResponse> SaveAsync(int provinceId, int cityId, int userId, VetProfile vetProfile);
        Task<VetProfileResponse> UpdateAsync(int id, VetProfile vetProfile);
        Task<VetProfileResponse> DeleteAsync(int id);
    }
}
