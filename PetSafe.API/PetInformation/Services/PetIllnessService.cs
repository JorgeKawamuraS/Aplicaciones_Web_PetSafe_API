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
    public class PetIllnessService : IPetIllnessService
    {
        private readonly IPetIllnessRepository _petIllnessRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PetIllnessService(IUnitOfWork unitOfWork, IPetIllnessRepository petIllnessRepository)
        {
            _unitOfWork = unitOfWork;
            _petIllnessRepository = petIllnessRepository;
        }

        public async Task<IEnumerable<PetIllness>> ListAsync()
        {
            return await _petIllnessRepository.ListAsync();
        }

        public async Task<IEnumerable<PetIllness>> ListByIllnessIdAsync(int illnessId)
        {
            return await _petIllnessRepository.ListByIllnessIdAsync(illnessId);
        }

        public async Task<IEnumerable<PetIllness>> ListByPetIdAsync(int petId)
        {
            return await _petIllnessRepository.ListByPetIdAsync(petId);
        }

        public async Task<PetIllnessResponse> AssignPetIllness(int petId, int illnessId)
        {
            try
            {
                await _petIllnessRepository.AssignPetIllness(petId, illnessId);
                await _unitOfWork.CompleteAsync();

                PetIllness petIllness = await _petIllnessRepository.FindByPetIdAndIllnessId(petId, illnessId);

                return new PetIllnessResponse(petIllness);
            }
            catch(Exception ex)
            {
                return new PetIllnessResponse($"An error ocurred while assigning Pet to Illness: {ex.Message}");
            }
        }

        public async Task<PetIllnessResponse> UnassignPetIllness(int petId, int illnessId)
        {
            try
            {
                PetIllness petIllness = await _petIllnessRepository.FindByPetIdAndIllnessId(petId, illnessId);
                _petIllnessRepository.UnassignPetIllness(petId, illnessId);
                await _unitOfWork.CompleteAsync();

                return new PetIllnessResponse(petIllness);
            }
            catch(Exception ex)
            {
                return new PetIllnessResponse($"An error ocurred while unassigning Pet to Illness: {ex.Message}");
            }
        }
    }
}
