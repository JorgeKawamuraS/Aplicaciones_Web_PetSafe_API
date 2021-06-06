using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface ITreatmentRepository
    {
        Task<IEnumerable<Treatment>> ListAsync();
        Task<Treatment> FindById(int id);
        Task AddAsync(Treatment treatment);
        void Update(Treatment treatment);
        void Remove(Treatment treatment);
    }
}
