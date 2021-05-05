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
    public class VetVeterinaryService : IVetVeterinaryService
    {
        private readonly IVetVeterinaryRepository _vetVeterinaryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VetVeterinaryService(IVetVeterinaryRepository vetVeterinaryRepository, IUnitOfWork unitOfWork)
        {
            _vetVeterinaryRepository = vetVeterinaryRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<VetVeterinary>> ListAsync()
        {
            return await _vetVeterinaryRepository.ListAsync();
        }

        public async Task<IEnumerable<VetVeterinary>> ListByVeterinaryIdAsync(int veterinaryId)
        {
            return await _vetVeterinaryRepository.ListByVeterinaryIdAsync(veterinaryId);
        }

        public async Task<IEnumerable<VetVeterinary>> ListByVetIdAsync(int vetId)
        {
            return await _vetVeterinaryRepository.ListByVetIdAsync(vetId);
        }

        public async Task<VetVeterinaryResponse> AssignVetVeterinaryAsync(int vetId, int veterinaryId)
        {
            try
            {
                await _vetVeterinaryRepository.AssignVetVeterinary(vetId, veterinaryId);
                await _unitOfWork.CompleteAsync();

                VetVeterinary vetVeterinary = await _vetVeterinaryRepository.FindByVetIdAndVeterinaryIdAsync(vetId,veterinaryId);

                return new VetVeterinaryResponse(vetVeterinary);

            }
            catch (Exception ex)
            {
                return new VetVeterinaryResponse($"An error ocurred while assigning Vet to Veterinary {ex.Message}");
            }
        }

        public async Task<VetVeterinaryResponse> UnassignVetVeterinaryAsync(int vetId, int veterinaryId)
        {
            try
            {
                VetVeterinary vetVeterinary = await _vetVeterinaryRepository.FindByVetIdAndVeterinaryIdAsync(vetId, veterinaryId);
                _vetVeterinaryRepository.UnassignVetVeterinary(vetId, veterinaryId);
                await _unitOfWork.CompleteAsync();

                
                return new VetVeterinaryResponse(vetVeterinary);

            }
            catch (Exception ex)
            {
                return new VetVeterinaryResponse($"An error ocurred while unassigning Vet to Veterinary {ex.Message}");
            }
        }
    }
}
