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
    public class PetProfileService : IPetProfileService
    {
        private readonly IPetProfileRepository _petProfileRepository;
        private readonly IPetOwnerRepository _petOwnerRepository;
        private readonly IPetIllnessRepository _petIllnessRepository;
        private readonly IPetTreatmentRepository _petTreatmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PetProfileService(IUnitOfWork unitOfWork, IPetTreatmentRepository petTreatmentRepository, IPetIllnessRepository petIllnessRepository, IPetOwnerRepository petOwnerRepository, IPetProfileRepository petProfileRepository)
        {
            _unitOfWork = unitOfWork;
            _petTreatmentRepository = petTreatmentRepository;
            _petIllnessRepository = petIllnessRepository;
            _petOwnerRepository = petOwnerRepository;
            _petProfileRepository = petProfileRepository;
        }

        public async Task<PetProfileResponse> DeleteAsync(int id)
        {
            var existingPetProfile = await _petProfileRepository.FindById(id);
            if (existingPetProfile==null)
            {
                return new PetProfileResponse("Pet Profile not found");
            }
            try
            {
                _petProfileRepository.Remove(existingPetProfile);
                await _unitOfWork.CompleteAsync();

                return new PetProfileResponse(existingPetProfile);
            }
            catch(Exception ex)
            {
                return new PetProfileResponse($"An error ocurred while deleting Pet Profile: {ex.Message}");
            }
        }

        public async Task<PetProfileResponse> GetByIdAsync(int id)
        {
            var existingPetProfile = await _petProfileRepository.FindById(id);
            if (existingPetProfile == null)
            {
                return new PetProfileResponse("Pet Profile not found");
            }
            return new PetProfileResponse(existingPetProfile);
        }

        public async Task<IEnumerable<PetProfile>> ListAsync()
        {
            return await _petProfileRepository.ListAsync();
        }

        public async Task<IEnumerable<PetProfile>> ListByIllnessIdAsync(int illnessId)
        {
            var petIllness = await _petIllnessRepository.ListByIllnessIdAsync(illnessId);
            var petProfile = petIllness.Select(pi => pi.PetProfile).ToList();
            return petProfile;
        }

        public async Task<IEnumerable<PetProfile>> ListByOwnerProfileIdAsync(int ownerProfileId)
        {
            var petOwner = await _petOwnerRepository.ListByOnwerIdAsync(ownerProfileId);
            var petProfile = petOwner.Select(po=>po.PetProfile).ToList();
            return petProfile;
        }

        public async Task<IEnumerable<PetProfile>> ListByTreatmentIdAsync(int treatmentId)
        {
            var petTreatment = await _petTreatmentRepository.ListByTreatmentIdAsync(treatmentId);
            var petProfile = petTreatment.Select(pt => pt.PetProfile).ToList();
            return petProfile;
        }

        public async Task<PetProfileResponse> SaveAsync(PetProfile petProfile)
        {
            try
            {
                await _petProfileRepository.AddAsync(petProfile);
                await _unitOfWork.CompleteAsync();

                return new PetProfileResponse(petProfile);
            }
            catch(Exception ex)
            {
                return new PetProfileResponse($"An error ocurred while saving PetProfile: {ex.Message}");
            }
        }

        public async Task<PetProfileResponse> UpdateAsync(int id, PetProfile petProfile)
        {
            var existingPetProfile = await _petProfileRepository.FindById(id);
            if (existingPetProfile == null)
            {
                return new PetProfileResponse("Pet Profile not found");
            }
            existingPetProfile.BirthDate = petProfile.BirthDate;
            existingPetProfile.Name = petProfile.Name;
            try
            {
                _petProfileRepository.Update(existingPetProfile);
                await _unitOfWork.CompleteAsync();

                return new PetProfileResponse(existingPetProfile);
            }
            catch(Exception ex)
            {
                return new PetProfileResponse($"An error ocurred while updating PetProfile: {ex.Message}");
            }
        }
    }
}
