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
        private readonly IUnitOfWork _unitOfWork;

        public VetProfileService(IVetProfileRepository vetProfileRepository, IVetVeterinaryRepository vetVeterinaryRepository, IUnitOfWork unitOfWork)
        {
            _vetProfileRepository = vetProfileRepository;
            _vetVeterinaryRepository = vetVeterinaryRepository;
            _unitOfWork = unitOfWork;
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

        public async Task<VetProfileResponse> SaveAsync(VetProfile vetProfile)
        {
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
