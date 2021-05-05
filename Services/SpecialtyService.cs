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
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        private readonly IVeterinarySpecialtyRepository _veterinarySpecialtyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SpecialtyService(ISpecialtyRepository specialtyRepository, IVeterinarySpecialtyRepository veterinarySpecialtyRepository, IUnitOfWork unitOfWork)
        {
            _specialtyRepository = specialtyRepository;
            _veterinarySpecialtyRepository = veterinarySpecialtyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<SpecialtyResponse> DeleteAsync(int id)
        {
            var existingSpecialty = await _specialtyRepository.FindById(id);
            if (existingSpecialty==null)
            {
                return new SpecialtyResponse("Specialty not found");
            }
            try
            {
                _specialtyRepository.Remove(existingSpecialty);
                await _unitOfWork.CompleteAsync();

                return new SpecialtyResponse(existingSpecialty);
            }
            catch (Exception ex)
            {
                return new SpecialtyResponse(existingSpecialty);
            }
        }

        public async Task<SpecialtyResponse> GetByIdAsync(int id)
        {
            var existingSpecialty = await _specialtyRepository.FindById(id);
            if (existingSpecialty == null)
            {
                return new SpecialtyResponse("Specialty not found");
            }
            return new SpecialtyResponse(existingSpecialty);
        }

        public async Task<IEnumerable<Specialty>> ListAsync()
        {
            return await _specialtyRepository.ListAsync();
        }

        public async Task<IEnumerable<Specialty>> ListByVeterinaryidAsync(int veterinaryId)
        {
            var veterinarySpecialty = await _veterinarySpecialtyRepository.ListByVeterinaryIdAsync(veterinaryId);
            var specialties = veterinarySpecialty.Select(vv => vv.Specialty).ToList();
            return specialties;
        }

        public async Task<SpecialtyResponse> SaveAsync(Specialty specialty)
        {
            try
            {
                await _specialtyRepository.AddAsync(specialty);
                await _unitOfWork.CompleteAsync();

                return new SpecialtyResponse(specialty);
            }
            catch(Exception ex)
            {
                return new SpecialtyResponse($"An error ocurred while saving specialty: {ex.Message}");
            }
        }

        public async Task<SpecialtyResponse> UpdateAsync(int id, Specialty specialty)
        {
            var existingSpecialty = await _specialtyRepository.FindById(id);
            if (existingSpecialty == null)
            {
                return new SpecialtyResponse("Specialty not found");
            }
            existingSpecialty.Name = specialty.Name;
            try
            {
                _specialtyRepository.Update(existingSpecialty);
                await _unitOfWork.CompleteAsync();

                return new SpecialtyResponse(existingSpecialty);
            }
            catch(Exception ex)
            {
                return new SpecialtyResponse($"An error ocurred while updating specialty: {ex.Message}");
            }

        }
    }
}
