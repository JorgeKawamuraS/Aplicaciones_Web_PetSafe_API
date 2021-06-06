using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Persistence.Repositories;
using PetSafe.API.Domain.Services;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Services
{
    public class RecordatoryTypeService : IRecordatoryTypeService
    {
        private readonly IRecordatoryTypeRepository _recordatoryTypeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RecordatoryTypeService(IRecordatoryTypeRepository recordatoryTypeRepository, IUnitOfWork unitOfWork)
        {
            _recordatoryTypeRepository = recordatoryTypeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RecordatoryTypeResponse> DeleteAsync(int recordatoryTypeId)
        {
            var existingRecordatoryType = await _recordatoryTypeRepository.FindById(recordatoryTypeId);
            if (existingRecordatoryType==null)
            {
                return new RecordatoryTypeResponse("Recordatory Type not found");
            }
            try
            {
                _recordatoryTypeRepository.Remove(existingRecordatoryType);
                await _unitOfWork.CompleteAsync();

                return new RecordatoryTypeResponse(existingRecordatoryType);
            }
            catch (Exception ex)
            {
                return new RecordatoryTypeResponse($"An error ocurred while deleting recordatory type: {ex.Message}");
            }
        }

        public async Task<RecordatoryTypeResponse> GetByIdAsync(int recordatoryTypeId)
        {
            var existingRecordatoryType = await _recordatoryTypeRepository.FindById(recordatoryTypeId);
            if (existingRecordatoryType == null)
            {
                return new RecordatoryTypeResponse("Recordatory Type not found");
            }
            return new RecordatoryTypeResponse(existingRecordatoryType);
        }

        public async Task<IEnumerable<RecordatoryType>> ListAsync()
        {
            return await _recordatoryTypeRepository.ListAsync();
        }

        public async Task<RecordatoryTypeResponse> SaveAsync(RecordatoryType recordatoryType)
        {
            try
            {
                await _recordatoryTypeRepository.AddAsync(recordatoryType);
                await _unitOfWork.CompleteAsync();

                return new RecordatoryTypeResponse(recordatoryType);
            }
            catch (Exception ex)
            {
                return new RecordatoryTypeResponse($"An error ocurred while saving recordatory type: {ex.Message}");
            }
        }

        public async Task<RecordatoryTypeResponse> UpdateAsync(int recordatoryTypeId, RecordatoryType recordatoryType)
        {
            var existingRecordatoryType = await _recordatoryTypeRepository.FindById(recordatoryTypeId);
            if (existingRecordatoryType == null)
            {
                return new RecordatoryTypeResponse("Recordatory Type not found");
            }
            existingRecordatoryType.Name = recordatoryType.Name;
            try
            {
                _recordatoryTypeRepository.Update(existingRecordatoryType);
                await _unitOfWork.CompleteAsync();

                return new RecordatoryTypeResponse(existingRecordatoryType);
            }
            catch (Exception ex)
            {
                return new RecordatoryTypeResponse($"An error ocurred while deleting recordatory type: {ex.Message}");
            }
        }
    }
}
