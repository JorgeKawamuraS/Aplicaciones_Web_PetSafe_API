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
    public class OwnerProfileService : IOwnerProfileService
    {
        private readonly IOwnerProfileRepository _ownerProfileRepository;
        private readonly IOwnerLocationRepository _ownerLocationRepository;
        private readonly IPetOwnerRepository _petOwnerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OwnerProfileService(IUnitOfWork unitOfWork, IPetOwnerRepository petOwnerRepository, IOwnerLocationRepository ownerLocationRepository, IOwnerProfileRepository ownerProfileRepository)
        {
            _unitOfWork = unitOfWork;
            _petOwnerRepository = petOwnerRepository;
            _ownerLocationRepository = ownerLocationRepository;
            _ownerProfileRepository = ownerProfileRepository;
        }

        public async Task<OwnerProfileResponse> DeleteAsync(int id)
        {
            var existingOwnerProfile = await _ownerProfileRepository.FindById(id);
            if (existingOwnerProfile==null)
            {
                return new OwnerProfileResponse("Owner Profile not found");
            }
            try
            {
                _ownerProfileRepository.Remove(existingOwnerProfile);
                await _unitOfWork.CompleteAsync();

                return new OwnerProfileResponse(existingOwnerProfile);
            }
            catch(Exception ex)
            {
                return new OwnerProfileResponse($"An error ocurred while deleting owner profile; {ex.Message}");
            }
        }

        public async Task<OwnerProfileResponse> GetByIdAsync(int id)
        {
            var existingOwnerProfile = await _ownerProfileRepository.FindById(id);
            if (existingOwnerProfile == null)
            {
                return new OwnerProfileResponse("Owner Profile not found");
            }
            return new OwnerProfileResponse(existingOwnerProfile);
        }

        public async Task<IEnumerable<OwnerProfile>> ListAsync()
        {
            return await _ownerProfileRepository.ListAsync();
        }

        public async Task<IEnumerable<OwnerProfile>> ListByCityAsync(int cityId)
        {
            var ownerLocation = await _ownerLocationRepository.ListByCityIdAsync(cityId);
            var ownerProfiles = ownerLocation.Select(ol=>ol.OwnerProfile).ToList();
            return ownerProfiles;
        }

        public async Task<IEnumerable<OwnerProfile>> ListByPetIdAsync(int petId)
        {
            var petOwner = await _petOwnerRepository.ListByPetIdAsync(petId);
            var ownerProfiles = petOwner.Select(po => po.OwnerProfile).ToList();
            return ownerProfiles;
        }

        public async Task<IEnumerable<OwnerProfile>> ListByProvinceAsync(int provinceId)
        {
            var ownerLocation = await _ownerLocationRepository.ListByProvinceIdAsync(provinceId);
            var ownerProfiles = ownerLocation.Select(ol => ol.OwnerProfile).ToList();
            return ownerProfiles;
        }

        public async Task<OwnerProfileResponse> SaveAsync(OwnerProfile ownerProfile)
        {
            try
            {
                await _ownerProfileRepository.AddAsync(ownerProfile);
                await _unitOfWork.CompleteAsync();

                return new OwnerProfileResponse(ownerProfile);
            }
            catch(Exception ex)
            {
                return new OwnerProfileResponse($"An error ocurred while saving owner profile: {ex.Message}");
            }
        }

        public async Task<OwnerProfileResponse> UpdateAsync(int id, OwnerProfile ownerProfile)
        {
            var existingOwnerProfile = await _ownerProfileRepository.FindById(id);
            if (existingOwnerProfile == null)
            {
                return new OwnerProfileResponse("Owner Profile not found");
            }
            existingOwnerProfile.Name = ownerProfile.Name;
            existingOwnerProfile.TelephonicNumber = ownerProfile.TelephonicNumber;
            try
            {
                _ownerProfileRepository.Update(ownerProfile);
                await _unitOfWork.CompleteAsync();

                return new OwnerProfileResponse(existingOwnerProfile);
            }
            catch(Exception ex)
            {
                return new OwnerProfileResponse($"An error ocurred while updating owner profile: {ex.Message}");
            }
        }
    }
}
