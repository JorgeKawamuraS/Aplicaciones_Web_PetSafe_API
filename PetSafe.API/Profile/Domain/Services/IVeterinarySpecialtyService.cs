using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IVeterinarySpecialtyService
    {
        Task<IEnumerable<VeterinarySpecialty>> ListAsync();
        Task<IEnumerable<VeterinarySpecialty>> ListByVeterinaryIdAsync(int veterinaryId);
        Task<IEnumerable<VeterinarySpecialty>> ListBySpecialtyIdAsync(int specialtyId);
        Task<VeterinarySpecialtyResponse> AssignVeterinarySpecialtyAsync(int veterinaryId, int specialtyId);
        Task<VeterinarySpecialtyResponse> UnassignVeterinarySpecialtyAsync(int veterinaryId, int specialtyId);
    }
}
