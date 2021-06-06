using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IVetVeterinaryService
    {
        Task<IEnumerable<VetVeterinary>> ListAsync();
        Task<IEnumerable<VetVeterinary>> ListByVetIdAsync(int vetId);
        Task<IEnumerable<VetVeterinary>> ListByVeterinaryIdAsync(int veterinaryId);
        Task<VetVeterinaryResponse> AssignVetVeterinaryAsync(int vetId, int veterinaryId);
        Task<VetVeterinaryResponse> UnassignVetVeterinaryAsync(int vetId, int veterinaryId);
    }
}
