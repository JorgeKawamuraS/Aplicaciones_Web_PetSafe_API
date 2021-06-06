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
    public class OwnerLocationService : IOwnerLocationService
    {
        private readonly IOwnerLocationRepository _ownerLocationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OwnerLocationService(IUnitOfWork unitOfWork, IOwnerLocationRepository ownerLocationRepository)
        {
            _unitOfWork = unitOfWork;
            _ownerLocationRepository = ownerLocationRepository;
        }

        public async Task<IEnumerable<OwnerLocation>> ListAsync()
        {
            return await _ownerLocationRepository.ListAsync();
        }

        public async Task<IEnumerable<OwnerLocation>> ListByCityIdAsync(int cityId)
        {
            return await _ownerLocationRepository.ListByCityIdAsync(cityId);
        }

        public async Task<IEnumerable<OwnerLocation>> ListByDateAsync(DateTime date)
        {
            return await _ownerLocationRepository.ListByDateAsync(date);
        }

        public async Task<IEnumerable<OwnerLocation>> ListByOwnerProfileIdAsync(int ownerId)
        {
            return await _ownerLocationRepository.ListByOwnerProfileIdAsync(ownerId);
        }

        public async Task<IEnumerable<OwnerLocation>> ListByProvinceIdAsync(int provinceId)
        {
            return await _ownerLocationRepository.ListByProvinceIdAsync(provinceId);
        }
        public async Task<OwnerLocationResponse> AssingOwnerLocation(int ownerId, int provinceId, int cityId, DateTime date)
        {
            try
            {
                await _ownerLocationRepository.AssignOwnerLocation(ownerId, provinceId, cityId, date);
                await _unitOfWork.CompleteAsync();

                OwnerLocation ownerLocation = await _ownerLocationRepository.FindByOwnerIdAndCityIdAndProvinceIdAndDateAsync(ownerId, provinceId, cityId, date);

                return new OwnerLocationResponse(ownerLocation);
            }
            catch(Exception ex)
            {
                return new OwnerLocationResponse($"An error ocurred while assigning Owner to City, Province and Date {ex.Message}");
            }
        }
        public async Task<OwnerLocationResponse> UnassingOwnerLocation(int ownerId, int provinceId, int cityId, DateTime date)
        {
            try
            {
                OwnerLocation ownerLocation = await _ownerLocationRepository.FindByOwnerIdAndCityIdAndProvinceIdAndDateAsync(ownerId, provinceId, cityId, date);

                _ownerLocationRepository.UnassingOwnerLocation(ownerId, provinceId, cityId, date);
                await _unitOfWork.CompleteAsync();
                
             
                return new OwnerLocationResponse(ownerLocation);
            }
            catch (Exception ex)
            {
                return new OwnerLocationResponse($"An error ocurred while unassigning Owner to City, Province and Date {ex.Message}");
            }
        }
    }
}
