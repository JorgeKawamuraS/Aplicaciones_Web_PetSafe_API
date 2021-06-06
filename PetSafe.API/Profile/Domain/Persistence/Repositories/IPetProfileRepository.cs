using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IPetProfileRepository
    {
        Task<IEnumerable<PetProfile>> ListAsync();
        Task<PetProfile> FindById(int id);
        Task AddAsync(PetProfile petProfile);
        void Update(PetProfile petProfile);
        void Remove(PetProfile petProfile);
    }
}
