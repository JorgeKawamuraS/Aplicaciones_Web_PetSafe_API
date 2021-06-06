using PetSafe.API.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Persistence.Repositories
{
    public interface IRecordatoryRepository
    {
        Task<IEnumerable<Recordatory>> ListByScheduleId(int scheduleId);
        Task<IEnumerable<Recordatory>> ListByRecordatoryTypeId(int recordatoryTypeId);
        Task<Recordatory> FindById(int recordatoryId);
        Task AddAsync(Recordatory recordatory);
        void Update(Recordatory recordatory);
        void Remove(Recordatory recordatory);
    }
}
