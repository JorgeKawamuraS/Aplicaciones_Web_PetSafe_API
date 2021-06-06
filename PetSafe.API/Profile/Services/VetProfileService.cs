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
    public class VetProfileService : IVetProfileService
    {
        private readonly IVetProfileRepository _vetProfileRepository;
        private readonly IVetVeterinaryRepository _vetVeterinaryRepository;
        private readonly IProvinceRepository _provinceRepository;
        private readonly ICityRepository _cityRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VetProfileService(IVetProfileRepository vetProfileRepository, IVetVeterinaryRepository vetVeterinaryRepository, 
            IUnitOfWork unitOfWork, IProvinceRepository provinceRepository, ICityRepository cityRepository, IUserRepository userRepository)
        {
            _vetProfileRepository = vetProfileRepository;
            _vetVeterinaryRepository = vetVeterinaryRepository;
            _unitOfWork = unitOfWork;
            _provinceRepository = provinceRepository;
            _cityRepository = cityRepository;
            _userRepository = userRepository;
        }

        public async Task<VetProfileResponse> DeleteAsync(int id)
        {
            var existingVetProfile = await _vetProfileRepository.FindById(id);
            if (existingVetProfile==null)
            {
                return new VetProfileResponse("VetProfile not found");
            }
            try
            {
                _vetProfileRepository.Remove(existingVetProfile);
                await _unitOfWork.CompleteAsync();

                return new VetProfileResponse(existingVetProfile);
            }
            catch(Exception ex)
            {
                return new VetProfileResponse($"An error ocurred while deleting VetProfile: {ex.Message}");
            }
        }

        public async Task<VetProfileResponse> GetByIdAsync(int id)
        {
            var existingVetProfile = await _vetProfileRepository.FindById(id);
            if (existingVetProfile == null)
            {
                return new VetProfileResponse("VetProfile not found");
            }
            return new VetProfileResponse(existingVetProfile);
        }

        public async Task<IEnumerable<VetProfile>> ListAsync()
        {
            return await _vetProfileRepository.ListAsync();
        }

        public async Task<IEnumerable<VetProfile>> ListByVeterinaryIdAsync(int veterinaryId)
        {
            var vetVetinary = await _vetVeterinaryRepository.ListByVeterinaryIdAsync(veterinaryId);
            var vetProfiles = vetVetinary.Select(vv=>vv.VetProfile).ToList();
            return vetProfiles;
        }

        public async Task<VetProfileResponse> SaveAsync(int provinceId, int cityId, int userId, VetProfile vetProfile)
        {
            var existingProvince = await _provinceRepository.FindById(provinceId);
            var existingCity = await _cityRepository.FindById(cityId);
            var existingUser = await _userRepository.FindByIdAsync(userId);
            if (existingProvince == null)
            {
                return new VetProfileResponse("Province not found, a vet needs a province to exist");
            }
            if (existingCity == null)
            {
                return new VetProfileResponse("City not found, a vet needs a city to exist");
            }
            if (existingCity.ProvinceId != provinceId)
            {
                return new VetProfileResponse("The City does not exist in the province");
            }
            if (existingUser == null)
            {
                return new VetProfileResponse("The User does not exist, a profile of vet depends of an user");
            }
            IEnumerable<VetProfile> vetProfiles= await ListAsync();
            List<VetProfile> vetProfilesList = vetProfiles.ToList();
            bool differentUserId = true;
            vetProfilesList.ForEach(vetP =>
            {
                if(vetP.UserId==userId)
                    differentUserId=false;
            });
            if (!differentUserId)
            {
                return new VetProfileResponse("The User is on used of other profile");
            }
            if (existingUser.UserTypeVet == false)
            {
                return new VetProfileResponse("The User is for owner profiles");
            }
            try
            {
                await _vetProfileRepository.AddAsync(vetProfile);
                await _unitOfWork.CompleteAsync();

                return new VetProfileResponse(vetProfile);
            }
            catch (Exception ex)
            {
                return new VetProfileResponse($"An error ocurred while saving VetProfile: {ex.Message}");
            }
                
        }

        public async Task<VetProfileResponse> UpdateAsync(int id, VetProfile vetProfile)
        {
            var existingVetProfile = await _vetProfileRepository.FindById(id);
            if (existingVetProfile == null)
            {
                return new VetProfileResponse("VetProfile not found");
            }
            existingVetProfile.BirthDate = vetProfile.BirthDate;
            existingVetProfile.ExperienceYear = vetProfile.ExperienceYear;
            existingVetProfile.Name = vetProfile.Name;
            existingVetProfile.TelephonicNumber = existingVetProfile.TelephonicNumber;
            try
            {
                _vetProfileRepository.Update(existingVetProfile);
                await _unitOfWork.CompleteAsync();

                return new VetProfileResponse(existingVetProfile);
            }
            catch (Exception ex)
            {
                return new VetProfileResponse($"An error ocurred whike updating VetProfile: {ex.Message}");
            }
        }
    }
}
