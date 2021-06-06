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
        private readonly IProvinceRepository _provinceRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OwnerProfileService(IUnitOfWork unitOfWork, IPetOwnerRepository petOwnerRepository, IOwnerLocationRepository ownerLocationRepository, 
            IOwnerProfileRepository ownerProfileRepository, IProvinceRepository provinceRepository, ICityRepository cityRepository, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _petOwnerRepository = petOwnerRepository;
            _ownerLocationRepository = ownerLocationRepository;
            _ownerProfileRepository = ownerProfileRepository;
            _provinceRepository = provinceRepository;
            _cityRepository = cityRepository;
            _userRepository = userRepository;
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

        public async Task<OwnerProfileResponse> SaveAsync(int provinceId, int cityId, int userId, OwnerProfile ownerProfile)
        {
            var existingProvince = await _provinceRepository.FindById(provinceId);
            var existingCity = await _cityRepository.FindById(cityId);
            var existingUser = await _userRepository.FindByIdAsync(userId);
            if (existingProvince == null)
            {
                return new OwnerProfileResponse("Province not found, a owner needs a province to exist");
            }
            if (existingCity == null)
            {
                return new OwnerProfileResponse("City not found, a owner needs a city to exist");
            }
            if (existingCity.ProvinceId != provinceId)
            {
                return new OwnerProfileResponse("The City does not exist in the province");
            }
            if (existingUser==null)
            {
                return new OwnerProfileResponse("The User does not exist, a profile of owner depends of an user");
            }
            IEnumerable<OwnerProfile> ownerProfiles = await ListAsync();
            List<OwnerProfile> ownerProfilesList = ownerProfiles.ToList();
            bool differentUserId = true;

            if(ownerProfilesList!=null)
                ownerProfilesList.ForEach(ownerP =>
                {
                    if (ownerP.UserId == userId)
                        differentUserId = false;
                });

            if (!differentUserId)
            {
                return new OwnerProfileResponse("The User is on used of other profile");
            }

            if (existingUser.UserTypeVet == true)
            {
                return new OwnerProfileResponse("The User is for vet profiles");
            }

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
