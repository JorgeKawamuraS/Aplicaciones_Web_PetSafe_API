using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IRecordatoryTypeRepository
    {
        Task<IEnumerable<RecordatoryType>> ListAsync();
        Task AddAsync(RecordatoryType recordatoryType);
        Task<RecordatoryType> FindById(int id);
        void Update(RecordatoryType recordatoryType);
        void Remove(RecordatoryType recordatoryType);
    }
}
