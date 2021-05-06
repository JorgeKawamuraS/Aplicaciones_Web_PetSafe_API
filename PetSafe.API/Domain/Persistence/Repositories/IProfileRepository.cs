using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> ListAsync();
        Task<IEnumerable<Profile>> ListByCityIdAsync(int cityId);
        Task<IEnumerable<Profile>> ListByProvinceIdAsync(int provinceId);
        Task AddAsync(Profile profile);
        Task<Profile> FindByIdAsync(int id);
        void Update(Profile profile);
        void Remove(Profile profile);
    }
}
