﻿using PetSafe.API.Domain.Models;
using PetSafe.API.Domain.Persistence.Repositories;
using PetSafe.API.Domain.Services;
using PetSafe.API.Domain.Services.Communications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetSafe.API.Services
{
    public class VeterinaryProfileService : IVeterinaryProfileService
    {
        private readonly IVeterinaryProfileRepository _veterinaryProfileRepository;
        private readonly IVeterinarySpecialtyRepository _veterinarySpecialtyRepository;
        private readonly IVetVeterinaryRepository _vetVeterinaryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VeterinaryProfileService(IVeterinaryProfileRepository veterinaryProfileRepository, IVeterinarySpecialtyRepository veterinarySpecialtyRepository,
            IVetVeterinaryRepository vetVeterinaryRepository, IUnitOfWork unitOfWork)
        {
            _veterinaryProfileRepository = veterinaryProfileRepository;
            _veterinarySpecialtyRepository = veterinarySpecialtyRepository;
            _vetVeterinaryRepository = vetVeterinaryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<VeterinaryProfileResponse> DeleteAsync(int id)
        {
            var existingVeterinaryProfile = await _veterinaryProfileRepository.FindById(id);
            if (existingVeterinaryProfile==null)
            {
                return new VeterinaryProfileResponse("Veterinary not found");
            }
            try
            {
                _veterinaryProfileRepository.Remove(existingVeterinaryProfile);
                await _unitOfWork.CompleteAsync();

                return new VeterinaryProfileResponse(existingVeterinaryProfile);
            }
            catch (Exception ex)
            {
                return new VeterinaryProfileResponse($"An error ocurred while deleting veterinary: {ex.Message}");
            }
        }

        public async Task<VeterinaryProfileResponse> GetByIdAsync(int id)
        {
            var existingVeterinaryProfile = await _veterinaryProfileRepository.FindById(id);
            if (existingVeterinaryProfile == null)
            {
                return new VeterinaryProfileResponse("Veterinary not found");
            }
            return new VeterinaryProfileResponse(existingVeterinaryProfile);
        }

        public async Task<IEnumerable<VeterinaryProfile>> ListAsync()
        {
            return await _veterinaryProfileRepository.ListAsync();
        }

        public async Task<IEnumerable<VeterinaryProfile>> ListBySpecialtyIdAsync(int specialtyId)
        {
            var vetSpecialty = await _veterinarySpecialtyRepository.ListBySpecialtyIdAsync(specialtyId);
            var veterinaryProfiles = vetSpecialty.Select(vs => vs.VeterinaryProfile).ToList();
            return veterinaryProfiles;
        }

        public async Task<IEnumerable<VeterinaryProfile>> ListByVetIdAsync(int vetId)
        {
            var vetVeterinary = await _vetVeterinaryRepository.ListByVetIdAsync(vetId);
            var veterinaryProfiles = vetVeterinary.Select(vs => vs.VeterinaryProfile).ToList();
            return veterinaryProfiles;
        }

        public async Task<VeterinaryProfileResponse> SaveAsync(VeterinaryProfile veterinaryProfile)
        {
            try
            {
                await _veterinaryProfileRepository.AddAsync(veterinaryProfile);
                await _unitOfWork.CompleteAsync();

                return new VeterinaryProfileResponse(veterinaryProfile);
            }
            catch(Exception ex)
            {
                return new VeterinaryProfileResponse($"An error ocurred while saving veterinary profile: {ex.Message}");
            }
        }

        public async Task<VeterinaryProfileResponse> UpdateAsync(int id, VeterinaryProfile veterinaryProfile)
        {
            var existingVeterinaryProfile = await _veterinaryProfileRepository.FindById(id);
            if (existingVeterinaryProfile == null)
            {
                return new VeterinaryProfileResponse("Veterinary not found");
            }
            existingVeterinaryProfile.Name = veterinaryProfile.Name;
            existingVeterinaryProfile.TelephonicNumber = veterinaryProfile.TelephonicNumber;
            try
            {
                _veterinaryProfileRepository.Update(existingVeterinaryProfile);
                await _unitOfWork.CompleteAsync();

                return new VeterinaryProfileResponse(existingVeterinaryProfile);
            }
            catch (Exception ex)
            {
                return new VeterinaryProfileResponse($"An error ocurred while updating: {ex.Message}");
            }
        }
    }
}
