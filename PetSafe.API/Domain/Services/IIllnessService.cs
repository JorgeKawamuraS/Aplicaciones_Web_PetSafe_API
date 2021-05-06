using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IIllnessService
    {
        Task<IEnumerable<Illness>> ListAsync();
        Task<IEnumerable<Illness>> ListByPetIdAsync(int petId);
        Task<IllnessResponse> GetByIdAsync(int id);
        Task<IllnessResponse> SaveAsync(Illness illness);
        Task<IllnessResponse> UpdateAsync(int id, Illness illness);
        Task<IllnessResponse> DeleteAsync(int id);
    }
}
