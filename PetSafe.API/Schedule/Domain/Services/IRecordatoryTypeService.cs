using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Domain.Services
{
    public interface IRecordatoryTypeService
    {
        Task<IEnumerable<RecordatoryType>> ListAsync();
        Task<RecordatoryTypeResponse> GetByIdAsync(int recordatoryTypeId);
        Task<RecordatoryTypeResponse> SaveAsync(RecordatoryType recordatoryType);
        Task<RecordatoryTypeResponse> UpdateAsync(int recordatoryTypeId ,RecordatoryType recordatoryType);
        Task<RecordatoryTypeResponse> DeleteAsync(int recordatoryTypeId);
    }
}
