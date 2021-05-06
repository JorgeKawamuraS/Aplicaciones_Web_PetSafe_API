using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IVetProfileRepository
    {
        Task<IEnumerable<VetProfile>> ListAsync();
        Task AddAsync(VetProfile vetProfile);
        Task<VetProfile> FindById(int id);
        void Update(VetProfile vetProfile);
        void Remove(VetProfile vetProfile);
    }
}
