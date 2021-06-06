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
    public class TreatmentService : ITreatmentService
    {
        private readonly ITreatmentRepository _treatmentRepository;
        private readonly IPetTreatmentRepository _petTreatmentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TreatmentService(ITreatmentRepository treatmentRepository, IPetTreatmentRepository petTreatmentRepository, IUnitOfWork unitOfWork)
        {
            _treatmentRepository = treatmentRepository;
            _petTreatmentRepository = petTreatmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TreatmentResponse> DeleteAsync(int id)
        {
            var existingTreatment = await _treatmentRepository.FindById(id);
            if (existingTreatment == null)
            {
                return new TreatmentResponse("Treatment not found");
            }
            try
            {
                _treatmentRepository.Remove(existingTreatment);
                await _unitOfWork.CompleteAsync();

                return new TreatmentResponse(existingTreatment);
            }
            catch(Exception ex)
            {
                return new TreatmentResponse($"An error ocurred while deleting treatment: {ex.Message}");
            }
        }

        public async Task<TreatmentResponse> GetByIdAsync(int id)
        {
            var existingTreatment = await _treatmentRepository.FindById(id);
            if (existingTreatment == null)
            {
                return new TreatmentResponse("Treatment not found");
            }
            return new TreatmentResponse(existingTreatment);
        }

        public async Task<IEnumerable<Treatment>> ListAsync()
        {
            return await _treatmentRepository.ListAsync();
        }

        public async Task<IEnumerable<Treatment>> ListByPetIdAsync(int petId)
        {
            var petTreatment = await _petTreatmentRepository.ListByPetIdAsync(petId);
            var treatments = petTreatment.Select(pt => pt.Treatment).ToList();
            return treatments;
        }

        public async Task<TreatmentResponse> SaveAsync(Treatment treatment)
        {
            try
            {
                await _treatmentRepository.AddAsync(treatment);
                await _unitOfWork.CompleteAsync();

                return new TreatmentResponse(treatment);
            }
            catch(Exception ex)
            {
                return new TreatmentResponse($"An error ocurred while saving treatment: {ex.Message}");
            }
        }

        public async Task<TreatmentResponse> UpdateAsync(int id, Treatment treatment)
        {
            var existingTreatment = await _treatmentRepository.FindById(id);
            if (existingTreatment == null)
            {
                return new TreatmentResponse("Treatment not found");
            }
            existingTreatment.Name = treatment.Name;
            try
            {
                _treatmentRepository.Update(existingTreatment);
                await _unitOfWork.CompleteAsync();

                return new TreatmentResponse(existingTreatment);
            }
            catch (Exception ex)
            {
                return new TreatmentResponse($"An error ocurred while updating treatment: {ex.Message}");
            }
        }
    }
}
