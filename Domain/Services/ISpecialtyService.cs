using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface ISpecialtyService
    {
        Task<IEnumerable<Specialty>> ListAsync();
        Task<IEnumerable<Specialty>> ListByVeterinaryidAsync(int veterinaryId);
        Task<SpecialtyResponse> GetByIdAsync(int id);
        Task<SpecialtyResponse> SaveAsync(Specialty specialty);
        Task<SpecialtyResponse> UpdateAsync(int id, Specialty specialty);
        Task<SpecialtyResponse> DeleteAsync(int id);
    }
}
