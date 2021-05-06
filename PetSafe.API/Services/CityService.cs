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
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IOwnerLocationRepository _ownerLocationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CityService(ICityRepository cityRepository, IOwnerLocationRepository ownerLocationRepository, IUnitOfWork unitOfWork)
        {
            _cityRepository = cityRepository;
            _ownerLocationRepository = ownerLocationRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CityResponse> DeleteAsync(int id)
        {
            var existingCity = await _cityRepository.FindById(id);
            if (existingCity==null)
            {
                return new CityResponse("City not found");
            }
            try
            {
                _cityRepository.Remove(existingCity);
                await _unitOfWork.CompleteAsync();

                return new CityResponse(existingCity);
            }
            catch(Exception ex)
            {
                return new CityResponse($"An error ocurred while deleting city: {ex.Message}");
            }
        }

        public async Task<CityResponse> GetByIdAsync(int id)
        {
            var existingCity = await _cityRepository.FindById(id);
            if (existingCity == null)
            {
                return new CityResponse("City not found");
            }
            return new CityResponse(existingCity);
        }

        public async Task<IEnumerable<City>> ListAsync()
        {
            return await _cityRepository.ListAsync();
        }

        public async Task<IEnumerable<City>> ListByOwnerProfileIdAsync(int ownerProfileId)
        {
            var ownerLocation = await _ownerLocationRepository.ListByOwnerProfileIdAsync(ownerProfileId);
            var cities = ownerLocation.Select(ol=>ol.City).ToList();
            return cities;
        }

        public async Task<IEnumerable<City>> ListByProvinceIdAsync(int provinceId)
        {
            return await _cityRepository.ListByProvinceIdAsync(provinceId);
        }

        public async Task<CityResponse> SaveAsync(City city)
        {
            try
            {
                await _cityRepository.AddAsync(city);
                await _unitOfWork.CompleteAsync();

                return new CityResponse(city);
            }
            catch (Exception ex)
            {
                return new CityResponse($"An error ocurred while saving city: {ex.Message}");
            }
        }

        public async Task<CityResponse> UpdateAsync(int id, City city)
        {
            var existingCity = await _cityRepository.FindById(id);
            if (existingCity==null)
            {
                return new CityResponse("City not found");
            }
            existingCity.Name = city.Name;
            try
            {
                _cityRepository.Update(existingCity);
                await _unitOfWork.CompleteAsync();

                return new CityResponse(existingCity);
            }
            catch (Exception ex)
            {
                return new CityResponse($"An error ocurred while updating city: {ex.Message}");
            }
        }
    }
}
