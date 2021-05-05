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
    public class VeterinarySpecialtyService : IVeterinarySpecialtyService
    {
        private readonly IVeterinarySpecialtyRepository _veterinarySpecialtyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VeterinarySpecialtyService(IVeterinarySpecialtyRepository veterinarySpecialtyRepository, IUnitOfWork unitOfWork)
        {
            _veterinarySpecialtyRepository = veterinarySpecialtyRepository;
            _unitOfWork = unitOfWork;
        }


        public async Task<IEnumerable<VeterinarySpecialty>> ListAsync()
        {
            return await _veterinarySpecialtyRepository.ListAsync();
        }

        public async Task<IEnumerable<VeterinarySpecialty>> ListBySpecialtyIdAsync(int specialtyId)
        {
            return await _veterinarySpecialtyRepository.ListBySpecialtyIdAsync(specialtyId);
        }

        public async Task<IEnumerable<VeterinarySpecialty>> ListByVeterinaryIdAsync(int veterinaryId)
        {
            return await _veterinarySpecialtyRepository.ListByVeterinaryIdAsync(veterinaryId);
        }

        public async Task<VeterinarySpecialtyResponse> AssignVeterinarySpecialtyAsync(int veterinaryId, int specialtyId)
        {
            try
            {
                await _veterinarySpecialtyRepository.AssignVeterinarySpecialty(veterinaryId, specialtyId);
                await _unitOfWork.CompleteAsync();

                VeterinarySpecialty veterinarySpecialty = await _veterinarySpecialtyRepository.FindByVeterinaryIdAndSpecialtyId(veterinaryId, specialtyId);

                return new VeterinarySpecialtyResponse(veterinarySpecialty);
            }
            catch (Exception ex)
            {
                return new VeterinarySpecialtyResponse($"An error ocurred while assigning Veterinary to Specialty {ex.Message}");
            }
        }

        public async Task<VeterinarySpecialtyResponse> UnassignVeterinarySpecialtyAsync(int veterinaryId, int specialtyId)
        {
            try
            {
                VeterinarySpecialty veterinarySpecialty = await _veterinarySpecialtyRepository.FindByVeterinaryIdAndSpecialtyId(veterinaryId, specialtyId);
                _veterinarySpecialtyRepository.UnassignVeterinarySpecialty(veterinaryId, specialtyId);
                await _unitOfWork.CompleteAsync();

               return new VeterinarySpecialtyResponse(veterinarySpecialty);
            }
            catch (Exception ex)
            {
                return new VeterinarySpecialtyResponse($"An error ocurred while unassigning Veterinary to Specialty {ex.Message}");
            }
        }
    }
}
