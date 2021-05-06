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
    public class IllnessService : IIllnessService
    {
        private readonly IIllnessRepository _illnessRepository;
        private readonly IPetIllnessRepository _petIllnessRepository;
        private readonly IUnitOfWork _unitOfWork;

        public IllnessService(IUnitOfWork unitOfWork, IPetIllnessRepository petIllnessRepository, IIllnessRepository illnessRepository)
        {
            _unitOfWork = unitOfWork;
            _petIllnessRepository = petIllnessRepository;
            _illnessRepository = illnessRepository;
        }

        public async Task<IllnessResponse> DeleteAsync(int id)
        {
            var existingIllness = await _illnessRepository.FindById(id);
            if (existingIllness==null)
            {
                return new IllnessResponse("Illness not found");
            }
            try
            {
                _illnessRepository.Remove(existingIllness);
                await _unitOfWork.CompleteAsync();

                return new IllnessResponse(existingIllness);
            }
            catch(Exception ex)
            {
                return new IllnessResponse($"An error ocurred while deleting illness: {ex.Message}");
            }
        }

        public async Task<IllnessResponse> GetByIdAsync(int id)
        {
            var existingIllness = await _illnessRepository.FindById(id);
            if (existingIllness == null)
            {
                return new IllnessResponse("Illness not found");
            }
            return new IllnessResponse(existingIllness);
        }

        public async Task<IEnumerable<Illness>> ListAsync()
        {
            return await _illnessRepository.ListAsync();
        }

        public async Task<IEnumerable<Illness>> ListByPetIdAsync(int petId)
        {
            var petIllness= await _petIllnessRepository.ListByPetIdAsync(petId);
            var illnesses = petIllness.Select(p=>p.Illness).ToList();
            return illnesses;
        }

        public async Task<IllnessResponse> SaveAsync(Illness illness)
        {
            try
            {
                await _illnessRepository.AddAsync(illness);
                await _unitOfWork.CompleteAsync();

                return new IllnessResponse(illness);
            }
            catch(Exception ex)
            {
                return new IllnessResponse($"An error ocurred while saving illness: {ex.Message}");
            }
        }

        public async Task<IllnessResponse> UpdateAsync(int id, Illness illness)
        {
            var existingIllness = await _illnessRepository.FindById(id);
            if (existingIllness == null)
            {
                return new IllnessResponse("Illness not found");
            }
            existingIllness.Name = illness.Name;
            try
            {
                _illnessRepository.Update(existingIllness);
                await _unitOfWork.CompleteAsync();

                return new IllnessResponse(existingIllness);
            }
            catch(Exception ex)
            {
                return new IllnessResponse($"An error ocurred while updating illness: {ex.Message}");
            }
        }
    }
}
