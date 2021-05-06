using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IPetProfileService
    {
        Task<IEnumerable<PetProfile>> ListAsync();
        Task<IEnumerable<PetProfile>> ListByTreatmentIdAsync(int treatmentId);
        Task<IEnumerable<PetProfile>> ListByOwnerProfileIdAsync(int ownerProfileId);
        Task<IEnumerable<PetProfile>> ListByIllnessIdAsync(int illnessId);
        Task<PetProfileResponse> GetByIdAsync(int id);
        Task<PetProfileResponse> SaveAsync(PetProfile petProfile);
        Task<PetProfileResponse> UpdateAsync(int id, PetProfile petProfile);
        Task<PetProfileResponse> DeleteAsync(int id);
    }
}
