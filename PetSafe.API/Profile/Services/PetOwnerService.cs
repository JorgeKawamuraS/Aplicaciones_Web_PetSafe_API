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
    public class PetOwnerService : IPetOwnerService
    {
        private readonly IPetOwnerRepository _petOwnerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PetOwnerService(IUnitOfWork unitOfWork, IPetOwnerRepository petOwnerRepository)
        {
            _unitOfWork = unitOfWork;
            _petOwnerRepository = petOwnerRepository;
        }

        
        public async Task<IEnumerable<PetOwner>> ListAsync()
        {
            return await _petOwnerRepository.ListAsync();
        }

        public async Task<IEnumerable<PetOwner>> ListByOnwerIdAsync(int ownerId)
        {
            return await _petOwnerRepository.ListByOnwerIdAsync(ownerId);
        }

        public async Task<IEnumerable<PetOwner>> ListByPetIdAsync(int petId)
        {
            return await _petOwnerRepository.ListByPetIdAsync(petId);
        }

        public async Task<PetOwnerResponse> AssingPetOwner(int petId, int ownerId, bool principal)
        {
            try
            {
                await _petOwnerRepository.AssignPetOwner(petId, ownerId,principal);
                await _unitOfWork.CompleteAsync();

                PetOwner petOwner = await _petOwnerRepository.FindByPetIdAndOwnerId(petId, ownerId);

                return new PetOwnerResponse(petOwner);

            }
            catch(Exception ex)
            {
                return new PetOwnerResponse($"An error ocurred while assigning Pet to Owner: {ex.Message}");
            }
        }

        public async Task<PetOwnerResponse> UnassingPetOwner(int petId, int ownerId)
        {
            try
            {
                PetOwner petOwner = await _petOwnerRepository.FindByPetIdAndOwnerId(petId, ownerId);
                _petOwnerRepository.UnassignPetOwner(petId, ownerId);
                await _unitOfWork.CompleteAsync();

                return new PetOwnerResponse(petOwner);
            }
            catch(Exception ex)
            {
                return new PetOwnerResponse($"An error ocurred while unassigning Pet to Owner: {ex.Message}");
            }
        }
    }
}
