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
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileService(IUnitOfWork unitOfWork, IProfileRepository profileRepository, IProvinceRepository provinceRepository, ICityRepository cityRepository)
        {
            _unitOfWork = unitOfWork;
            _profileRepository = profileRepository;
            _provinceRepository = provinceRepository;
            _cityRepository = cityRepository;
        }

        public async Task<ProfileResponse> DeleteAsync(int id)
        {
            var existingProfile = await _profileRepository.FindByIdAsync(id);
            if (existingProfile==null)
            {
                return new ProfileResponse("Profile not found");
            }
            try
            {
                _profileRepository.Remove(existingProfile);
                await _unitOfWork.CompleteAsync();

                return new ProfileResponse(existingProfile);
            }
            catch(Exception ex)
            {
                return new ProfileResponse($"An error ocurred while deleting profile: {ex.Message}");
            }
        }

        public async Task<ProfileResponse> GetByIdAsync(int id)
        {
            var existingProfile = await _profileRepository.FindByIdAsync(id);
            if (existingProfile == null)
            {
                return new ProfileResponse("Profile not found");
            }
            return new ProfileResponse(existingProfile);
        }

        public async Task<IEnumerable<Profile>> ListAsync()
        {
            return await _profileRepository.ListAsync();
        }

        public async Task<IEnumerable<Profile>> ListByCityIdAsync(int cityId)
        {
            return await _profileRepository.ListByCityIdAsync(cityId);
        }

        public async Task<IEnumerable<Profile>> ListByProvinceIdAsync(int provinceId)
        {
            return await _profileRepository.ListByProvinceIdAsync(provinceId);
        }

        public async Task<ProfileResponse> SaveAsync(int cityId, int provinceId, Profile profile)
        {
            var existingProvince = await _provinceRepository.FindById(provinceId);
            var existingCity = await _cityRepository.FindById(cityId);
            if (existingProvince == null)
            {
                return new ProfileResponse("Province not found, a profile needs a province to exist");
            }
            if (existingCity == null)
            {
                return new ProfileResponse("City not found, a profile needs a city to exist");
            }
            if (existingCity.ProvinceId != provinceId)
                return new ProfileResponse("The City does not belong that province");
            try
            {
                profile.ProvinceId = provinceId;
                profile.CityId = cityId;
                await _profileRepository.AddAsync(profile);
                await _unitOfWork.CompleteAsync();

                return new ProfileResponse(profile);
            }
            catch(Exception ex)
            {
                return new ProfileResponse($"An error ocurred while saving profile: {ex.Message}");
            }
        }

        public async Task<ProfileResponse> UpdateAsync(int id, Profile profile)
        {
            var existingProfile = await _profileRepository.FindByIdAsync(id);
            if (existingProfile == null)
            {
                return new ProfileResponse("Profile not found");
            }
            existingProfile.BirthDate = profile.BirthDate;
            existingProfile.Name = profile.Name;
            existingProfile.TelephonicNumber = profile.TelephonicNumber;
            try
            {
                _profileRepository.Update(existingProfile);
                await _unitOfWork.CompleteAsync();

                return new ProfileResponse(profile);
            }
            catch(Exception ex)
            {
                return new ProfileResponse($"An error ocurred while updating profile: {ex.Message}");
            }
        }
    }
}
