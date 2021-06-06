using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IOwnerProfileRepository
    {
        Task<IEnumerable<OwnerProfile>> ListAsync();
        Task AddAsync(OwnerProfile ownerProfile);
        Task<OwnerProfile> FindById(int id);
        void Update(OwnerProfile ownerProfile);
        void Remove(OwnerProfile ownerProfile);
    }
}
