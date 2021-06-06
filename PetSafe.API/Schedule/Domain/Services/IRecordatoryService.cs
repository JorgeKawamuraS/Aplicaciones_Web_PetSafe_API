using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IRecordatoryService
    {
        Task<IEnumerable<Recordatory>> ListByScheduleId(int scheduleId);
        Task<IEnumerable<Recordatory>> ListByRecordatoryTypeId(int recordatoryTypeId);
        Task<RecordatoryResponse> GetByIdAsync(int recordatoryId);
        Task<RecordatoryResponse> SaveOwnerAsync(int ownerId, int scheduleId, int recordyTypeId, int petId, Recordatory recordatory);
        Task<RecordatoryResponse> SaveVetAsync(int vetId,int ownerId, int scheduleId, int recordyTypeId, int petId, Recordatory recordatory);
        Task<RecordatoryResponse> UpdateAsync(int recordatoryId, Recordatory recordatory);
        Task<RecordatoryResponse> DeleteAsync(int recordatoryId);

    }
}
