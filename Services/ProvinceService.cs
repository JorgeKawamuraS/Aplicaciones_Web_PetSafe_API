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
    public class ProvinceService : IProvinceService
    {
        private readonly IProvinceRepository _provinceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProvinceService(IUnitOfWork unitOfWork, IProvinceRepository provinceRepository)
        {
            _unitOfWork = unitOfWork;
            _provinceRepository = provinceRepository;
        }

        public async Task<ProvinceResponse> DeleteAsync(int id)
        {
            var existingProvince = await _provinceRepository.FindById(id);
            if (existingProvince==null)
            {
                return new ProvinceResponse("Province not found");
            }
            try
            {
                _provinceRepository.Remove(existingProvince);
                await _unitOfWork.CompleteAsync();

                return new ProvinceResponse(existingProvince);
            }
            catch(Exception ex)
            {
                return new ProvinceResponse($"An error ocurred while deleting tag: {ex.Message}");
            }
        }

        public async Task<ProvinceResponse> GetByIdAsync(int id)
        {
            var existingProvince = await _provinceRepository.FindById(id);
            if (existingProvince == null)
            {
                return new ProvinceResponse("Province not found");
            }
            return new ProvinceResponse(existingProvince);
        }

        public async Task<IEnumerable<Province>> ListAsync()
        {
            return await _provinceRepository.ListAsync();
        }

        public async Task<ProvinceResponse> SaveAsync(Province province)
        {
            try
            {
                await _provinceRepository.AddAsync(province);
                await _unitOfWork.CompleteAsync();

                return new ProvinceResponse(province);
            }
            catch (Exception ex)
            {
                return new ProvinceResponse($"An error ocurred while saving province: {ex.Message}");
            }
        }

        public async Task<ProvinceResponse> UpdateAsync(int id, Province province)
        {
            var existingProvince = await _provinceRepository.FindById(id);
            if (existingProvince == null)
            {
                return new ProvinceResponse("Province not found");
            }
            existingProvince.Name = province.Name;
            try
            {
                _provinceRepository.Update(existingProvince);
                await _unitOfWork.CompleteAsync();

                return new ProvinceResponse(existingProvince);
            }
            catch (Exception ex)
            {
                return new ProvinceResponse($"An error ocurred while updating tag: {ex.Message}");
            }
        }
    }
}
