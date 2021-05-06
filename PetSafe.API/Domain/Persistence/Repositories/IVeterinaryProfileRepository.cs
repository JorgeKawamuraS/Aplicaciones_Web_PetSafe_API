using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IVeterinaryProfileRepository
    {
        Task<IEnumerable<VeterinaryProfile>> ListAsync();
        Task<IEnumerable<VeterinaryProfile>> ListByProvinceIdAsync(int provinceId);
        Task<IEnumerable<VeterinaryProfile>> ListByCityIdAsync(int cityId);
        Task AddAsync(VeterinaryProfile veterinaryProfile);
        Task<VeterinaryProfile> FindById(int id);
        void Update(VeterinaryProfile veterinaryProfile);
        void Remove(VeterinaryProfile veterinaryProfile);

    }
}
