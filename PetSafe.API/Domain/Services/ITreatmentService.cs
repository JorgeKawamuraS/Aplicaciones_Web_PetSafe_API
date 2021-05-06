using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface ITreatmentService
    {
        Task<IEnumerable<Treatment>> ListAsync();
        Task<IEnumerable<Treatment>> ListByPetIdAsync(int petId);
        Task<TreatmentResponse> GetByIdAsync(int id);
        Task<TreatmentResponse> SaveAsync(Treatment treatment);
        Task<TreatmentResponse> UpdateAsync(int id, Treatment treatment);
        Task<TreatmentResponse> DeleteAsync(int id);
    }
}
