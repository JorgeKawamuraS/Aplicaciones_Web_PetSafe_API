using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IVetVeterinaryRepository
    {
        Task<IEnumerable<VetVeterinary>> ListAsync();
        Task<IEnumerable<VetVeterinary>> ListByVetIdAsync(int vetId);
        Task<IEnumerable<VetVeterinary>> ListByVeterinaryIdAsync(int veterinaryId);
        Task<VetVeterinary> FindByVetIdAndVeterinaryIdAsync(int vetId, int veterinaryId);
        Task AddAsync(VetVeterinary vetVeterinary);
        void Remove(VetVeterinary vetVeterinary);
        Task AssignVetVeterinary(int vetId, int veterinaryId);
        void UnassignVetVeterinary(int vetId, int veterinaryId);
    }
}
