using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IPlanRepository
    {
        Task<IEnumerable<Plan>> ListAsync();
        Task<Plan> FindById(int planId);
        Task AddAsync(Plan plan);
        void Update(Plan plan);
        void Remove(Plan plan);
    }
}
