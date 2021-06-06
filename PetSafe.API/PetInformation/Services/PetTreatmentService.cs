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
    public class PetTreatmentService : IPetTreatmentService
    {
        private readonly IPetTreatmentRepository _petTreatmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PetTreatmentService(IUnitOfWork unitOfWork, IPetTreatmentRepository petTreatmentRepository)
        {
            _unitOfWork = unitOfWork;
            _petTreatmentRepository = petTreatmentRepository;
        }

        
        public async Task<IEnumerable<PetTreatment>> ListAsync()
        {
            return await _petTreatmentRepository.ListAsync();
        }

        public async Task<IEnumerable<PetTreatment>> ListByDateAsync(DateTime date)
        {
            return await _petTreatmentRepository.ListByDateAsync(date);
        }

        public async Task<IEnumerable<PetTreatment>> ListByPetIdAsync(int petId)
        {
            return await _petTreatmentRepository.ListByPetIdAsync(petId);
        }

        public async Task<IEnumerable<PetTreatment>> ListByTreatmentIdAsync(int treatmentId)
        {
            return await _petTreatmentRepository.ListByTreatmentIdAsync(treatmentId);
        }

        public async Task<PetTreatmentResponse> AssignPetTreatmentAsync(int petId, int treatmentId, DateTime date)
        {
            try
            {
                await _petTreatmentRepository.AssignPetTreatment(petId, treatmentId, date);
                await _unitOfWork.CompleteAsync();

                PetTreatment petTreatment = await _petTreatmentRepository.FindByPetIdAndTreatmentIdAndDate(petId, treatmentId, date);

                return new PetTreatmentResponse(petTreatment);
            }
            catch (Exception ex)
            {
                return new PetTreatmentResponse($"An error ocurred while assigning Pet to Treatment: {ex.Message}");
            }
        }

        public async Task<PetTreatmentResponse> UnassignPetTreatmentAsync(int petId, int treatmentId, DateTime date)
        {
            try
            {
                PetTreatment petTreatment = await _petTreatmentRepository.FindByPetIdAndTreatmentIdAndDate(petId, treatmentId, date);
                _petTreatmentRepository.UnassignPetTreatment(petId,treatmentId,date);
                await _unitOfWork.CompleteAsync();

                return new PetTreatmentResponse(petTreatment);
            }
            catch (Exception ex)
            {
                return new PetTreatmentResponse($"An error ocurred while unassigning Pet to Treatment {ex.Message}");
            }
        }
    }
}
