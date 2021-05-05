using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IVeterinaryProfileService
    {
        Task<IEnumerable<VeterinaryProfile>> ListAsync();
        Task<IEnumerable<VeterinaryProfile>> ListByVetIdAsync(int vetId);
        Task<IEnumerable<VeterinaryProfile>> ListBySpecialtyIdAsync(int specialtyId);
        Task<VeterinaryProfileResponse> GetByIdAsync(int id);
        Task<VeterinaryProfileResponse> SaveAsync(VeterinaryProfile veterinaryProfile);
        Task<VeterinaryProfileResponse> UpdateAsync(int id, VeterinaryProfile veterinaryProfile);
        Task<VeterinaryProfileResponse> DeleteAsync(int id);
    }
}
