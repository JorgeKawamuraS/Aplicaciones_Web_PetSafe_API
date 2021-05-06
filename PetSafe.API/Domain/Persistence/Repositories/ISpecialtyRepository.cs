using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface ISpecialtyRepository
    {
        Task<IEnumerable<Specialty>> ListAsync();
        Task<Specialty> FindById(int id);
        Task AddAsync(Specialty specialty);
        void Update(Specialty specialty);
        void Remove(Specialty specialty);

    }
}
