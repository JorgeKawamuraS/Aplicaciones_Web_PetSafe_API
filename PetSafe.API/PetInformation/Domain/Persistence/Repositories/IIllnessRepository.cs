using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IIllnessRepository
    {
        Task<IEnumerable<Illness>> ListAsync();
        Task AddAsync(Illness illness);
        Task<Illness> FindById(int id);
        void Update(Illness illness);
        void Remove(Illness illness);

    }
}
