using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IVeterinarySpecialtyRepository
    {
        Task<IEnumerable<VeterinarySpecialty>> ListAsync();
        Task<IEnumerable<VeterinarySpecialty>> ListByVeterinaryIdAsync(int veterinaryId);
        Task<IEnumerable<VeterinarySpecialty>> ListBySpecialtyIdAsync(int specialtyId);
        Task<VeterinarySpecialty> FindByVeterinaryIdAndSpecialtyId(int veterinaryId, int specialtyId);
        Task AddAsync(VeterinarySpecialty veterinarySpecialty);
        void Remove(VeterinarySpecialty veterinarySpecialty);
        Task AssignVeterinarySpecialty(int veterinaryId, int specialtyId);
        void UnassignVeterinarySpecialty(int veterinaryId, int specialtyId);

    }
}
