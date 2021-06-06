using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> ListAsync();
        Task<IEnumerable<City>> ListByProvinceIdAsync(int provinceId);
        Task AddAsync(City city);
        Task<City> FindById(int id);
        void Update(City city);
        void Remove(City city);
    }
}
